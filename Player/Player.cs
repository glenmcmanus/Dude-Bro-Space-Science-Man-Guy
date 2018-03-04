using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Created by Glen McManus January 27, 2018
 * Last edited March 2, 2018
 */ 

/*
 * Player holds references to all aspects of the player's avatar; 
 * handles taking damage, aiming, shooting, and goal / loss states.
 */ 
public class Player : MonoBehaviour {

    public int hitPoints = 3;
    public float recoilTime = 1f;
    public bool recoiling = false;

    [Header("Player State")]
    public static Player instance;
    public IPlayerState currentState;
    [HideInInspector] public IPlayerIdle idleState;
    public InputConfig inputConfig = new InputConfig();
    

    [Header("References")]
    public ShootableNode selectedNode;
    public ProjectilePool projectilePool;
    public ProjectileType projectileType = ProjectileType.platformMaker;
    public Transform gunMuzzle;
    public CameraFollow mainCamera;
    public HUD hud;
    public Transform arm;
    
    [Header("Sprites")]
    public Sprite regularGunArm;
    public Sprite recoilGunArm;
    public SpriteRenderer gunArm;
    public AnimateSprite spriteAnimator;
    public Vector3 shoulderMountLeft;
    public Vector3 shoulderMountRight;

    [Header("Colours")]
    public Color normalTint = Color.white;
    public Color transitionStartColour = Color.cyan;
    public Color transitionEndColour = new Color(0, 1, 1, 0);
    public float transitionColourChangeRate = 0.01f;
    public float transitionDuration = 3f;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip projectileSound;

    [Header("Collider")]
    public BoxCollider2D boxCollider2D;
    public Vector2 leftFacingBoxOffset;
    public Vector2 rightFacingBoxOffset;

    /**
     * Initializes singleton
     */ 
    void Awake()
    {
        if (instance != null)
            DestroyImmediate(this);

        instance = this;

        currentState = idleState;
    }

    /**
     * Sets current laser values in the HUD
     */
    private void Start()
    {
        hud.SetLaserDisplay(projectilePool.projectiles[0].GetColour(projectileType));
        hud.SetCurrentLaserText(projectileType);
    }

    /**
     * Handles aiming, shooting, and projectileType selection
     */
    void Update () {
        arm.right = Input.mousePosition - mainCamera.halfScreenSize;

        if(Input.GetKeyUp(inputConfig.firePrimaryProjectile) && recoiling == false)
        {
            if (projectileSound != null && audioSource != null)
            {
               
                audioSource.Play();
            }

            recoiling = true;
            projectilePool.ActivateProjectile(gunMuzzle.position, arm.right);
            StartCoroutine(Recoil());
        }

        if(Input.GetKeyUp(inputConfig.toggleLaser))
        {
            projectileType += 1;

            if (projectileType == ProjectileType.Length)
                projectileType = 0;

            hud.SetLaserDisplay(projectilePool.projectiles[0].GetColour(projectileType));
            hud.SetCurrentLaserText(projectileType);
        }
	}

    /**
     * Delay and recoil animation after shooting
     */
    IEnumerator Recoil()
    {
        gunArm.sprite = recoilGunArm;

        float timer = Time.time + recoilTime;

        while(timer > Time.time)
        {
            yield return null;
        }

        gunArm.sprite = regularGunArm;
        recoiling = false;
        yield return null;
    }

    /**
     * Starts the transition to the next level
     */
    public void TransmitToNextLevel()
    {
        recoiling = true;
        GetComponent<AAPM>().rb.gravityScale = -.1f;

        StartCoroutine(TransmissionTransition());
    }

    /**
     * Transitions player tint over time, then loads the next level
     */
    IEnumerator TransmissionTransition()
    {
        float timer = transitionDuration + Time.time;
        float currLerp = 0;
        while (timer > Time.time) {
            StandardShaderUtils.ChangeRenderMode(spriteAnimator._renderer.material, StandardShaderUtils.BlendMode.Fade);
            spriteAnimator._renderer.material.color = Color.Lerp(transitionStartColour, transitionEndColour, currLerp);
            gunArm.color = Color.Lerp(transitionStartColour, transitionEndColour, currLerp);

            currLerp += transitionColourChangeRate;
            yield return new WaitForEndOfFrame();
        }
        
        ProgressTracker.instance.currentLevel++;
        SceneManager.LoadScene(ProgressTracker.instance.currentLevel, LoadSceneMode.Single);

        StandardShaderUtils.ChangeRenderMode(spriteAnimator._renderer.material, StandardShaderUtils.BlendMode.Cutout);

        yield return null;
    }

    /**
     * Subtracts damage from health, calls the HUD to display current HP briefly.
     * @param damage    damage sustained
     */ 
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        hud.DisplayHP();

        if (hitPoints <= 0)
            Lose();
    }

    /**
     * Loads the lose screen
     */
    public void Lose() {
        SceneManager.LoadScene("LoseScreen", LoadSceneMode.Single);
    }
}
