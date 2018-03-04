using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * InputConfig is referenced by any (non-UI) script taking player input. Each key could
 * be mapped by the player's preference.
 */ 
[System.Serializable]
public class InputConfig {

    [Header("Movement keys")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.W;
    public KeyCode crouch = KeyCode.S;

    [Header("Function keys")]
    public KeyCode toggleLaser = KeyCode.LeftShift;
    public KeyCode menu = KeyCode.Escape;
    public KeyCode confirm = KeyCode.Return;

    [Header("Action keys")]
    public KeyCode firePrimaryProjectile = KeyCode.Mouse0;
    public KeyCode fireSecondaryProjectile = KeyCode.Mouse1;

    /**
     * Creates config using the standard control scheme.
     */ 
    public InputConfig()
    {
        //Movement
        left = KeyCode.A;
        right = KeyCode.D;
        jump = KeyCode.W;
        crouch = KeyCode.S;

        //Function
        toggleLaser = KeyCode.LeftShift;
        menu = KeyCode.Escape;
        confirm = KeyCode.Return;

        //Action
        firePrimaryProjectile = KeyCode.Mouse0;
        fireSecondaryProjectile = KeyCode.Mouse1;
    }

}