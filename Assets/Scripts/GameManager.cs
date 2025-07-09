using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int coin = 0;
    public TextMeshProUGUI textMeshProCoin;

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
}
