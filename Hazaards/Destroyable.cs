using System.Collections;
using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */

/* Destroyable disables objects (returning them to the available state in the object pool)
 * when they take enough damage from a projectile to be destroyed.
 */
public class Destroyable : MonoBehaviour {

    public bool hasHP = false;
    public int hitPoints = 0;
    public ParticleSystem particleSystem;
    public SpriteRenderer spriteRenderer;

    /**
     * Ensures the sprite is rendered when the gameobject is enabled.
     */ 
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    /**
     * Calls Destroyed() when this object reaches 0hp by being damaged by a projectile
     * @param collision     The collision event
     */ 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Projectile>() != null)
        {
            Projectile p = collision.collider.GetComponent<Projectile>();
            if (p.projectileType != ProjectileType.damaging)
            {
                return;
            }

            if (hasHP == false || hitPoints - p.damage <= 0)
            {
                Destroyed();
                GetComponent<Collider2D>().enabled = false;
                
            }
            else
            {
                hitPoints -= p.damage;
            }
        }
    }

    /**
     * Deactivates this gameobject and, if present, plays an on-death particle effect.
     */ 
    void Destroyed()
    {
        if (particleSystem != null)
        {
            StartCoroutine(deactivateAfterParticleEffect());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /**
     * Plays on-death particle effect and deactivates this gameobject
     * after the duration of the effect.
     */ 
    IEnumerator deactivateAfterParticleEffect()
    {
        spriteRenderer.enabled = false;
        particleSystem.Play();

        while(particleSystem.isPlaying)
        {
            yield return null;
        }

        gameObject.SetActive(false);
        yield return null;
    }
}
