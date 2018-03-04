using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * NodeLinks holds a reference to a pool of projectiles which shoot from the selected platformMaker node to the
 * target platformMaker node when trying to create a bridge between them.
 */ 
public class NodeLinks : MonoBehaviour {

    public static NodeLinks instance;

    public GameObject platformPrefab;
    public float platformHeight;

    public ObjectPool connectionRayPool;

    /**
     * Initializes platform height for easy reference, ensures singleton integrity.
     */ 
    private void Awake()
    {
        if (instance != null)
            DestroyImmediate(this);

        instance = this;

        platformHeight = platformPrefab.GetComponent<SpriteRenderer>().size.y;

    }

}
