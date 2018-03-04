using System.Collections;
using UnityEngine;

/* Created by Glen McManus January 28, 2018
 * Last edited March 2, 2018
 */ 

/* 
 * Goal handles the logic for how a player completes a level.
*/ 
[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour {

    public int activeNodes = 0;
    private bool transmitting = false;
    public bool inProximity = false;
    public ShootableNode[] dataNodes;
    public ParticleSystem progressParticles;
    public ParticleSystem transmissionParticles;
    public HazaardSpawner onesAndZeroesSpawner;
    public BoxCollider2D boxCollider2D;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip progressSound;
    public AudioClip completesound;
    float pitchStep;
    float minPitch = 0.28f;
    float maxPitch = 0.58f;

    /**
     * Initialize values
     */ 
    private void Awake()
    {
        boxCollider2D.isTrigger = true;
        pitchStep = (maxPitch - minPitch) / 3;
    }

    /**
     * Steps toward the goal state each time a data node is activated.
     */
    public void ActivateNode()
    {
        if (audioSource != null && audioSource.isPlaying == false)
            audioSource.Play();

        if (progressParticles.isPlaying == false)
            progressParticles.Play();

        activeNodes++;

        if (activeNodes == dataNodes.Length && transmitting == false)
            EnableTransmission();

        if(activeNodes >= dataNodes.Length - 3 && activeNodes < dataNodes.Length)
        {
            audioSource.pitch = minPitch + (dataNodes.Length - activeNodes) * pitchStep;

        } else if(activeNodes == dataNodes.Length)
        {
            audioSource.pitch = maxPitch;
        }
     
    }

    /**
     * Allows the level to be completed once all data nodes are enabled.
     */
    public void EnableTransmission()
    {
        transmitting = true;
        StartCoroutine(Transmission());

        transmissionParticles.Play();
        progressParticles.Stop();
    }

    /**
     * Sets proximity condition true for completing the level.
     * Prompts the player to push a button to finish the level
     * if the goal state has been reached.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inProximity = true;

            if (transmitting == true)
            {
                Player.instance.hud.DisplayNotification("Press \""
                                                        + Player.instance.inputConfig.confirm.ToString()
                                                        + "\" to transmit.");
            }
        }
    }

    /**
     * Sets proximity condition false for completing the level.
     * Disables the prompt to the player.
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inProximity = false;

            if (transmitting == true)
            {
                Player.instance.hud.notificationPanel.SetActive(false);
            }
        }
    }

    /**
     * Loops goal state effects and waits for user input to end the level.
     */ 
    IEnumerator Transmission()
    {
        while(transmitting)
        {
            if (inProximity == true && Input.GetKeyUp(Player.instance.inputConfig.confirm))
            {
                audioSource.Stop();
                audioSource.clip = completesound;
                audioSource.Play();

                Player.instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                Player.instance.mainCamera.maximumHeight = GetComponentInChildren<HazaardEndPoint>().transform.position.y;
                Player.instance.TransmitToNextLevel();
                onesAndZeroesSpawner.enabled = true;
            }

            yield return null;
        }
    }
               
}
