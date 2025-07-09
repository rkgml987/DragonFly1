using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    int missIndex = 0;
    public GameObject[] missilePrefabs;
    public Transform spPosition;

    [SerializeField]
    private float shootInterval  = 0.05f; // 발사 간격
    private float lastshotTime = 0f; // 마지막 발사 시간
    private Animator animator;

    public GameObject gameOverUI; // 게임 오버 UI 오브젝트를 에디터에서 할당

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, 0, 0);
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        if (horizontalInput < 0)
        {
            animator.Play("Left1");
        }
        else if (horizontalInput > 0)
        {
            animator.Play("Right1");
        }
        else
        {
            animator.Play("Idle1");
        }
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time - lastshotTime > shootInterval)
        {
            Instantiate(missilePrefabs[missIndex], spPosition.position, Quaternion.identity);
            lastshotTime = Time.time;
        }
        else
        {
            Debug.LogWarning($"[Shoot 오류] missIndex({missIndex})가 missilePrefabs 범위를 벗어났습니다.");
        }
    }

    public void MissileUp()
    {
        missIndex++;
        shootInterval = shootInterval - 0.1f;
        if (shootInterval  <= 0.1f)
        {
            shootInterval = 0.1f;
        }
        if (missIndex >= missilePrefabs.Length)
        {
            missIndex = missilePrefabs.Length - 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Time.timeScale = 0f; // 게임 정지
            if (animator != null)
                animator.enabled = false; // 애니메이션 정지
            if (gameOverUI != null)
                gameOverUI.SetActive(true); // 게임 오버 UI 표시
            Debug.Log("Game Over");
        }
    }
}
