using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;

    private int ordersInProgress = 0;

    private void Start()
    {
        StartCoroutine(StartOrderCoroutine());
    }

    private IEnumerator StartOrderCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            orderManager.CreateRandomOrder();
        }
    }
}
