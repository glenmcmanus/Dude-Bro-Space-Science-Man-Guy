using System.Collections;
using UnityEngine;

/*Created by Glen McManus January 28, 2018
 */

/*
 * Turret is a stationary enemy which aims at players and shoots
 */ 
public class Turret : MonoBehaviour {

    private bool turretOnline = false;
    private bool firing = false;

    public float fireDelay = 1;
    private WaitForSeconds delay;

    public Transform barrel;
    public Transform gun;
    public ObjectPool laserPool;

    //Initialize values
    private void Awake()
    {
        delay = new WaitForSeconds(fireDelay);
    }

    /**
     * Aiming logic. Ceases firing when the barrel is "destroyed", and deactivates this gameobject.
     */
    void Update()
    {
        if (turretOnline == true)
        {
            gun.right = (gun.position - Player.instance.transform.position);
        }

        if (barrel.parent.gameObject.activeSelf == false)
        {
            StopCoroutine(Fire());
            gameObject.SetActive(false);
        }
    }

    /**
     * Enables aiming and shooting logic when the player enters radius
     */ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && turretOnline == false)
        {
            turretOnline = true;
            Activate();
        }
    }

    /**
     * Disables aiming and shooting logic when the player exits radius
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && turretOnline == true)
        {
            turretOnline = false;
            Deactivate();
        }
    }

    /**
     * Enables aiming and shooting logic
     */
    private void Activate()
    {
        if (firing == false)
        {
            firing = true;
            StartCoroutine(Fire());
        }
    }

    /**
     * Disables aiming and shooting logic
     */
    private void Deactivate()
    {
        StopCoroutine(Fire());
        firing = false;
    }

    /**
     * Shoots a projectile at the player with a delay between attacks
     */
    IEnumerator Fire()
    {
        float timer = Time.time + fireDelay;
        while(turretOnline == true)
        {
            while(timer > Time.time)
            {
                yield return null;
            }

            Debug.Log("firing");
            GameObject laser = laserPool.ActivateObject(barrel.position);
            laser.GetComponent<EnemyProjectile>().Fire(-gun.right);

            timer = Time.time + fireDelay;

            yield return null;
        }

        yield return null;
    }
}
