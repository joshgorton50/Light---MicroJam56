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
            
            
            // Just tell the manager to reveal the next one!
            FindObjectOfType<ShopManager>().RevealNextItem();
        }
    }

    void Update()
    {
        if(isClicked && sr != null)
        {
            sr.color =  new Color(0.5f, 0.5f, 0.5f, 1f); 
        }else if(sr != null)
        {
            sr.color =  new Color(0.64f, 0.64f, 0.64f, 1f); 
        }
    }

}