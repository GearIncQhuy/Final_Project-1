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
        if(time >= 2f)
        {
            Destroy(this.gameObject);
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach (GameObject enemy in enemies)
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
