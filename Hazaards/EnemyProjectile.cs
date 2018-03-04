using System.Collections;
using UnityEngine;

/* Created by Glen McManus January 28, 2018
 */
 
/*
 * EnemyProjectile handles the projectile's movement, and deactivation (on collision / after duration).
 */ 
public class EnemyProjectile : MonoBehaviour {

    public float speed = 3f;
    public int damage = 1;
    public float life = 5f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    /**
     * Adds force to the rigidbody using the vector along which the enemy is aiming.
     */ 
    public void Fire(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
        StartCoroutine(lifeTimer());
    }

    /**
     * Sets gameobject inactive
     */ 
    private void Disable()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    /**
     * Deactivates gameobject after duration
     */ 
    IEnumerator lifeTimer()
    {
        float start = Time.time;
        while (Time.time - start < life)
        {
            yield return null;
        }

        Disable();
        yield return null;
    }

    /**
     * Damages player on collision; disables on collision
     */ 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player.instance.TakeDamage(damage);
        }

        Disable();
    }
}
