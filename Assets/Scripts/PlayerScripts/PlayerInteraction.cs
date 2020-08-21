using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int moveSpeed;
    public Rigidbody2D playerRigidBody;
    Vector2 movementDirection;
    
    static GameObject itemInMouth = null;//The current item the player has picked up.
    bool mouthFull;//Whether the player is currently holding an item.

    static float cooldownTime = .5f;
    static float nextPickUpTime = 0;

    public Animator playerAnimator;
    int facingDirection; //0 = right. 1 = up. 2 = left. 3 = down.

    // Start is called before the first frame update
    void Start()
    {
        itemInMouth = null;
        cooldownTime = .5f;
        nextPickUpTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.getGameHasEnded())
        {
            //Check movementInputted.
            checkMovementInput();
            //Check if player is attempting to drop item.
            checkDroppingInput();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.getGameHasEnded())
        {
            playerRigidBody.MovePosition(playerRigidBody.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void checkMovementInput()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        //Change rotation. The way the sprite should be facing.
        if(movementDirection.x == 1)
        {
            facingDirection = 0;//Facing right. 
            transform.rotation = Quaternion.Euler(0,0,-90);
        }
        else if(movementDirection.x == -1)
        {
            facingDirection = 2;//Facing left.
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if(movementDirection.y == 1)
        {
            facingDirection = 1;//Facing up.
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(movementDirection.y == -1)
        {
            facingDirection = 3;//Facing down.
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        playerAnimator.SetFloat("speed", movementDirection.sqrMagnitude);
    }

    private void checkDroppingInput()
    {
        if (Time.time > nextPickUpTime)
        {
            if (Input.GetButtonDown("InteractButton") && (itemInMouth != null))
            {
                //Drop item in front.
                itemInMouth.transform.position = new Vector2(transform.position.x, transform.position.y);
                itemInMouth.SetActive(true);
                //Clear inventory.
                itemInMouth = null;
                //Set cooldown. Fixes bugs?
                nextPickUpTime = Time.time + cooldownTime;
            }
        }
    }

    public static void setItemInMouth(GameObject newItem)
    {
        //Add item to inventory slot.
        itemInMouth = newItem;
        //Deactivate item from screen.
        newItem.SetActive(false);
    }

    public static GameObject getItemInMouth()
    {
        return itemInMouth;
    }

    public static float getNextPickUpTime()
    {
        return nextPickUpTime;
    }

    public static void setNextPickUpTime(float nextTime)
    {
        nextPickUpTime = nextTime;
    }

    public static float getCoolDownTime()
    {
        return cooldownTime;
    }
}
