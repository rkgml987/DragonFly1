using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject damaegeTextPrefab; // 데미지 텍스트 프리팹
    private SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red; // 플래시 색상
    public float flashDuration = 0.1f; // 플래시 지속 시간
    private Color originalColor; // 원래 색상
    public float enemyHp = 1;

    [SerializeField]
    public float moveSpeed = 1f; // 적 속도
    public GameObject Coin;
    public GameObject Effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor; // 색상 변경
        yield return new WaitForSeconds(flashDuration); // 잠시 대기
        spriteRenderer.color = originalColor; // 원래 색상으로 복원
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            Missile missile = collision.GetComponent<Missile>();
            StopAllCoroutines(); // 기존 플래시 코루틴 중지
            StartCoroutine("HitColor");
            enemyHp = enemyHp - missile.missileDamage; // 적의 체력 감소
            if (enemyHp < 0)
            {
                Destroy(this.gameObject); // 적 제거
                Instantiate(Coin, transform.position, Quaternion.identity); // 코인 생성
                Instantiate(Effect, transform.position, Quaternion.identity); // 이펙트 생성
            }
            TakeDamage(missile.missileDamage); // 데미지 처리
        }
    }
    IEnumerator HitColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red; // 색상 변경
        yield return new WaitForSeconds(0.2f); // 잠시 대기
        spriteRenderer.color = Color.white; // 원래 색상으로 복원
    }

    void TakeDamage(int damage)
    {
        // 데미지 텍스트 생성
        DamagePopupManager.Instance.CreateDamageText(damage, transform.position);
    }
}
