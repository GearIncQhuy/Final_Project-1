using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeRandom : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GlobeMove();
    }

    #region Globe Move
    /**
     * Quả cầu rơi dần xuống
     */
    private void GlobeMove()
    {
        Vector3 target = transform.position;
        target.y = 0;
        Vector3 move =  target - transform.position;
        transform.Translate(move.normalized * 20f * Time.deltaTime);

        // Kiểm tra xem đối tượng đã đến vị trí mới chưa
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget < 1f)
        {
            // tạo vụ nổ
            GameObject boom = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill3_3, transform.position, Quaternion.identity);
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill3_2, this.gameObject);
        }
    }
    #endregion
}
