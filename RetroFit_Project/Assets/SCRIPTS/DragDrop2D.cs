using System.Collections;
using UnityEngine;

public class DragDrop2D : MonoBehaviour
{
    public bool isDragging = false, inMan;
    public GameObject clamHome;

    private void OnMouseDown()
    {
        isDragging = true;
        
        Compatibility compScript = GetComponent<Compatibility>();
        if (compScript != null) compScript.isDragging = true;

        transform.SetParent(null);   
        
        // You can keep this turned off while dragging!
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
        
        // Start your custom routine instead of running the math instantly
        StartCoroutine(HandleDropRoutine());
    }

    // THE MAGIC IENUMERATOR
    private IEnumerator HandleDropRoutine()
    {
        // 1. Turn the collider back on FIRST
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

        // 2. WAIT! This tells Unity to pause this script for exactly one physics frame.
        // This gives the Bot's OnTriggerEnter2D time to realize the item is inside it and set inMan = true!
        yield return new WaitForFixedUpdate();

        // 3. NOW run the rest of the math!
        Compatibility compScript = GetComponent<Compatibility>();
        if (compScript != null) compScript.isDragging = false; 

        // Because we waited a frame, !inMan is now 100% accurate!
        if(compScript != null && compScript.botPart.ToString() == "clamp" && !inMan)
        {
            print("go home");
            if (clamHome != null) 
            {
                transform.SetParent(clamHome.transform);
            }
            transform.localPosition = Vector3.zero; 
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