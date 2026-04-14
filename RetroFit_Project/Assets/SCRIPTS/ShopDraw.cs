using UnityEngine;

public class ShopDraw : MonoBehaviour
{
    public bool isClicked = false;
    public bool grabable; 
    private SpriteRenderer sr;
private void Start()
    {
        // 2. Grab it the moment the game starts
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (isClicked == false)
        {
            isClicked = true;
            if (sr != null)
            {
                sr.color =  new Color(0.5f, 0.5f, 0.5f, 1f); 
                
                // Bonus tip: If Color.gray is too light, you can use Color.darkGray
                // Or make your own custom color like this: 
                // sr.color = new Color(0.5f, 0.5f, 0.5f, 1f); 
            }
            
            // Just tell the manager to reveal the next one!
            FindObjectOfType<ShopManager>().RevealNextItem();
        }
    }
    
}