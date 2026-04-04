using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager : MonoBehaviour
{

    public void GoToMainlevel()
    {
        SceneManager.LoadScene(1);
    }
    
}
