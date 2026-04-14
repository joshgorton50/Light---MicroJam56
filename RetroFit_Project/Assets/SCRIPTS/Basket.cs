using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject currentIteam, basketHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && collision.gameObject.GetComponent<Compatibility>().botPart.ToString() != "clamp")
        {
            print("get in me");
            currentIteam = collision.gameObject;
            currentIteam.transform.SetParent(basketHolder.transform);
            currentIteam.transform.localPosition = Vector3.zero;
        }
    }

    private void Update()
    {
        if (currentIteam != null)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (!currentIteam.transform.IsChildOf(transform))
            {
                currentIteam = null;
            }
        }

    }
}
