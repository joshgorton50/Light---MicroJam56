using UnityEditor;
using UnityEngine;

public class Compatibility : MonoBehaviour
{
    public string itemCode = "A1a";
    public bool isDragging = false;

    public enum BotPart
    {
        ArmLeft,
        ArmRight,
        HandLeft,
        HandRight,
        LegLeft,
        LegRight,
        core,
        clamp,

    }

    public BotPart botPart;
    

    public static bool CheckCodes(string item, string basket)
    {
        if (item.Length < 3 || basket.Length < 3) return false;

        int itemSize = item[1] - '0';
        int basketSize = basket[1] - '0';

        bool sizeMatch = Mathf.Abs(itemSize - basketSize) <= 1;
        bool typeMatch = item[2] == basket[2];
        

        return sizeMatch && typeMatch;
    }

    void Update()
    {
        if( transform.parent != null && transform.parent.GetComponent<BotClass>().deadBot == true)
        {
            itemCode = transform.parent.GetComponent<BotClass>().botGrade + transform.parent.GetComponent<BotClass>().size + transform.parent.GetComponent<BotClass>().type;
        }
        else
        {
            
        }
    }


    
}