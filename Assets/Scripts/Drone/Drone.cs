using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public static Action<Drone> OnDroneFreeingDockingStation;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 queueStartPosition;
    [SerializeField] private Vector3 exitPointPosition;

    public Vector3 dockingStationPosition { get; set; }
    public int placeInQueue { get; set; } = 0;
    public OrderSO orderSO { private get; set; }

    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool hasOrder = false;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        OnDroneFreeingDockingStation += MoveInQueue;
    }

    private void OnDisable()
    {
        OnDroneFreeingDockingStation -= MoveInQueue;
    }

    private void Update()
    {
        if (placeInQueue > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(queueStartPosition.x - placeInQueue * spriteRenderer.size.x, queueStartPosition.y), moveSpeed * Time.deltaTime);
            return;
        }

        if (hasOrder)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPointPosition, moveSpeed * Time.deltaTime);
            animator.SetBool("droneArrived", false);
            animator.SetBool("droneHasOrder", true);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, dockingStationPosition, moveSpeed * Time.deltaTime);
        }

        if (transform.position == dockingStationPosition)
        {
            animator.SetBool("droneArrived", true);
        }
    }

    public bool HandleOrder(Order order)
    {
        if (this.orderSO == order.orderSO)
        {
            hasOrder = true;
            OnDroneFreeingDockingStation?.Invoke(this);
            Destroy(order.gameObject);
            return true;
        }

        return false;
    }

    private void MoveInQueue(Drone _)
    {
        if (placeInQueue > 0)
        {
            placeInQueue--;
        }
    }
}
