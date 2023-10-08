using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Enemy enemy;
    private EnemyUI enemyUI;
    private void Start()
    {
        enemyUI = gameObject.GetComponent<EnemyUI>();
        enemy = gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm Player -> chuyển đánh Player gây dame vào máu Player
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            ManagerScript.Ins.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, enemy.dame);
        }

        // Kiểm tra va chạm vào đạn của Player
        if (other.gameObject.CompareTag(Constants.Tag_Bullet))
        {
            enemyUI.UpdateHealEnemy(enemy.heal, ManagerScript.Ins.player.dame);
        }

        // Kiểm tra va chạm vào chiêu 1 của Player
        if (other.gameObject.CompareTag(Constants.Tag_Skill1))
        {
            enemyUI.UpdateHealEnemy(enemyUI.enemy.heal, ManagerScript.Ins.player.GetDamePlayer(1, ManagerScript.Ins.player.data.level, true));
        }
    }
}
