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
            SelecEnemy();
            Destroy(this.gameObject);
        }
    }

    /**
     * Tìm kiến enemy trong vùng ảnh hưởng
     */
    private EnemyUI enemyUI;
    private void SelecEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= 8f)
            {
                enemyUI = enemy.GetComponent<EnemyUI>();
                // Update lại máu enemy đấy
                enemyUI.UpdateHealEnemy(enemyUI.enemy.heal, enemyUI.enemy.heal);
            }
        }
    }
}
