using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public List<GameObject> botPrefabs = new List<GameObject>();
    
    [Header("Current Status")]
    public GameObject currentBot; // <--- YOUR NEW VARIABLE

    [Header("Leap Settings")]
    public Transform leapStartPoint; 
    public Transform spawnPoint;     
    public float leapDuration = 0.5f; 
    public float leapHeight = 3f;     

    public void SpawnNextBot()
    {
        currentBot.GetComponent<Bot>().leave = true;
        // 1. Check if we already have a bot on the table. 
        // (Optional: You might want to destroy or move the old one first!)


        if (botPrefabs.Count > 0)
        {
            GameObject newBot = Instantiate(botPrefabs[0], leapStartPoint.position, Quaternion.identity);
            
            // 2. Set your current bot variable IMMEDIATELY.
            currentBot = newBot;

            botPrefabs.RemoveAt(0);
            StartCoroutine(LeapRoutine(newBot));
        }
        else
        {
            print("Out of bots!");
        }
    }

    private IEnumerator LeapRoutine(GameObject bot)
    {
        yield return new WaitForSeconds(2.5f); // Pauses for 2.5 seconds
        float timeElapsed = 0f;
        Vector3 startPos = leapStartPoint.position;
        Vector3 endPos = spawnPoint.position;

        Collider2D botCol = bot.GetComponent<Collider2D>();
        if (botCol != null) botCol.enabled = false;

        while (timeElapsed < leapDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / leapDuration;

            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
            currentPos.y += Mathf.Sin(t * Mathf.PI) * leapHeight;

            bot.transform.position = currentPos;
            yield return null;
        }

        bot.transform.position = endPos;
        if (botCol != null) botCol.enabled = true;
    }
    
    // Bonus Game Jam Tool: Call this when a bot is fully repaired/finished to clear the slot
    public void ClearCurrentBot()
    {
        currentBot = null;
    }
}