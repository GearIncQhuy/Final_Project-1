using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire : MonoBehaviour
{
    private ManagerScript manager;
    private float time;
    private float timeDestroy;

    void Start()
    {
        time = 0;
        timeDestroy = 0;
        manager = ManagerScript.Ins;
    }

    private void Update()
    {
        manager.player.checkMove = true;
        manager.player.DontMove = true;
        timeDestroy += Time.deltaTime;
        time += Time.deltaTime;
        if(time >= 1f)
        {
            // Dừng di chuyển và dừng bắn
            CheckEnemy();
            time = 0f;
        }
        // Destroy vòng lửa
        if(timeDestroy >= 5f)
        {
            // Huỷ dừng di chuyển và tự động bắn
            manager.player.checkMove = false;
            manager.player.DontMove = false;
            Destroy(this.gameObject);
        }
    }
    /**
     * Hàm check Enemy xung quanh
     */
    private void CheckEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach(GameObject enemyObj in enemies)
        {
            float distance = Vector3.Distance(enemyObj.transform.position, transform.position);
            if(distance <= 10f)
            {
                EnemyUI enemyUI = enemyObj.GetComponent<EnemyUI>();
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                // Update máu của Enemy 
                enemyUI.UpdateHealEnemy(enemy.heal, manager.player.GetDamePlayer(2, manager.player.data.level, false));
            }
        }
    }
}
