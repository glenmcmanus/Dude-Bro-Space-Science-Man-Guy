using System.Collections;
using UnityEngine;

/* Created by Glen McManus January 26, 2018
 * Last edited by Glen McManus March 3. 2018
 */ 

/*
 * ShootableNodes can increment progress toward a goal state (data nodes)
 * or be connected to make bridges (by being shot with the platformMaker laser)
 */ 
[RequireComponent(typeof(SpriteRenderer))]
public class ShootableNode : MonoBehaviour {

    //for data nodes only
    public Goal dataGoal;

    //for connector nodes only
    public GameObject platformConnectionRay;

    public bool inUse;
    public ProjectileType nodeType = ProjectileType.platformMaker;

    public SpriteRenderer spriteRenderer;
    public Color activeColour;
    public Color inactiveColour;
    public float inactiveGreyPercent = 0.8f;
    public float colourLerpRate = 0.01f;

    /**
     * Initialize values, depending on type of node
     */ 
    private void Start()
    {
        activeColour = Player.instance.projectilePool.projectiles[0].GetColour(nodeType);
        inactiveColour = Color.Lerp(activeColour, Color.grey, inactiveGreyPercent);

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = inactiveColour;

        if (nodeType == ProjectileType.data && dataGoal == null)
        {
            dataGoal = FindObjectOfType<Goal>();
        }
    }

    /**
     * Flags the node as "inUse" and starts the coroutine to make the colour on the node flash on/off
     */ 
    public void Select()
    {
        inUse = true;
        StartCoroutine(Selected());
    }

    /**
     * Makes the node colour flash on/off effect by lerping colour between active / inactive values
     */ 
    public IEnumerator Selected()
    {
        float t = colourLerpRate;
        bool increasing = true;
        while(inUse)
        {
            Debug.Log("running colour lerper");
            spriteRenderer.color =  Color.Lerp(inactiveColour, activeColour, t);

            if(t == 1)
            {
                increasing = false;
            }
            else if(t == 0)
            {
                increasing = true;
            }

            t = increasing == true ? t + colourLerpRate : t - colourLerpRate;

            if(t > 1)
            {
                t = 1;
            }
            else if(t < 0)
            {
                t = 0;
            }
            
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    /**
     * Sets the node to active (either an enabled data node, or a node connected to another via a bridge)
     */ 
    public void Activate()
    {
        StopAllCoroutines();
        spriteRenderer.color = activeColour;
    }

    /**
     * Sets the node to an inactive state.
     */ 
    public void Deactivate()
    {
        inUse = false;
        spriteRenderer.color = inactiveColour;
    }

    /**
     * Activates the node if shot with the proper laser. If a platformMaker node, sets the node to the player's most recently shot,
     * to be paired with the next node shot. If the path between platform nodes is obscured, they are both deactivated.
     */ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>() == null)
            return;

        Projectile p = collision.GetComponent<Projectile>();
        if (p.projectileType != ProjectileType.platformMaker && nodeType == ProjectileType.platformMaker)
            return;

        if (inUse)
        {
            return;
        }

        if (nodeType == ProjectileType.platformMaker)
        {
            Select();

            if (Player.instance.selectedNode == null || Player.instance.selectedNode.nodeType != nodeType)
            {
                Player.instance.selectedNode = this;
            }
            else if (Player.instance.selectedNode != this)
            {
                Vector3 direction = Player.instance.selectedNode.transform.position - transform.position;
                float distance = Vector2.Distance(Player.instance.selectedNode.transform.position, transform.position);

                GameObject ray = NodeLinks.instance.connectionRayPool.ActivateObject(transform.position);
                ray.GetComponent<NodeConnectionRay>().TryConnection(this, Player.instance.selectedNode, direction);
                ray.transform.up = -direction;
            }
        }
        else if(nodeType == ProjectileType.data && p.projectileType == ProjectileType.data && inUse == false)
        {
            inUse = true;
            Activate();
            dataGoal.ActivateNode();
        }
    }

    /**
     * Creates a bridge between two platformNodes
     * @param offset    the midway point between the nodes
     * @param targetNode    the node which the player's "selected node" connects with
     */ 
    public void MakeBridgeConnection(Vector3 offset, ShootableNode targetNode)
    {
        GameObject go = Instantiate(NodeLinks.instance.platformPrefab, transform.position + (offset * 0.5f),
                                Quaternion.identity);

        go.transform.right = offset;

        NodeBridge nodeBridge = go.GetComponent<NodeBridge>();
        float xSize = Vector3.Distance(transform.position, targetNode.transform.position);
        nodeBridge.spriteRenderer.size = new Vector2(xSize, nodeBridge.spriteRenderer.size.y);
        nodeBridge.boxCollider.size = nodeBridge.spriteRenderer.size;

        nodeBridge.parentNodes[0] = this;
        nodeBridge.parentNodes[1] = targetNode;

        inUse = true;
        targetNode.inUse = true;

        Player.instance.selectedNode = null;
    }

}
