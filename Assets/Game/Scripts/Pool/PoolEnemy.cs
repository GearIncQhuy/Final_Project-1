using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject BossMap1;

    public int enemyMax;
    private int enemyDefault;

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
        if(ManagerTimeSet.Ins.data.level < 20)
        {
            enemyMax = NumberOfEnemies();
        }
        timeStart += Time.deltaTime;
        timeDelay = (ManagerTimeSet.Ins.timeEndTurn - 5) / enemyMax;

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
                }
            }
            // coutEnemyActive < enemyMaxInMap && enemyDie <= enemyMax &&
            if (timeStart >= timeDelay && ObjectPool.Ins.enemyList.Count <= enemyMax && ManagerTimeSet.Ins.checkSpawn && ManagerTimeSet.Ins.data.level < 20)
            {
                SpawnEnemy(player.transform);
                timeStart = 0f;
            }
        }

        // Kiểm tra hết turn chưa clear quái trên map
        if (ManagerTimeSet.Ins.endTurn)
        {
            Clearreturn();
            ManagerTimeSet.Ins.endTurn = false;
        }
        if (!ManagerTimeSet.Ins.checkSpawn)
        {
            Clearreturn();
        }

        if(ManagerTimeSet.Ins.data.level == 20 && ManagerTimeSet.Ins.checkSpawn)
        {
            switch (ManagerTimeSet.Ins.data.map)
            {
                case 1:
                    BossMap1.SetActive(true);
                    break;
            }
        }
    }

    public void Clearreturn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach(GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                Enemy enemyData = enemy.GetComponent<Enemy>();
                ObjectPool.Ins.ReturnToPool(enemyData.data.tag,enemy.gameObject);
            }
        }
    }

    public int NumberOfEnemies()
    {
        return ManagerTimeSet.Ins.level * ManagerTimeSet.Ins.turn * enemyDefault;
    }

    /**
     * Random vị trí Enemy -> trong khoảng từ 20 -> 30f so với Player
     */
    private Vector3 RandomPositionEnemy(float min, float max, string enemyCategory, Transform trans)
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(min, max);
        Vector3 randomPosition = trans.position + Quaternion.Euler(0, randomAngle, 0) * (Vector3.forward * randomDistance);
        return randomPosition;
    }

    /**
     * Random enemy
     */
    private string RandomEnemy()
    {
        float random = Random.Range(0f, 100f);
        
        switch (ManagerTimeSet.Ins.data.map)
        {
            case 1:
                if (ManagerTimeSet.Ins.data.level > 0 && ManagerTimeSet.Ins.data.level <= 10)
                {
                    return Constants.EnemyRun;
                }
                else
                {
                    if (random < 50)
                    {
                        return Constants.EnemyRun;
                    }
                    else
                    {
                        return Constants.EnemyFly;
                    }
                }
            //case 2:
            //    break;
            //case 3:
            //    break;
            default:
                return Constants.EnemyRun;
        }
    }

    public void SpawnEnemy(Transform trans)
    {
        string enemyCateggory = RandomEnemy();
        GameObject enemy = ObjectPool.Ins.SpawnFromPool(enemyCateggory, RandomPositionEnemy(15, 30f, enemyCateggory, trans), Quaternion.identity);
        ObjectPool.Ins.enemyList.Add(enemy);
    }
}
