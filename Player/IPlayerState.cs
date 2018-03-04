/* Created by Glen McManus January 27, 2018 
 */ 

/* IPlayerState provides a framework for a finite state machine.
 * Because of time constraints, the state machine was not implemented.
 */ 
public interface IPlayerState {

    //Transitions
    void ToIdle();
    void ToMove();
    void ToStun();
    void ToShoot();
    void ToFall();

    //Core methods
    void Update();
    void FixedUpdate();
    void Enter();
    void Exit();

}
