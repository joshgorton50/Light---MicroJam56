using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for the UI Image!

public class LevelManager : MonoBehaviour
{
    [Header("Level Progress")]
    public List<int> botsPerDay = new List<int> { 3, 5, 7 }; // E.g. Day 1 = 3 bots, Day 2 = 5 bots
    public int currentDayIndex = 0;
    public int botsCompletedToday = 0;

    [Header("Cinematic Transition")]
    public Image fadeScreen; // Drag your black Canvas Image here
    public Transform mainCamera; 
    public float cameraMoveDistance = 20f; // How far to the right the camera moves
    
    [Header("Speeds")]
    public float fadeSpeed = 1.5f;
    public float moveSpeed = 2f;
    public float waitTimeInBlack = 1f;

    private bool isTransitioning = false;

    // Call this function whenever a bot is fully repaired/submitted!
    public void BotCompleted()
    {
        // Prevent counting extra bots while the screen is fading
        if (isTransitioning) return; 

        botsCompletedToday++;
        print("Bot finished! Today's progress: " + botsCompletedToday + " / " + botsPerDay[currentDayIndex]);

        // Did we hit the goal for today?
        if (botsCompletedToday >= botsPerDay[currentDayIndex])
        {
            StartCoroutine(DayCompleteRoutine());
        }
    }

    private IEnumerator DayCompleteRoutine()
    {
        isTransitioning = true;
        print("Day Complete! Fading out...");

        // 1. FADE TO BLACK
        Color fadeColor = fadeScreen.color;
        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null; // Wait for next frame
        }

        // 2. WAIT IN THE DARK
        yield return new WaitForSeconds(waitTimeInBlack);

        // 3. MOVE THE CAMERA TO THE RIGHT
        Vector3 startPos = mainCamera.position;
        Vector3 endPos = startPos + new Vector3(cameraMoveDistance, 0f, 0f);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            mainCamera.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        mainCamera.position = endPos; // Snap it to exactly the right spot at the end

        // 4. RESET FOR NEXT DAY
        currentDayIndex++;
        botsCompletedToday = 0;
        
        // (Optional: You might want to call BotSpawner.SpawnNextBot() here!)

        // 5. FADE BACK IN
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null;
        }

        isTransitioning = false;
        print("New Day Started!");
        
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime * fadeSpeed;
            fadeScreen.color = fadeColor;
            yield return null;
        }

        isTransitioning = false;
        print("New Day Started!");

        // ADD THIS LINE HERE:
        FindObjectOfType<ShopManager>().SpawnHiddenItems();
    }
}