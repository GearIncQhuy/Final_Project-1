using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public ScripTableEnemy enemy;

    private float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 5f)
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet_Enemy, this.gameObject);
        }
    }

    #region Trigger 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            if (!ManagerScript.Ins.player.checkBatTu)
            {
                ManagerScript.Ins.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, enemy.dameMax);
            }
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet_Enemy, this.gameObject);
        }

        if (other.gameObject.CompareTag(Constants.Tag_Map))
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet_Enemy, this.gameObject);
        }
    }
    #endregion
}
