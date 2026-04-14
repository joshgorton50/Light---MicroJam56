using UnityEngine;

public class Bot : MonoBehaviour
{
    public string botName;
    public GameObject currentIteam, socket;
    public string acceptedCode = "A1a";
    
    private CircleCollider2D col;
    private GameObject hoveringItem; 
    public enum BotPartSocket
    {
        ArmLeft,
        ArmRight,
        HandLeft,
        HandRight,
        LegLeft,
        LegRight,
        core,

    }
    public BotPartSocket botPartSocket;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            hoveringItem = collision.gameObject;
            print("nohme");
            hoveringItem.GetComponent<DragDrop2D>().inMan = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == hoveringItem)
        {
            hoveringItem.GetComponent<DragDrop2D>().inMan = true;
            hoveringItem = null;
        }
    }

    private void Update()
    {
        if (hoveringItem != null && currentIteam == null)
        {
            // 1. Grab your Compatibility script!
            Compatibility compScript = hoveringItem.GetComponent<Compatibility>();

            // 2. Check if the item has the script AND if it has been dropped
            if (compScript != null && !compScript.isDragging)
            {
                // 3. Run your math check using the variables from the script
                if ((Compatibility.CheckCodes(compScript.itemCode, acceptedCode) && botPartSocket.ToString() == compScript.botPart.ToString()) || compScript.botPart.ToString() == "clamp")
                {
                    print("It fits! Get in me");
                    
                    currentIteam = hoveringItem;
                    currentIteam.transform.SetParent(socket.transform);
                    currentIteam.transform.localPosition = Vector3.zero;

                    col.enabled = false; 
                    hoveringItem = null; 
                }
                else 
                {
                    hoveringItem = null; 
                }
            }
        }

        if (currentIteam != null)
        {
            if (!currentIteam.transform.IsChildOf(transform))
            {
                currentIteam = null;
                col.enabled = true; 
            }
        }
    }
}