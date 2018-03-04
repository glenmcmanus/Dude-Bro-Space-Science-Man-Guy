using UnityEngine;

/* Created by Glen McManus January 28, 2018
 */

/*
 * DeathHole kills the player when they fall down a hole.
 */
[RequireComponent(typeof(BoxCollider2D))]
public class DeathHole : MonoBehaviour {

    /**
    * Kills player
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Player.instance.Lose();
    }
}
