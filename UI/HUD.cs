using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* Created by Glen McManus January 28, 2018
 */

/*
 * HUD displays player state information on the heads up display canvas.
 */ 
public class HUD : MonoBehaviour {

    public Image currentLaserType;
    public Text currentLaserName;
    public GameObject notificationPanel;
    public Text notificationText;

    public GameObject hpBar;
    public Image hpFill;
    public float hpDisplayDuration = 3f;

    private void Awake()
    {
        notificationPanel.SetActive(false);
    }

    /**
     * Adjusts ProjectileType label to match the player's weapon mode.
     * @param projectileType    The mode which the weapon is in.
     */ 
    public void SetCurrentLaserText(ProjectileType projectileType)
    {
        switch (projectileType)
        {
            case ProjectileType.damaging:
                currentLaserName.text = "Lethal";
                break;


            case ProjectileType.platformMaker:
                currentLaserName.text = "Platform Maker";
                break;


            case ProjectileType.platformBreaker:
                currentLaserName.text = "Platform Breaker";
                break;

            case ProjectileType.data:
                currentLaserName.text = "Data";
                break;


            default:
                currentLaserName.text = "default";
                break;

        }       
    }

    /**
     * Changes the colour of the laser type displayed beside the ProjectileType label
     * to match the colour of the projectile fired by the current weapon mode.
     */ 
    public void SetLaserDisplay(Color colour)
    {
        currentLaserType.color = colour;
    }

    /**
     * Shows a message in the bottom center of the screen.
     * @param text  The message
     */ 
    public void DisplayNotification(string text)
    {
        notificationText.text = text;
        notificationPanel.SetActive(true);
    }

    /**
     * Shows the player's current HP, and starts a timer that disables the alert after the duration
     */ 
    public void DisplayHP()
    {
        hpFill.fillAmount = Player.instance.hitPoints / 3.0f;

        StartCoroutine(ShowHP());
    }

    /**
     * Disables the hp display after the duration of the timer.
     */ 
    IEnumerator ShowHP()
    {
        hpBar.SetActive(true);

        float timer = Time.time + hpDisplayDuration;
        while(timer > Time.time)
        {
            yield return null;
        }

        hpBar.SetActive(false);
        yield return null;
    }
}
