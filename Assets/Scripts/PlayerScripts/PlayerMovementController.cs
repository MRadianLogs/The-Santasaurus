using UnityEngine;

/// <summary>
/// This script is responsible for getting input and moving the player, including changing the direction they are facing.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController instance;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Rigidbody2D playerRigidBody = null;
    private Vector2 movementDirection;
    private int facingDirection;
    [SerializeField] private PlayerInputController inputController = null;

    [SerializeField] private Animator playerAnimator = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists! Destroying object!");
            Destroy(transform.root.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (inputController != null)
        {
            movementDirection.x = inputController.HorizontalInput;
            movementDirection.y = inputController.VerticalInput;

            //Change rotation. The way the sprite should be facing. 
            //TODO: Add diagnal directions AND/OR fix priority directions(Up/down).
            if (movementDirection.x > 0)
            {
                facingDirection = 0;//Facing right. 
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (movementDirection.x < 0)
            {
                facingDirection = 2;//Facing left.
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (movementDirection.y > 0)
            {
                facingDirection = 1;//Facing up.
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movementDirection.y < 0)
            {
                facingDirection = 3;//Facing down.
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            
            playerAnimator.SetFloat("speed", movementDirection.sqrMagnitude);
            Debug.Log("Player speed: " + GetPlayerCurrentMovementSpeed());
        }
    }

    private void FixedUpdate()
    {
        if (inputController != null)
        {
           playerRigidBody.MovePosition(playerRigidBody.position + movementDirection * movementSpeed * Time.fixedDeltaTime);

        }
    }

    public float GetPlayerCurrentMovementSpeed()
    {
        return movementDirection.sqrMagnitude;
    }
}
