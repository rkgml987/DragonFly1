using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int coin = 0;
    public TextMeshProUGUI textMeshProCoin;
    public GameObject gameOverUI;
    private bool isGameOver = false;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowCoinCount()
    {
        coin++;
        textMeshProCoin.SetText(coin.ToString());
        if (coin % 2 == 0)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.MissileUp(); // 코인이 2의 배수일 때 플레이어 미사일 업그레이드
            }
        }
    }

    public void GameOver(Player player)
    {
        Time.timeScale = 0f;
        if (player != null && player.GetComponent<Animator>() != null)
            player.GetComponent<Animator>().enabled = false;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        isGameOver = true;
        Debug.Log("Game Over");
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
