using System.Collections;
using UnityEngine;

/* Created by Glen McManus January 26, 2018
 */

/*
 * Projectile handles logic for the motion, colour, and life duration of projectiles. Each projectile knows
 * how much damage it deals, and deactivates on collision.
 */
public class Projectile : MonoBehaviour {

    public float speed = 3f;
    public int damage = 1;
    public float life = 5f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Transform myTransform;
    public ProjectileType projectileType = ProjectileType.damaging;

    [Header("Type Colours")]
    public Color damagingColour = Color.red;
    public Color platformMakerColour = Color.green;
    public Color platformBreakerColour = Color.yellow;
    public Color dataColour = Color.cyan;

    /**
     * Caches transform
     */
    private void Awake()
    {
        if(myTransform == null)
            myTransform = transform;
    }

    /**
     * Sets the colour of the projectile based on the projectileType.
     */ 
    private void SetColour()
    {
        switch (projectileType)
        {
            case ProjectileType.damaging:
                spriteRenderer.color = damagingColour;
                break;

            case ProjectileType.platformMaker:
                spriteRenderer.color = platformMakerColour;
                break;

            case ProjectileType.platformBreaker:
                spriteRenderer.color = platformBreakerColour;
                break;

            default:
                spriteRenderer.color = Color.magenta;
                break;

        }
    }

    /**
     * Returns the colour of the current projectile type.
     */
    public Color GetColour(ProjectileType projectileType)
    {
        switch (projectileType)
        {
            case ProjectileType.damaging:
                return damagingColour;
                

            case ProjectileType.platformMaker:
                return platformMakerColour;
                

            case ProjectileType.platformBreaker:
                return platformBreakerColour;
                

            default:
                return Color.magenta;
                
        }
    }

    /**
     * Sets the colour of the projectile based on its type, adds force to the projectile's rigidbody2D,
     * and begins the timer for the projectile's life duration.
     */
    private void OnEnable()
    {
        SetColour();
        
        rb.AddForce(myTransform.right * speed, ForceMode2D.Impulse);
        StartCoroutine(lifeTimer());
    }

    /**
     * Returns the projectile to its pool, clearing current velocity.
     */
    private void Disable()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    /**
     * Disables the projectile after duration.
     */
    IEnumerator lifeTimer()
    {
        float start = Time.time;
        while(Time.time - start < life)
        {
            yield return null;
        }

        Disable();
        yield return null;
    }

    /**
     * Returns projectile to pool on collision.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Disable();
    }

}

public enum ProjectileType
{
    platformMaker,
    platformBreaker,
    data,
    damaging,
    Length
}
