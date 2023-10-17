using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire : MonoBehaviour
{
    #region Poperties
    private float time;
    private float timeDestroy;
    #endregion

    void Start()
    {
        time = 0;
        timeDestroy = 0;
    }

    private void Update()
    {
        ManagerScript.Ins.player.checkMove = true;
        ManagerScript.Ins.player.DontMove = true;
        timeDestroy += Time.deltaTime;
        time += Time.deltaTime;
        if (time >= 1f)
        {
            // Dừng di chuyển và dừng bắn
            CheckEnemy();
            time = 0f;
        }
        // Destroy vòng lửa
        if (timeDestroy >= 5f)
        {
            // Huỷ dừng di chuyển và tự động bắn
            ManagerScript.Ins.player.checkMove = false;
            ManagerScript.Ins.player.DontMove = false;
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill2, this.gameObject);
            timeDestroy = 0f;
        }
    }

    #region Check Enemy In Distans
    /**
     * Hàm check Enemy xung quanh
     */
    private void CheckEnemy()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach (GameObject enemyObj in ObjectPool.Ins.enemyList)
        {
            float distance = Vector3.Distance(enemyObj.transform.position, transform.position);
            if (distance <= 10f)
            {
                EnemyUI enemyUI = enemyObj.GetComponent<EnemyUI>();
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                // Update máu của Enemy 
                enemyUI.UpdateHealEnemy(enemy.heal, ManagerScript.Ins.player.GetDamePlayer(2, ManagerScript.Ins.player.data.level, false));
            }
        }
    }
    #endregion
}
