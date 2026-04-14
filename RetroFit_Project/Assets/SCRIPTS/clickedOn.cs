using Unity.VisualScripting;
using UnityEngine;

public class clickedOn : MonoBehaviour
{
    public bool isClicked = false;
    public bool grabable;
    private void OnMouseDown()
    {
        isClicked
            = true;
            
        //FindObjectOfType<ShopManager>().RevealNextItem(transform);
    }
}
