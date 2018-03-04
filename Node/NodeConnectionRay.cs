using UnityEngine;

/* Created by Glen McManus January 28, 2018
 * Last edited by Glen McManus March 3, 2018
 */ 

/*
 * NodeConnectionRay tries to connect two ShootableNodes by shooting a rigidbody2D from node to targetNode
 * (because there were collision detection issues with just casting a ray, and this provides a visualization
 * to the player / for debugging)
 */ 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class NodeConnectionRay : MonoBehaviour {
    
    public float speed = 3;
    public Rigidbody2D rb;
    private ShootableNode node;
    private ShootableNode targetNode;
    private Vector3 direction;

    /**
     * Shoots a rigidbody2D between nodes to attempt making a bridge.
     */ 
    public void TryConnection(ShootableNode node, ShootableNode targetNode, Vector3 direction)
    {
        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
        this.node = node;
        this.direction = direction;
        this.targetNode = targetNode;
    }

    /**
     * Makes a bridge if the rigidbody2D is unobstructed along the path.
     */ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(targetNode != null && collision.gameObject == targetNode.gameObject)
        {
            if (Player.instance.selectedNode != null)
                Player.instance.selectedNode = null;

            node.MakeBridgeConnection(direction, targetNode);
            node.Activate();
            targetNode.Activate();

            gameObject.SetActive(false);
        }
    }

    /**
     * Deactivates nodes if the path between them is obstructed.
     */ 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (node != null)
            node.Deactivate();

        if (targetNode != null)
            targetNode.Deactivate();

        if (Player.instance.selectedNode != null)
            Player.instance.selectedNode = null;

        gameObject.SetActive(false);
    }

}
