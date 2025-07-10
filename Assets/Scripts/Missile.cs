using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f; // 미사일 속도
    public int missileDamage = 1;

    [SerializeField]
    GameObject Expeffect;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
        if (transform.position.y > 7f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // Debug.Log("Missile Hit Enemy");
            GameObject effect = Instantiate(Expeffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // 이펙트는 1초 후에 제거
            Destroy(this.gameObject); // 미사일 제거
        }
    }
}
