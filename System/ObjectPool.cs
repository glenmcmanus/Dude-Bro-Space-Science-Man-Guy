using System.Collections.Generic;
using UnityEngine;

/*Created by Glen McManus January 27, 2018
 */

/*
 * ObjectPool instantiates a bunch of objects when initialized, and keeps track of them
 * so that they may be disabled, and reused when necessary. Improves performance by reducing
 * garbage collection.
 */ 
public class ObjectPool : MonoBehaviour {
    
    public int poolSize = 10;
    public GameObject objectPrefab;
    public List<GameObject> objects = new List<GameObject>();
    public bool canGrow = false;

    /**
     * Initialize values
     */ 
    void Start()
    {
        for (int i = 0; i <= poolSize; i++)
        {
            objects.Add(Instantiate(objectPrefab));
            objects[i].gameObject.transform.SetParent(transform);
            objects[i].gameObject.SetActive(false);
        }
    }

    /**
     * Tries to enable an object from the pool at the given position.
     * @param position  where the object will be placed
     * @return          the object just activated. Null if none are available.
     */
    public GameObject ActivateObject(Vector3 position)
    {
        GameObject go = getInactive();

        if (go == null)
            return null;

        go.gameObject.SetActive(true);
        go.transform.position = position;

        return go;
    }

    /**
     * Looks for an inactive object in the pool.
     * @return  the first inactive object found.
     */
    private GameObject getInactive()
    {
        GameObject go = null;

        foreach (GameObject g in objects)
        {
            if (g.gameObject.activeSelf == false)
            {
                go = g;
                break;
            }
        }

        if (go == null && canGrow == true)
        {
            go = IncreasePool();
        }
        else if (go == null)
        {
            return null;
        }

        return go;
    }

    /**
     * Adds an object to the pool all objects in the pool are active, and more inactive objects are sought.
     * @return  The new object added to the pool.
     */ 
    public GameObject IncreasePool()
    {
        objects.Add(Instantiate(objectPrefab));
        objects[objects.Count - 1].transform.SetParent(transform);
        return objects[objects.Count - 1];
    }
}
