using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * RotateOverTime rotates an object every frame about a given axis, by preset degrees.
 */ 
public class RotateOverTime : MonoBehaviour {

    [Tooltip("Angle in degrees")]
    public float angleStep = 6f;
    public Vector3 axis = new Vector3(0, 0, 1);
    Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    /**
     * Rotates the object about given axis
     */ 
    void Update () {
        myTransform.Rotate(axis, angleStep);
	}
}
