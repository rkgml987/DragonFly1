using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int coin = 0;
    public TextMeshProUGUI textMeshProCoin;
    public GameObject coinGoalUI; // 100코인 달성 UI 오브젝트를 에디터에서 할당
    public GameObject restartButton; // 재시작 버튼 오브젝트를 에디터에서 할당

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
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.MissileUp(); // 코인이 2의 배수일 때 플레이어 미사일 업그레이드
            }
        }
        if (coin >= 100)
        {
            Time.timeScale = 0f; // 게임 멈춤
            if (coinGoalUI != null)
                coinGoalUI.SetActive(true); // 100코인 달성 UI 표시
            if (restartButton != null)
                restartButton.SetActive(true); // 재시작 버튼 표시
        }
    }

    public void ShowGameOverUI()
    {
        Time.timeScale = 0f;
        if (restartButton != null)
            restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
