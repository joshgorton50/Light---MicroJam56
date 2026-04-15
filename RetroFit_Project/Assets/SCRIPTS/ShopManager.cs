using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Setup")]
    public List<GameObject> itemPrefabs = new List<GameObject>(); 
    public Transform hiddenSpawnPoint; // Way off the bottom of the screen
    public Transform displayPoint;     // NEW: The single spot on the counter where items appear!
    
    [Header("Animation Settings")]
    public float lerpSpeed = 3f;

    private List<GameObject> spawnedItems = new List<GameObject>(); 
    private int currentItemIndex = 0;
    
    // NEW: We use this to remember which item is currently sitting on the counter
    private GameObject activeItem = null; 

    public void SpawnHiddenItems()
    {
        foreach (GameObject items in spawnedItems) Destroy(items);

        spawnedItems.Clear();
        
        foreach(ShopDraw draw in FindObjectsOfType<ShopDraw>())
        {
            draw.isClicked = false;
        }

        currentItemIndex = 0;
        activeItem = null; // Clear the counter at the start of a new day

        foreach (GameObject prefab in itemPrefabs)
        {
            GameObject newItem = Instantiate(prefab, hiddenSpawnPoint.position, Quaternion.identity);
            spawnedItems.Add(newItem);
        }
        
        print("Shop items spawned and ready!");
    }

    public void RevealNextItem()
    {
        if (currentItemIndex < spawnedItems.Count)
        {
            // 1. If there is already an item on the counter, drop it back down!
            if (activeItem != null)
            {
                StartCoroutine(MoveItemRoutine(activeItem, displayPoint.position, hiddenSpawnPoint.position));
            }

            // 2. Get the new item ready
            GameObject itemToMove = spawnedItems[currentItemIndex];
            
            // 3. Make the new item the "active" one for the next time we click
            activeItem = itemToMove; 
            currentItemIndex++; 
            
            // 4. Send the new item up to the counter!
            StartCoroutine(MoveItemRoutine(itemToMove, hiddenSpawnPoint.position, displayPoint.position));
        }
    }

    // This is a universal Lerp that can handle going up OR down
    private IEnumerator MoveItemRoutine(GameObject item, Vector3 startPos, Vector3 endPos)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * lerpSpeed;
            item.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        item.transform.position = endPos; 
    }
}