using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        timeStart = Time.time;
        yPosition = transform.position;
        yPosition.y += 5f;
        currentState = EnemyState.Move;
    }
    private void Update()
    {
        if (!enemy.isDieEnemy)
        {
            positionCurrent = transform.position;
            Vector3 target = ManagerScript.Ins.player.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
            switch (currentState)
            {
                case EnemyState.Move:
                    MoveToPlayer();
                    animator.SetBool(Constants.Enemy_Run_Ani, true);
                    animator.SetBool(Constants.Enemy_Attack_Ani, false);
                    break;
                case EnemyState.Attack:
                    if (positionCurrent == transform.position)
                    {
                        if (Time.time - timeStart >= enemy.data.speedFire)
                        {
                            animator.SetBool(Constants.Enemy_Run_Ani, false);
                            Attack();
                        }
                    }
                    break;
            }
        }
    }

    private void MoveToPlayer()
    {
        float distance = Vector3.Distance(ManagerScript.Ins.player.transform.position, transform.position);
        if (distance >= enemy.data.tamdanh)
        {
            Vector3 target = ManagerScript.Ins.player.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
            transform.Translate(Vector3.forward * enemy.data.speed * Time.deltaTime);
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

    private float timeAttack = 2f;
    private void Attack()
    {
        animator.SetBool(Constants.Enemy_Attack_Ani, true);
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
        else
        {
            timeAttack += Time.deltaTime;
            if(timeAttack >= 2f)
            {
                ManagerScript.Ins.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, enemy.dame);
                timeAttack = 0f;
            }
        }
        
        float distance = Vector3.Distance(ManagerScript.Ins.player.transform.position, transform.position);
        if (distance >= enemy.data.tamdanh)
        {
            animator.SetBool(Constants.Enemy_Attack_Ani, false);
            currentState = EnemyState.Move;
        }
    }
}
