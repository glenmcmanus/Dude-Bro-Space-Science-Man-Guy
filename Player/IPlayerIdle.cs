using UnityEngine;

/* Created by Glen McManus January 27, 2018
 */ 

/*
 * IPlayerIdle is the idle state for the player 
 * (when no input is entered, and no external forces are acting on the player).
 * Because of time constraints, the state machine was not implemented.
 */ 
public class IPlayerIdle : ScriptableObject, IPlayerState  {

    Player player;

    //Transitions
    public void ToIdle() {
        //player.currentState = player.idleState;
    }

    public void ToMove()
    {

    }
    public void ToStun()
    {

    }
    public void ToShoot()
    {

    }
    public void ToFall()
    {

    }

    //Core methods
    public void Update()
    {

    }
    public void FixedUpdate()
    {

    }
    public void Enter()
    {

    }
    public void Exit()
    {

    }

}
