using UnityEngine;

/* Created by Glen McManus January 28, 2018
 */

/*
 * ProgressTracker tracks the current level index in the build settings, and persists between scenes
 * for loading the correct scene after dying / losing on a level.
 */
public class ProgressTracker : MonoBehaviour {

    public static ProgressTracker instance;
    public int currentLevel = 0;

    /**
     * Ensures singleton design pattern integrity, and object persistence between scenes.
     */ 
    private void Awake()
    {
        if (instance != null)
            DestroyImmediate(gameObject);

        instance = this;
        DontDestroyOnLoad(this);
    }
}
