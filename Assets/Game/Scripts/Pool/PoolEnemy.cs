using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
    #region Poperties Default
    [SerializeField] private Transform player;
    [SerializeField] private GameObject BossMap1;

    public int enemyMax;
    private int enemyDefault;

    private float timeDelay;
    private float timeStart;
    #endregion

    private void Start()
    {
        enemyDefault = 2;
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
            ManagerTimeSet.Ins.endTurn = false;
        }
        if (!ManagerTimeSet.Ins.checkSpawn)
        {
            Clearreturn();
        }

        if(ManagerTimeSet.Ins.data.level == 20 && ManagerTimeSet.Ins.checkSpawn && ManagerScript.Ins.player.checkPlayerLife)
        {
            switch (ManagerTimeSet.Ins.data.map)
            {
                case 1:
                    StartCoroutine(spawnBossMap1());
                    break;
            }
        }
    }

    #region Spawn Boss Map 1
    IEnumerator spawnBossMap1()
    {
        yield return new WaitForSeconds(1.5f);
        BossMap1.SetActive(true);
    }
    #endregion

    #region Clear Enemy End Ware
    public void Clearreturn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        foreach(GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                Enemy enemyData = enemy.GetComponent<Enemy>();
                if(enemyData != null)
                {
                    enemyData.isDieEnemy = true;
                }
            }
        }
        if(ManagerTimeSet.Ins.data.level == 20)
        {
            switch (ManagerTimeSet.Ins.data.map)
            {
                case 1:
                    BossMap1 boss = BossMap1.GetComponent<BossMap1>();
                    if(boss != null)
                    {
                        boss.ResetPoperties();
                        BossMap1.SetActive(false);
                    }
                    break;
            }
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag(Constants.Tag_BulletEnemy);
        foreach(GameObject bullet in bullets)
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullet_Enemy, bullet);
        }
    }
    #endregion

    #region Number Max Enemies In SubWare
    public int NumberOfEnemies()
    {
        return ManagerTimeSet.Ins.level * ManagerTimeSet.Ins.turn * enemyDefault;
    }
    #endregion

    #region Random Position Enemy In Map
    /**
     * Random vị trí Enemy -> trong khoảng từ 20 -> 30f so với Player
     */
    private Vector3 RandomPositionEnemy(float min, float max, string enemyCategory, Transform trans)
    {
        bool pool = true;
        while (pool)
        {
            float randomAngle = Random.Range(0f, 360f);
            float randomDistance = Random.Range(min, max);
            Vector3 randomPosition = trans.position + Quaternion.Euler(0, randomAngle, 0) * (Vector3.forward * randomDistance);
            if (ManagerMap.Ins.ListMap[0] != null)
            {
                Map map = ManagerMap.Ins.ListMap[0].GetComponent<Map>();
                if (map.CheckPositonSpawn(randomPosition))
                {
                    pool = false;
                    return randomPosition;
                }
            }
        }
        return Vector3.zero;
    }
    #endregion

    #region Random Category Enemy
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
                    //return Constants.EnemyFly;
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
    #endregion

    #region Spawn Enemy
    public void SpawnEnemy(Transform trans)
    {
        string enemyCateggory = RandomEnemy();
        GameObject enemy = ObjectPool.Ins.SpawnFromPool(enemyCateggory, RandomPositionEnemy(10, 15f, enemyCateggory, trans), Quaternion.identity);
        ObjectPool.Ins.enemyList.Add(enemy);
    }
    #endregion
}
