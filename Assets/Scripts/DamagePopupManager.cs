using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager Instance { get; private set; }

    public RectTransform canvasRect;
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹
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
    public void CreateDamageText(int damage, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        GameObject textObj = Instantiate(damageTextPrefab, canvasRect);
        textObj.GetComponent<RectTransform>().position = screenPos;

        textObj.GetComponent<DamageText>().Show(damage);
    }
}
