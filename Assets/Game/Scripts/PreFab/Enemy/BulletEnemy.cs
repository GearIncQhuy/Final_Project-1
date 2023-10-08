using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public ScripTableEnemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            ManagerScript.Ins.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, enemy.dameMax);
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet_Enemy, this.gameObject);
        }
    }
}
