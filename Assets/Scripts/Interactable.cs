using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactionRadius = 3f;
    Transform player;
    bool canInteract;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    public void Start()
    {
        player = FindObjectOfType<PlayerInteraction>().transform;
    }

    public virtual void interact()
    {
        //This function is meant to be overridden.
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.getGameHasEnded())
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            checkDistance(distanceFromPlayer);
            checkInteracting();
        }
    }

    void checkInteracting()
    {
        if(canInteract && Input.GetButtonDown("InteractButton"))
        {
            interact();
        }
    }

    void checkDistance(float distanceFromPlayer)
    {
        if(distanceFromPlayer <= interactionRadius)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }
}
