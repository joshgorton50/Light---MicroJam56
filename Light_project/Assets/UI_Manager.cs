using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
     public static UI_Manager Instance;

     public GameObject MainMenu;

     int currentScene;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if(currentScene == 0)
        {
            MainMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(false);
        }
    }
}
