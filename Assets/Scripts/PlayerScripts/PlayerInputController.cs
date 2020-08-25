using System;
using UnityEngine;

/// <summary>
/// This script handles all player inputs, such as movement, attempting to pickup/drop an item, and interacting.
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool PauseButtonInput { get; private set; }

    public event Action OnInteract = delegate { }; //This event will be set off whenever the player presses the interact key.

    // Update is called once per frame
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        PauseButtonInput = Input.GetButtonDown("PauseButton");
        InteractInput = Input.GetButtonDown("InteractButton");
        if (InteractInput)
            OnInteract();            
    }
}
