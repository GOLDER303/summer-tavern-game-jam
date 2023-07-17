using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public OrderSO orderSO { get; set; }
    public PlayerController playerController;

    public void PickUpTheOrder(PlayerController playerController)
    {
        //TODO: Improve pickup logic
        this.playerController = playerController;
    }

    private void Update()
    {
        if(playerController != null)
        {
            transform.position = playerController.gameObject.transform.position;
        }
    }
}
