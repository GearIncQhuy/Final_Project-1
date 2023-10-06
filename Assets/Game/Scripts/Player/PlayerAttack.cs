using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController player;
    public Transform transformBullet;
    private float timeStart = 0f;

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
     * Hàm Check xem 10f xung quanh có Enemy không (dựa vào Tag và so sánh position) bắn vào Enemy gần nhất
     */
    private void CheckDistanEnemy()
    {
        // Lấy toàn bộ enemy trong game có tag enemy -> hiện tại ít e còn thấy nó nhận được không biết nhiều nhận được không
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        //foreach (GameObject enemy in enemies)

        if(ObjectPool.Ins.enemyList.Count > 0)
        {
            float[] position = new float[ObjectPool.Ins.enemyList.Count];
            for(int i = 0; i < ObjectPool.Ins.enemyList.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, ObjectPool.Ins.enemyList[i].transform.position);
                position[i] = distance;
            }

            int indexMin = 0;
            float minPosition = position[0];

            for(int i = 0; i < ObjectPool.Ins.enemyList.Count; i++)
            {
                if(minPosition > position[i])
                {
                    minPosition = position[i];
                    indexMin = i;
                }
            }

            if(minPosition <= 15f)
            {
                if(timeStart >= 0f && !player.checkMove)
                {
                    if(Time.time - timeStart >= 0.5f)
                    {
                        GameObject bullet = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Bullet, transformBullet.position, Quaternion.identity);
                        if (bullet != null)
                        {
                            Rigidbody rb = bullet.GetComponent<Rigidbody>();
                            rb.velocity = Vector3.Normalize(ObjectPool.Ins.enemyList[indexMin].transform.position - transform.position) * 20f;
                            timeStart = Time.time;
                        }
                    }
                }
            }
        }
    }
}
