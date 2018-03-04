using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * DeactivateAfterTime disables a gameobject after some duration.
 * The timer is (re)set when the gameobject is enabled.
 */ 
public class DeactivateAfterTime : MonoBehaviour {

    public float duration = 1f;
    public WaitForSeconds waitTime;

    /**
     * Initialize values
     */
    private void Awake()
    {
        waitTime = new WaitForSeconds(duration);
    }

    /**
     * Starts timer
     */ 
    private void OnEnable()
    {
        StartCoroutine(timer());
    }

    /**
     * Deactivates gameobject after duration.
     */ 
    IEnumerator timer()
    {
        int i = 1;
        do
        {
            i--;
            yield return waitTime;
        } while (i > 0);

        gameObject.SetActive(false);
        yield return null;
    }
}
