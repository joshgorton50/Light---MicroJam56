using Unity.VisualScripting;
using UnityEngine;

public class BotClass : MonoBehaviour
{
    public string 
    botName,
    type,
    job,
    botGrade,
    size,
    child,
    wife,
    record;

    public bool deadBot;
    

    private void Start()
    {
        if (!deadBot)
        {
            IteamReader iteamReader = GameObject.Find("Canvas").GetComponent<IteamReader>();
            iteamReader.botName.text = botName;
            iteamReader.type.text = type;
            iteamReader.job.text = job;
            iteamReader.size.text = size;
            iteamReader.child.text = child;
            iteamReader.wife.text = wife;
            iteamReader.record.text = record;

        }
        else
        {
            RandomizeCode();
        }
    }
    public void RandomizeCode()
    {
        // 1. Pick a random uppercase letter from A to Z
        char firstLetter = (char)Random.Range('A', 'E' + 1);

        // 2. Pick a random number from 1 to 9 (This is the size your math checks!)
        int sizeNumber = Random.Range(1, 4);

        // 3. Pick a random lowercase letter from a to z
        char lastLetter = (char)Random.Range('a', 'c' + 1);

        // Combine them together into the final string
        botGrade = firstLetter.ToString();
        size = sizeNumber.ToString();
        type = lastLetter.ToString();
    }
}
