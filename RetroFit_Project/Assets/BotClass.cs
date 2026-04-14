using Unity.VisualScripting;
using UnityEngine;

public class BotClass : MonoBehaviour
{
    public string 
    botName,
    type,
    job,
    size,
    child,
    wife,
    record;

    private void Awake()
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
}
