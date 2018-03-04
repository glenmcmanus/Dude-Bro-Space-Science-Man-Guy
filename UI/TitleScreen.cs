using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Written by Aiden Bull, January 27, 2018
 */

/*
 * TitleScreen handles GUI interactions on the main menu
 */ 
public class TitleScreen : MonoBehaviour {

    /**
     * Loads the first level.
     */
	public void StrOnClick(){
		SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
	}

    /**
     * Plays the credits.
     */
    public void Credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    /**
     * Quits the application
     */
    public void ExtOnClick(){
		Application.Quit();
	}
}
