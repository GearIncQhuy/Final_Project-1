using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour
{
    #region Poperties
    private float time;
    #endregion

    private void Start()
    {
        time = 0f;
    }

    private bool checkEnd = true;
    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 1f)
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill1_2, this.gameObject);
            time = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (checkEnd)
        {
            SelecEnemy();
        }
    }

    #region Select Enemy In Distans
    private EnemyUI enemyUI;
    private void SelecEnemy()
    {
        foreach (GameObject enemy in ObjectPool.Ins.enemyList)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= 8f)
            {
                enemyUI = enemy.GetComponent<EnemyUI>();
                // Update lại máu enemy đấy
                enemyUI.UpdateHealEnemy(enemyUI.enemy.heal, ManagerScript.Ins.player.GetDamePlayer(1, ManagerScript.Ins.player.data.level, false));

                checkEnd = false;
            }
        }
    }
    #endregion
}
