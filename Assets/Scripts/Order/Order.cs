using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private OrderSO _orderSO;
    public OrderSO orderSO
    {
        get => _orderSO;
        set
        {
            _orderSO = value;
            spriteRenderer.sprite = orderSO.sprite;
        }
    }

    public PlayerController playerController { get; private set; }
    private Focusable focusable;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        focusable = GetComponent<Focusable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUpTheOrder(PlayerController playerController)
    {
        this.playerController = playerController;
        focusable.UnFocus();
    }

    private void Update()
    {
        if (playerController != null)
        {
            transform.position = playerController.gameObject.transform.position + Vector3.up * .5f;
        }
    }
}
