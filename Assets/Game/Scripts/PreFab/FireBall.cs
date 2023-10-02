using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private ManagerScript manager;
    [SerializeField] private GameObject ringOfFire;

    private Skill_1 skill;
    private void Start()
    {
        manager = ManagerScript.Ins;
        skill = manager.player.GetComponent<Skill_1>();

    }

    private void Update()
    {
        if(skill.checkUse == 2)
        {
            GameObject ringFire = Instantiate(ringOfFire, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Tìm kiếm Enemy
        GetEnemy();
    }

    private EnemyUI enemyUI;

    /**
     * Hàm gọi toàn bộ Enemy trong game và check con Enemy nào gần quả cầu nhất
     */
    private void GetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= 1f)
            {
                enemyUI = enemy.GetComponent<EnemyUI>();
                // Update lại máu enemy đấy
                enemyUI.UpdateHealEnemy(enemyUI.enemy.heal, manager.player.GetDamePlayer(1, manager.player.data.level, true));
                //
                Destroy(this.gameObject);
            }
        }
    }
}
