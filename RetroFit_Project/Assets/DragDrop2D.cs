using UnityEngine;

public class DragDrop2D : MonoBehaviour
{
    private Vector3 dragOffset;
    public bool isDragging = false;

    private void OnMouseDown()
    {
        //dragOffset = transform.position - GetMouseWorldPosition();
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
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePoint);
        worldPoint.z = 0f;
        return worldPoint;
    }
}
