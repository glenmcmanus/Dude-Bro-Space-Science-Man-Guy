using UnityEngine;
using UnityEngine.SceneManagement;

/* Created by Glen McManus January 28, 2018
 */

/*
 * Credits handles input in the credit scene.
 */ 
public class Credits : MonoBehaviour {

	void Update()
    {
        if(Input.anyKey)
        {
            if(FindObjectOfType<AudioSource>() != null)
                Destroy(FindObjectOfType<AudioSource>());

            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
