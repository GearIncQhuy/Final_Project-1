using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    // Get data poperties
    public ScriptTableBullet data;
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if(time > 3)
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet, this.gameObject);
            time = 0;
        }
    }
    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Enemy))
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet, this.gameObject);
        }

        if (other.gameObject.CompareTag(Constants.Tag_Boss))
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet, this.gameObject);
        }

        if (other.gameObject.CompareTag(Constants.Tag_Map))
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet, this.gameObject);
        }
    }
    #endregion
}
