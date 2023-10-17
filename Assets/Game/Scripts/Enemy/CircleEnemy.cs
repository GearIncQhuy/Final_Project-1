using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    #region Poperties
    [SerializeField] private string TagEnemy;
    [SerializeField] private string TagCircle;
    private float time;
    private int dem;
    private Vector3 enemyPosition;
    #endregion

    private void Start()
    {
        enemyPosition = transform.position;
        if(TagEnemy == Constants.EnemyRun)
        {
            enemyPosition.y = ManagerScript.Ins.player.transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1.5f && dem == 0 && !ManagerScript.Ins.player.checkBatTu && ManagerScript.Ins.player.checkPlayerLife)
        {
            GameObject enemy = ObjectPool.Ins.SpawnFromPool(TagEnemy, enemyPosition, Quaternion.identity);
            ObjectPool.Ins.enemyList.Add(enemy);
            dem++;
        }
        if(time >= 2f)
        {
            time = 0;
            dem = 0;
            ObjectPool.Ins.ReturnToPool(TagCircle, this.gameObject);
        }
    }
}
