using UnityEngine;

/*Created by Glen McManus January 27, 2018 
 */ 

/*
 * OnesZeroes draws ones and zeroes on the screen as the player warps out of a level from the goal.
 */ 
public class OnesZeroes : MonoBehaviour {

    public TextMesh textMesh;
    public Rigidbody2D rb;
    public float speed = 1f;

    void OnEnable()
    {
        textMesh.text = Random.Range(0, 2).ToString();
    }

}
