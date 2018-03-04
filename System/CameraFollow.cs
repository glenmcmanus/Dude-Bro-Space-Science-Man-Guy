using UnityEngine;

/* Created by Glen McManus January 28, 2018
 */

/*
 * CameraFollow follows the player by a given offset, 
 * and is used to track the mouse position for player projectile vector logic.
 */
public class CameraFollow : MonoBehaviour {
    public float minimumHeight = 0f;
    public float maximumHeight = 0f;
    public Vector3 offset;
    public Vector3 halfScreenSize;
    

    public bool useInitialOffset = false;

    private void Awake()
    {
        halfScreenSize = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

    // Use this for initialization
    void Start () {
        if(useInitialOffset == true)
		    offset = transform.position - Player.instance.transform.position;
	}
	 
	void FixedUpdate () {
		transform.position = Player.instance.transform.position + offset;

        if (transform.position.y < minimumHeight)
            transform.position = new Vector3(transform.position.x, minimumHeight, transform.position.z);

        if(maximumHeight != 0 && transform.position.y > maximumHeight)
            transform.position = new Vector3(transform.position.x, maximumHeight, transform.position.z);
    }
}
