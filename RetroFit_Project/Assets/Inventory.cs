using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Button;
    public List<GameObject> shelves = new List<GameObject>();
    public List<GameObject> shelfSlots = new List<GameObject>();

    public float slideSpeed = 8f;

    private clickedOn buttonScript;
    private int currentOffset = 0;

    void Start()
    {
        buttonScript = Button.GetComponent<clickedOn>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (buttonScript.isClicked == true)
        {
            currentOffset++;
            buttonScript.isClicked = false;
        }

        
        for (int i = 0; i < shelves.Count; i++)
        {
            int targetSlotIndex = (i + currentOffset) % shelfSlots.Count;

            shelves[i].GetComponent<CircleCollider2D>().enabled = (targetSlotIndex == 0);

            if(shelves[i].GetComponent<Basket>().currentIteam != null)
            {
                shelves[i].GetComponent<CircleCollider2D>().enabled = false;
                shelves[i].GetComponent<Basket>().currentIteam.GetComponent<CapsuleCollider2D>().enabled = (targetSlotIndex == 0);
            }


            Vector2 targetPos = shelfSlots[targetSlotIndex].transform.position;

            
            shelves[i].transform.position = Vector2.Lerp(
                shelves[i].transform.position,
                targetPos,
                slideSpeed * Time.deltaTime
            );
        }
    }
}
