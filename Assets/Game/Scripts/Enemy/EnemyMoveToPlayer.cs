using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Move,
    Attack
}

public class EnemyMoveToPlayer : MonoBehaviour
{
    private Enemy enemy;
    private Vector3 yPosition;
    private Vector3 positionCurrent;
    private float timeStart;

    public EnemyState currentState;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        timeStart = Time.time;
        yPosition = transform.position;
        yPosition.y += 5f;
        currentState = EnemyState.Move;
    }
    private void Update()
    {
        positionCurrent = transform.position;
        switch (currentState)
        {
            case EnemyState.Move:
                MoveToPlayer();
                break;
            case EnemyState.Attack:
                if (positionCurrent == transform.position)
                {
                    if (Time.time - timeStart >= enemy.data.speedFire)
                    {
                        Attack();
                    }
                }
                break;
        }
    }

    private void MoveToPlayer()
    {
        float distance = Vector3.Distance(ManagerScript.Ins.player.transform.position, transform.position);
        if (distance >= enemy.data.tamdanh)
        {
            Vector3 target = ManagerScript.Ins.player.transform.position - transform.position;
            transform.Translate(target.normalized * enemy.data.speed * Time.deltaTime);
            if (enemy.data.category == EnemyCategory.Fly)
            {
                yPosition.x = transform.position.x;
                yPosition.z = transform.position.z;
                transform.position = yPosition;
            }
        }
        if (distance <= enemy.data.tamdanh)
        {
            currentState = EnemyState.Attack;
        }
    }

    private void Attack()
    {
        if (enemy.data.category == EnemyCategory.Fly)
        {
           GameObject bullet = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Bullet_Enemy, transform.position, Quaternion.identity);
           if (bullet != null)
           {
              Rigidbody rb = bullet.GetComponent<Rigidbody>();
              rb.velocity = Vector3.Normalize(ManagerScript.Ins.player.transform.position - transform.position) * 10f;
              timeStart = Time.time;
           }
        }
        currentState = EnemyState.Move;
    }
}
