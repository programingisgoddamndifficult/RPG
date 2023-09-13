
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;//允许与物品交互的半径
    public Transform interactionTransform;//交互变换

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("INTERACT WITH ");
    }

    void Update()
    {
        if (isFocus&&!hasInteracted)
        {
            float distance=Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius) 
            {
                Interact();
                hasInteracted =true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player= playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus= false;
        player= null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);

    }
}
