using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController player;
    public GameObject bulletPreFab;
    public Transform transformBullet;
    private float timeStart = 0f;
    private float timeStop = 1f;

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckDistanEnemy();
    }

    /**
     * Hàm Check xem 10f xung quanh có Enemy không (dựa vào Tag và so sánh position)
     */
    private void CheckDistanEnemy()
    {
        // Lấy toàn bộ enemy trong game có tag enemy -> hiện tại ít e còn thấy nó nhận được không biết nhiều nhận được không
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach (GameObject enemy in enemies)
        {
            // tính khoảng cách từ player đến enemy
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= 10f)
            {
                if (timeStart >= 0f && !player.checkMove)
                {
                    timeStart += Time.deltaTime;
                    if (timeStart >= timeStop)
                    {
                        // sinh ra 1 viên đạn sau 1s
                        GameObject bullet = Instantiate(bulletPreFab, transformBullet.position, Quaternion.identity);
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        // chỉnh hướng viên đạn
                        rb.velocity = Vector3.Normalize(enemy.transform.position - transform.position) * 10f;
                        timeStart = 0f;
                    }
                }
            }
        }
    }
}
