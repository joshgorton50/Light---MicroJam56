using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class IteamReader : MonoBehaviour
{
    public TMP_Text itemCompatibility, itemPart,
    botName,
    type,
    job,
    size,
    child,
    wife,
    record;
    public Inventory inventory;
    public GameObject patientPanel;
    public float slideSpeed = 8f;
    public bool slide;

    public Button dischargeButton;
    
    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.openShelf.transform.GetChild(0).childCount > 0)
        {
            Transform grandchild = inventory.openShelf.transform.GetChild(0).GetChild(0);
            itemCompatibility.text = grandchild.gameObject.GetComponent<Compatibility>().itemCode;
            itemPart.text = grandchild.gameObject.GetComponent<Compatibility>().botPart.ToString();
        }
        else
        {
            itemCompatibility.text = "NULL";
            itemPart.text= "ERROR";
        }
        
        if (slide == true)
        {
            patientPanel.transform.localPosition = Vector2.Lerp(patientPanel.transform.localPosition, new Vector2(-288.4f, -35.5f), slideSpeed * Time.deltaTime);
        }
        else
        {
            patientPanel.transform.localPosition = Vector2.Lerp(patientPanel.transform.localPosition, new Vector2(-499, -35.5f), slideSpeed * Time.deltaTime);
        }

    }

    public void PanelMove()
    {
        slide = !slide;
    }

}
