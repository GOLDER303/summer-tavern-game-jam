using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private float moveSpeedIncreaseSpeed = 10f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    private Vector2 moveVector = Vector2.left;
    private Rigidbody2D playerRigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject currentInteractionTarget;
    private Order currentlyOwnedOrder;

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

    private void Update()
    {
        moveSpeed += moveSpeedIncreaseSpeed * Time.deltaTime;
        moveSpeedText.text = "Move speed: " + (int)moveSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        if (inputVector != Vector2.zero)
        {
            moveVector = inputValue.Get<Vector2>();
        }

        if (moveVector.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveVector.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Drone") || other.CompareTag("Order"))
        {
            if (other.CompareTag("Order") && other.gameObject.GetComponent<Order>().playerController != null)
            {
                return;
            }

            if (currentInteractionTarget != null)
            {
                currentInteractionTarget.GetComponent<Focusable>().UnFocus();
            }

            currentInteractionTarget = other.gameObject;
            currentInteractionTarget.GetComponent<Focusable>().Focus();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Drone") || other.CompareTag("Order"))
        {
            other.gameObject.GetComponent<Focusable>().UnFocus();
            currentInteractionTarget = null;
        }
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
        else if (currentInteractionTarget.CompareTag("Order") && currentlyOwnedOrder == null)
        {
            currentlyOwnedOrder = currentInteractionTarget.GetComponent<Order>();
            currentlyOwnedOrder.PickUpTheOrder(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameManager.GameOver();
        Destroy(gameObject);
    }
}
