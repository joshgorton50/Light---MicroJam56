using UnityEngine;

public class DragDrop2D : MonoBehaviour
{

    public bool isDragging = false, inMan;
    public GameObject clamHome;

    private void OnMouseDown()
    {
        isDragging = true;
        transform.SetParent(null);   
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition();
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        Compatibility compScript = GetComponent<Compatibility>();

        
        bool isNotAttachedToBot = (transform.parent == null || transform.parent.GetComponent<Bot>() == null);


        if(compScript != null && compScript.botPart.ToString() == "clamp" && transform.parent == null)
        {
            print("go home");
            if (clamHome != null) 
            {
                transform.SetParent(clamHome.transform);
            }
            transform.localPosition = Vector2.zero;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePoint);
        worldPoint.z = 0f;
        return worldPoint;
    }
}
