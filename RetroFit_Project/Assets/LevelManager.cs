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

    private void Update()
    {
        // THE TIMER LOGIC
        if (inShopPhase && !isTransitioning)
        {
            currentShopTimer -= Time.deltaTime;

            if (currentShopTimer <= 0f)
            {
                // Time is up! Go back to work.
                StartCoroutine(ReturnFromShopRoutine());
            }
        }
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
        FindObjectOfType<ShopManager>().SpawnHiddenItems();
        
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
}