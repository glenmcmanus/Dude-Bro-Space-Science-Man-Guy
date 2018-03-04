using UnityEngine;
using UnityEngine.SceneManagement;

/* Created by Sterling Beeston January 28, 2018
 */ 

/*
 * LoseScreenControler handles UI input on the lose scene. 
 */ 
public class LoseScreenControler : MonoBehaviour {
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(ProgressTracker.instance.currentLevel, LoadSceneMode.Single);
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
