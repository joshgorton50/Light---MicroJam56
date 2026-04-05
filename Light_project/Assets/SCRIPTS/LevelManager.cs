using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{

    public void GoToMainlevel()
    {
        SceneManager.LoadScene(1);
    }
    
}
