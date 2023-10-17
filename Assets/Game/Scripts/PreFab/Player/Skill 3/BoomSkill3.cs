using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomSkill3 : MonoBehaviour
{
    private float time;
    private void Start()
    {
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 0.2f)
        {
            SelectEnemy();
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill3_3, this.gameObject);
            time = 0f;
        }
    }

    #region Select Enemy In Distan
    /**
     * Tìm kiến enemy trong vùng ảnh hưởng
     */
    private EnemyUI enemyUI;
    private void SelectEnemy()
    {
        foreach (GameObject enemy in ObjectPool.Ins.enemyList)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= 8f)
            {
                enemyUI = enemy.GetComponent<EnemyUI>();
                // Update lại máu enemy đấy
                if(enemyUI != null)
                {
                    enemyUI.UpdateHealEnemy(enemyUI.enemy.heal, enemyUI.enemy.heal);
                }

            }
        }
    }
    #endregion
}
