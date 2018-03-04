using UnityEngine;
using UnityEngine.SceneManagement;

/* Created by Glen McManus January 28, 2018
 */

/*
 * Win handles input and audio in the end-of-game win scene.
 */
public class Win : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(audioSource);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Credits", LoadSceneMode.Single);
        }
    }
}
