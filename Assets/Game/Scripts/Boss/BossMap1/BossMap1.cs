using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap1 : MonoBehaviour
{
    public enum BossMap1State
    {
        Idle,
        Move,
        Attack1,
        Attack2,
        Attack3,
        Defense,
        Die
    }
    
    [SerializeField] private GameObject skill2;
    [SerializeField] private GameObject skill22;
    public ScriptTableMap1 data;
    public BossMap1State currentState;
    private float speed;
    private float dameCurrent;
    public float healCurrent;

    private float time;
    public float timeSkill;
    private bool timeStart;

    private bool useDefense;
    private int dem = 0;
    public bool battu = false;

    private void Start()
    {
        speed = data.speed;
        healCurrent = data.healMax;
        dameCurrent = data.dameMax;
        currentState = BossMap1State.Idle;

        time = 0f;
        timeSkill = 0f;
        timeStart = true;

        useDefense = true;
    }

    private void Update()
    {
        if(dem == 0)
        {
            ObjectPool.Ins.enemyList.Add(this.gameObject);
            dem++;
        }

        float distance = Vector3.Distance(ManagerScript.Ins.player.transform.position, transform.position);
        switch (currentState)
        {
            case BossMap1State.Idle:
                timeStart = true;
                IdleState();
                break;
            case BossMap1State.Move:
                MoveState(distance);
                break;
            case BossMap1State.Attack1:
                Attack1(distance);
                break;
            case BossMap1State.Attack2:
                Attack2();
                break;
            case BossMap1State.Attack3:
                Attack3();
                break;
            case BossMap1State.Defense:
                Defense();
                break;
            case BossMap1State.Die:
                Die();
                break;
        }

        if (timeStart)
        {
            time += Time.deltaTime;
        }

        timeSkill += Time.deltaTime;
        // Skill 1

        if (timeSkill >= 10f && distance < 18f && timeSkill <= 15f)
        {
            currentState = BossMap1State.Attack2;
        }
        // Skill 2
        if(timeSkill >= 30f && currentState == BossMap1State.Move)
        {
            currentState = BossMap1State.Attack3;
            timeSkill = 0f;
        }

        // Boss heal < 40%
        if(healCurrent <= data.healMax * 4 / 10 && useDefense)
        {
            useDefense = false;
            currentState = BossMap1State.Defense;
        }

        if(healCurrent <= 0)
        {
            currentState = BossMap1State.Die;
            ResetPoperties();
        }
    }

    private void IdleState()
    {
        // animation

        // change move
        if(time >= 0.5f)
        {
            currentState = BossMap1State.Move;
            timeStart = false;
            time = 0f;
        }
    }

    private void MoveState(float distance)
    {
        // animation

        // Move
        if(distance > data.tamdanh)
        {
            Vector3 target = ManagerScript.Ins.player.transform.position - transform.position;
            transform.Translate(target.normalized * speed * Time.deltaTime);
        }
        // check attack 1
        if(distance < data.tamdanh)
        {
            timeStart = true;
            currentState = BossMap1State.Attack1;
        }
    }

    private float timeAttack1 = 1f;
    private void Attack1(float distance)
    {
        if (distance < data.tamdanh)
        {
            // animation

            timeAttack1 += Time.deltaTime;
            if (timeAttack1 >= 2f)
            {
                ManagerScript.Ins.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, dameCurrent);
                timeAttack1 = 0f;
            }
        }
        else
        {
            currentState = BossMap1State.Move;
        }
    }

    private float timeUseAttack2 = 0f;
    private void Attack2()
    {
        timeUseAttack2 += Time.deltaTime;
        skill2.SetActive(true);
        if(timeUseAttack2 > 2f)
        {
            skill22.SetActive(true);
            timeUseAttack2 = 0;
        }
    }

    private float timePool = 0f;
    private int useAttack3 = 1;
    private int spawnEnemy = 10;
    private void Attack3()
    {
        timePool += Time.deltaTime;
        if(timePool >= 0.5f)
        {
            for(int i = 0; i < spawnEnemy; i++)
            {
                ManagerScript.Ins.poolEnemy.SpawnEnemy(transform);
            }
            timePool = 0f;
            useAttack3++;
            spawnEnemy *= useAttack3;
            timeSkill = -10f;
            currentState = BossMap1State.Move;
        }
    }

    private void ResetPoperties()
    {
        healCurrent = data.healMax;
        ObjectPool.Ins.enemyList.Remove(this.gameObject);
    }

    private void Die()
    {
        ResetPoperties();

        time = 0f;
        timeSkill = 0f;
        timeStart = true;
        useDefense = true;
        dem = 0; 

        ManagerTimeSet.Ins.NextLevel();
        this.gameObject.SetActive(false);
    }

    private float timeBatTu = 0f;
    private int spawnEnemyDef = 10;
    private bool checkSpawnDef = true;
    private void Defense()
    {
        battu = true;
        timeBatTu += Time.deltaTime;
        if (checkSpawnDef)
        {
            for(int i = 0; i < spawnEnemyDef; i++)
            {
                ManagerScript.Ins.poolEnemy.SpawnEnemy(transform);
            }
            checkSpawnDef = false;
        }
        if(timeBatTu >= 5)
        {
            healCurrent += data.healMax * (3 / 10);
            battu = false;
            currentState = BossMap1State.Move;
        }
    }
}
