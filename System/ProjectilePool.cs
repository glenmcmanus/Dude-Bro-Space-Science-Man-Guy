using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Created by Glen McManus January 27, 2018
 */

/*
 * ProjectilePool instantiates a bunch of projectiles when initialized, and keeps track of them
 * so that they may be disabled, and reused when necessary. Improves performance by reducing
 * garbage collection.
 */
public class ProjectilePool : MonoBehaviour {

    public int poolSize = 10;
    public GameObject projectilePrefab;
    public List<Projectile> projectiles = new List<Projectile>();
    public bool canGrow = false;

    /**
     * Sets up initial pool
     */ 
	void Awake () {
		for(int i = 0; i <= poolSize; i++)
        {
            projectiles.Add( Instantiate(projectilePrefab).GetComponent<Projectile>() );
            projectiles[i].gameObject.transform.SetParent(transform);
            projectiles[i].gameObject.SetActive(false);
        }
	}

    /**
     * Enables an inactive projectile at given position, and sets the right vector for
     * proper orientation.
     * @param position  The position the projectile is moved to once enabled.
     * @param right     The direction of the right vector of the projectile's transform.
     */ 
    public void ActivateProjectile(Vector3 position, Vector3 right)
    {
        Projectile projectile = getInactive();

        if (projectile == null)
            return;

        projectile.projectileType = Player.instance.projectileType;
        projectile.myTransform.right = right;
        projectile.gameObject.SetActive(true);
        projectile.rb.position = position;

    }
	
    /**
     * Tries to get an inactive projectile from the pool. Adds a new projectile to the pool if all are currently
     * active, and the pool is allowed to grow.
     * @return  The first inactive projectile discovered (or the new one created if necessary / possible).
     *          Otherwise, null.
     */ 
	private Projectile getInactive()
    {
        Projectile projectile = null;
        
        foreach(Projectile p in projectiles)
        {
            if(p.gameObject.activeSelf == false)
            {
                projectile = p;
                break;
            }
        }

        if(projectile == null && canGrow == true)
        {
            projectile = IncreasePool();
        } else if(projectile == null)
        {
            return null;
        }

        return projectile;
    }

    /**
     * Adds a new projectile to the pool.
     * @return  The projectile added to the pool.
     */ 
    public Projectile IncreasePool()
    {
        projectiles.Add(Instantiate(projectilePrefab).GetComponent<Projectile>());
        projectiles[projectiles.Count - 1].transform.SetParent(transform);
        return projectiles[projectiles.Count - 1];
    }
}
