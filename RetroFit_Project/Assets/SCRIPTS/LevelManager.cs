using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level Progress")]
    public List<int> botsPerDay = new List<int> { 3, 5, 7 }; 
    public int currentDayIndex = 0;
    public int botsCompletedToday = 0;

    [Header("Cinematic Transition")]
    public Image fadeScreen; 
    public Transform mainCamera; 
    public float cameraMoveDistance = 20f; 
    public float fadeSpeed = 1.5f;
    public float moveSpeed = 2f;
    public float waitTimeInBlack = 1f;

    [Header("Shop Timer")]
    public float shopDuration = 60f; // 60 seconds in the shop
    private float currentShopTimer;
    private bool inShopPhase = false;

    private bool isTransitioning = false;

    public float money;

    [Header("UI")]
    public TMPro.TMP_Text timerText;
    public TMPro.TMP_Text Money;

    private void Update()
{
    // THE TIMER LOGIC
    if (inShopPhase && !isTransitioning)
    {
        currentShopTimer -= Time.deltaTime;

        // --- THE NEW CLOCK MATH ---
        // 1. Divide by 60 to get total minutes
        int minutes = Mathf.FloorToInt(currentShopTimer / 60f);
        
        // 2. Use Modulo (%) to get the leftover seconds
        int seconds = Mathf.FloorToInt(currentShopTimer % 60f);

        // 3. Format it to always have 2 zeros (00:00) and update the text!
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // --- CHECK IF TIME IS UP ---
        if (currentShopTimer <= 0f)
        {
            // Lock it exactly to zero so it doesn't accidentally flash a negative number
            currentShopTimer = 0f; 
            if (timerText != null) timerText.text = "00:00";

            // Time is up! Go back to work.
            StartCoroutine(ReturnFromShopRoutine());
        }
    }
    Money.text = "£" + money.ToString();
}
    public void BotCompleted()
    {
        if (isTransitioning || inShopPhase) return; 

        botsCompletedToday++;
        print("Bot finished! Today's progress: " + botsCompletedToday + " / " + botsPerDay[currentDayIndex]);

        // Prevent index out of bounds if they finish all programmed days
        if (currentDayIndex >= botsPerDay.Count) 
        {
            print("YOU BEAT THE GAME!");
            return;
        }

        if (botsCompletedToday >= botsPerDay[currentDayIndex])
        {
            StartCoroutine(DayCompleteRoutine());
        }
    }

    // 1. GOING TO THE SHOP
    private IEnumerator DayCompleteRoutine()
    {
        isTransitioning = true;
        FindObjectOfType<ShopManager>().SpawnHiddenItems();
        
        // FADE OUT
        Color fadeColor = fadeScreen.color;
        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null; 
        }

        yield return new WaitForSeconds(waitTimeInBlack);

        // MOVE CAMERA RIGHT
        Vector3 startPos = mainCamera.position;
        Vector3 endPos = startPos + new Vector3(cameraMoveDistance, 0f, 0f);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            mainCamera.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        mainCamera.position = endPos; 

        // FADE IN
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null;
        }

        // SPAWN THE SHOP ITEMS AND START THE CLOCK
        
        currentShopTimer = shopDuration;
        inShopPhase = true;
        isTransitioning = false;
        
        print("Welcome to the shop! You have " + shopDuration + " seconds.");
    }

    // 2. RETURNING FROM THE SHOP
    private IEnumerator ReturnFromShopRoutine()
    {
        isTransitioning = true;
        inShopPhase = false; // Stop the timer
        print("Shop closed! Going back to work...");

        // FADE OUT
        Color fadeColor = fadeScreen.color;
        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null; 
        }

        yield return new WaitForSeconds(waitTimeInBlack);

        // MOVE CAMERA LEFT (Notice the MINUS sign on cameraMoveDistance)
        Vector3 startPos = mainCamera.position;
        Vector3 endPos = startPos - new Vector3(cameraMoveDistance, 0f, 0f);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            mainCamera.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        mainCamera.position = endPos; 

        // RESET PROGRESS FOR TOMORROW
        currentDayIndex++;
        botsCompletedToday = 0;
        
        // FADE IN
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null;
        }

        isTransitioning = false;
        print("New Day Started! Get back to repairing bots.");
    }


    public void GiveMoney()
{
    // 1. Grab the bot
    BotClass bot = GetComponent<BotSpawner>().currentBot.GetComponent<BotClass>();

    // 2. THE SAFETY CHECK: Is the string null or empty?
    if (string.IsNullOrEmpty(bot.botGrade))
    {
        print("ERROR: This bot's grade is blank in the Inspector! Giving default money.");
        money += 5; // Give a default amount so the game doesn't break
        return;     // Stop running the rest of this function
    }

    // 3. If it's safe, do the math!
    char gradeLetter = bot.botGrade[0];
    int gradeMultiplier = ('E' - gradeLetter) + 1;
    
    // Clamp it just to be ultra-safe
    gradeMultiplier = Mathf.Clamp(gradeMultiplier, 1, 5);
    
    money += 5 * gradeMultiplier;
}

}