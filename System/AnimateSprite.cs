using UnityEngine;
using System.Collections;

/* Adapted from http://wiki.unity3d.com/index.php/Animating_Tiled_texture
 * which is liscensed under https://creativecommons.org/licenses/by-sa/3.0/
 * by Glen McManus January 28, 2018
 */

/* Animate sprite loops through a sprite sheet by uniform UV cells.
 * Called by the movement script of the object that is animated.
 */ 
public class AnimateSprite : MonoBehaviour
{
    public int columns = 2;
    public int rows = 2;
    private Vector2 size;
    private Vector2 offset;

    public float framesPerSecond = 10f;
    private WaitForSeconds frameWait;

    public Vector3 spriteScaleRight;
    public Vector3 spriteScaleLeft;

    public MeshRenderer _renderer;
    public Texture spriteSheet;
    public bool moving;

    private int index = 0; //the current frame to display

    /**
     * Initializes values
     */
    private void Awake()
    {
        if (_renderer == null)
            _renderer = GetComponent<MeshRenderer>();

        spriteScaleRight = transform.localScale;
        spriteScaleLeft = new Vector3(-1 * spriteScaleRight.x,
                                      spriteScaleRight.y,
                                      spriteScaleRight.z);

        frameWait = new WaitForSeconds(1 / framesPerSecond);

        //set the tile size of the texture (in UV units), based on the rows and columns
        size = new Vector2(1f / columns, 1f / rows);
        _renderer.sharedMaterial.SetTextureScale("_MainTex", size);
    }

    /**
     * Starts the walk cycle animation loop if the player is moving
     * Assigns the walk cycle sprite sheet to the player
     */
    public void Move()
    {
        if (moving == true)
            return;

        Animate();
    }

    /**
     * Stops animation when the player is not moving
     * Assigns the idle sprite to the player
     */
    public void Idle()
    {
        StopAnimation();

        _renderer.sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);     
    }

    /**
     * Starts the coroutine that loops the walk cycle animation
     */
    void Animate()
    {
        if (moving == true || IsInvoking("updateTiling") )
            return;

        StartCoroutine(updateTiling());
    }

    /**
     * Ends the coroutine that loops the walkcycle
     */ 
    void StopAnimation()
    {
        StopAllCoroutines();
        moving = false;
    }

    /**
     * Loops the spritesheet walk cycle by incrementing UV coordinates,
     * or looping back to the first cell after the final frame.
     */ 
    private IEnumerator updateTiling()
    {
        moving = true;
        while (moving == true)
        {
            Debug.Log("updatingTiling");
            // yield return new WaitForFixedUpdate();

            //move to the next index
            index++;

            if (index >= rows * columns)
                index = 1;

            int indexByColumns = index / columns;

            offset = new Vector2((float)index / columns - indexByColumns,
                                 indexByColumns / (float)rows);

            _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);

            yield return frameWait;
        }

        moving = false;
    }
}