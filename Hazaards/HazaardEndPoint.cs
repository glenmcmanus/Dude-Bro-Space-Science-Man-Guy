using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * HazaardEndPoint disables falling hazaards when they enter the trigger
 * attached to this gameobject, returning the hazaard gameobject to its object pool.
 */ 
[RequireComponent(typeof(BoxCollider2D))]
public class HazaardEndPoint : MonoBehaviour {

    /**
     * Ensures boxcollider2D is set to a trigger
     */ 
    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    /**
     * Deactivates hazaard gameobjects that enter the trigger.
     */ 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Hazaard")) {
            collider.gameObject.SetActive(false);
        }
    }
	
}
