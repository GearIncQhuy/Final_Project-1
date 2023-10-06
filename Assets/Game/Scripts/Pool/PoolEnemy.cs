using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;

    private int enemyMax;
    private int enemyMaxInMap;
    private int enemyDefault;
    private int enemyDie;

    private float timeDelay;
    private float timeStart;

    private void Start()
    {
        enemyDefault = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Enemy Max in turn
        enemyMax = NumberOfEnemies();
        timeStart += Time.deltaTime;
        timeDelay = ManagerTimeSet.Ins.timeEndTurn / enemyMax;

        
        enemyMaxInMap = 5 * ManagerTimeSet.Ins.level * ManagerTimeSet.Ins.turn;

        // Check Player life
        if (ManagerScript.Ins.player.checkPlayerLife)
        {
            int coutEnemyActive = 0;
            for (int i = 0; i < ObjectPool.Ins.enemyList.Count; i++)
            {
                if (ObjectPool.Ins.enemyList[i].activeInHierarchy)
                {
                    coutEnemyActive++;
                }
                else
                {
                    ObjectPool.Ins.enemyList.Remove(ObjectPool.Ins.enemyList[i]);
                    enemyDie++;
                    if(enemyDie >= enemyMax)
                    {
                        enemyDie = 0;
                    }
                }
            }

            if(coutEnemyActive < enemyMaxInMap && enemyDie <= enemyMax && timeStart >= timeDelay)
            {
                GameObject enemy = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Enemy, RandomPositionEnemy(15, 30f), Quaternion.identity);
                ObjectPool.Ins.enemyList.Add(enemy);
                timeStart = 0f;
            }
        }

        //
        if (ManagerTimeSet.Ins.endTurn)
        {
            Clearreturn();
            ManagerTimeSet.Ins.endTurn = false;
        }
    }

    private void Clearreturn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach(GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                ObjectPool.Ins.ReturnToPool(Constants.Tag_Enemy,enemy.gameObject);
            }
        }
    }

    private int NumberOfEnemies()
    {
        return ManagerTimeSet.Ins.level * ManagerTimeSet.Ins.turn * enemyDefault;
    }

    /**
     * Random vị trí Enemy -> trong khoảng từ 20 -> 30f so với Player
     */
    private Vector3 RandomPositionEnemy(float min, float max)
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(min, max);
        Vector3 randomPosition = player.position + Quaternion.Euler(0, randomAngle, 0) * (Vector3.forward * randomDistance);
        return randomPosition;
    }
}
