using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * NodeBridge connects two platformMaker ShootableNodes with a traversable bridge.
 */ 
public class NodeBridge : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public ShootableNode[] parentNodes = new ShootableNode[2];

    /**
     * Listens for collisions with a platformBreaker projectile. If the bridge collides with a platformBreaker, the bridge is destroyed.
     */ 
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.GetComponent<Projectile>() == null)
        {
            return;
        }

        if(collision.rigidbody.GetComponent<Projectile>().projectileType == ProjectileType.platformBreaker)
        {
            parentNodes[0].Deactivate();
            parentNodes[1].Deactivate();

            Destroy(gameObject);
        }
    }
}
