using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500f;

    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D playerRigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject currentInteractionTarget;
    [SerializeField] private Order currentlyOwnedOrder;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = moveVector * moveSpeed * Time.deltaTime;
    }

    private void OnMove(InputValue inputValue)
    {
        moveVector = inputValue.Get<Vector2>();
        animator.SetBool("isMoving", moveVector != Vector2.zero);

        if (moveVector.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveVector.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Drone") || other.CompareTag("Order"))
        {
            currentInteractionTarget = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentInteractionTarget = null;
    }

    private void OnInteract()
    {
        if (currentInteractionTarget == null)
        {
            return;
        }

        if (currentInteractionTarget.CompareTag("Drone"))
        {
            if (currentlyOwnedOrder != null)
            {
                bool orderAccepted = currentInteractionTarget.GetComponent<Drone>().HandleOrder(currentlyOwnedOrder);
                if (orderAccepted)
                {
                    currentlyOwnedOrder = null;
                }
            }
        }
        else if (currentInteractionTarget.CompareTag("Order"))
        {
            currentlyOwnedOrder = currentInteractionTarget.GetComponent<Order>();
            currentlyOwnedOrder.PickUpTheOrder(this);
        }

    }
}
