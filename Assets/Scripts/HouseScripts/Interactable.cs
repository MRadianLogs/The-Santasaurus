using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private float interactionRadius = 3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    public virtual void Interact()
    {
        //This function is meant to be overridden.
    }
}
