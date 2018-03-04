using UnityEngine;

/* Created by Glen McManus January 28, 2018
 */

/*
 * Boulder provides logic for falling objects that damage the player.
 */
public class Boulder : MonoBehaviour {
    public int damage = 1;
	private bool boulderState;
	private float speed = 1.5f;
	private float maxSpeed = 1.5f;
	public Rigidbody2D boulder;

	// Use this for initialization
	void Start () {
		boulderState = false;
		boulder = GetComponent<Rigidbody2D>();
	}
	
	/**
     * Drops boulders at the player if they are in range.
     * Provides terminal velocity to the boulder.
     */ 
	void Update () {
		if(Player.instance.transform.position.x > 12.5){
			boulderState = true;
		}

		if(boulderState){
			boulder.gravityScale = 1;
		}
		if(boulder.velocity.x < -maxSpeed){
			boulder.velocity = new Vector2(-maxSpeed, boulder.velocity.y);
		}
	}

    /**
     * Damages the player on collision
     */ 
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.collider.CompareTag("Player"))
        {
            Player.instance.TakeDamage(damage);
        }
	}
}
