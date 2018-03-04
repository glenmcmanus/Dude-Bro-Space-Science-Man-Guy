using System.Collections;
using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * HazaardSpawner spawns hazaard gameobjects from an object pool
 * at regular intervals at a preset position (with the option for a bit
 * of random offsetting)
 */ 
public class HazaardSpawner : MonoBehaviour {

    public bool randomSpawnOffset = false;
    public Vector3 offsetRange = new Vector3(1, 1, 0);
    public float rate = 3f;
    public Transform startPosition;
    public ObjectPool objectPool;

    private WaitForSeconds waitTime;
    
    /**
     * Initialize values, start coroutines
     */ 
	void Start () {
        waitTime = new WaitForSeconds(rate);

        if (randomSpawnOffset == false)
        {
            StartCoroutine(Spawn());
        } else
        {
            StartCoroutine(SpawnRandomOffset());
        }
	}

    /**
     * Spawns hazaard objects at the preset position at regular intervals.
     */ 
    IEnumerator Spawn()
    {
        while(true)
        {
            objectPool.ActivateObject(startPosition.position);

            yield return waitTime;
        }
    }

    /**
     * Spawns hazaard objects at the preset position + a random offset at regular intervals.
     */
    IEnumerator SpawnRandomOffset()
    {
        Vector3 randomOffset;
        while (true)
        {
            randomOffset = new Vector3(Random.Range(-offsetRange.x, offsetRange.x), Random.Range(-offsetRange.y, offsetRange.y), 0);
            objectPool.ActivateObject(startPosition.position + randomOffset);

            yield return waitTime;
        }
    }
}
