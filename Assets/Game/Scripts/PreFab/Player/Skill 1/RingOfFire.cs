using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour
{
    private ManagerScript manager;
    private float time;
    private void Start()
    {
        manager = ManagerScript.Ins;
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
                enemyUI.UpdateHealEnemy(enemyUI.enemy.heal, manager.player.GetDamePlayer(1, manager.player.data.level, false));

                checkEnd = false;
            }
        }
    }
}
