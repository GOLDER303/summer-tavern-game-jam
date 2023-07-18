using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private GameObject gameOverScreen;

    private int ordersInProgress = 0;
    private Coroutine startOrderCoroutine;

    private void Start()
    {
        orderManager.CreateRandomOrder();
        startOrderCoroutine = StartCoroutine(StartOrderCoroutine());
    }

    private IEnumerator StartOrderCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            orderManager.CreateRandomOrder();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        StopCoroutine(startOrderCoroutine);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
