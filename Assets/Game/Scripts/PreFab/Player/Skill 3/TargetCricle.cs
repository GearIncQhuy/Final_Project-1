using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCricle : MonoBehaviour
{
    Vector3 globalPosition;
    // Start is called before the first frame update
    void Start()
    {
        globalPosition = transform.position;
        globalPosition.x += 50f;
        globalPosition.y += 20f;
    }

    private float timeStart = 1f;
    private float timeEnd;

    private void LateUpdate()
    {
        timeStart += Time.deltaTime;
        timeEnd += Time.deltaTime;

        // Sau 1s active true 1 quả cầu
        if (timeStart >= 1f)
        {
            GameObject globe = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill3_2, RandomPosition(), Quaternion.identity);
            timeStart = 0f;
        }

        // Sau 10s kết thúc chiêu
        if (timeEnd >= 10f)
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill3, this.gameObject);
            timeEnd = 0f;
        }
    }

    #region Radom Position
    /**
     * Random vị trí rơi của Quả cầu
     */
    public Vector3 RandomPosition()
    {
        float angle = Random.Range(0f, 360f); // Góc ngẫu nhiên
        float distance = Random.Range(0f, 20f); // bán kính 

        Vector3 position = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
        position.y += 60f;
        return position;
    }
    #endregion
}
