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
    private Animator animator;
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
        animator = gameObject.GetComponent<Animator>();

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

        if (timeSkill >= 10f && distance < 18f && timeSkill <= 10f)
        {
            if(currentState != BossMap1State.Defense)
            {
                currentState = BossMap1State.Attack2;
            }
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
        animator.Play(Constants.BossMap1_Idle);
        // change move
        if (time >= 0.5f)
        {
            currentState = BossMap1State.Move;
            timeStart = false;
            time = 0f;
        }
    }

    private void MoveState(float distance)
    {
        // animation
        animator.Play(Constants.BossMap1_Move);
        // Move
        if (distance > data.tamdanh)
        {
            Vector3 target = ManagerScript.Ins.player.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
            transform.Translate(Vector3.forward * data.speed * Time.deltaTime);
        }
        // check attack 1
        if(distance < data.tamdanh)
        {
            timeStart = true;
            currentState = BossMap1State.Attack1;
        }
    }

    private float timeAttack1 = 2f;
    private void Attack1(float distance)
    {
        if (distance < data.tamdanh)
        {
            //animator.Play(Constants.BossMap1_Attack1);
            timeAttack1 += Time.deltaTime;
            if (timeAttack1 >= 2f)
            {
                // animation
                animator.Play(Constants.BossMap1_Attack1);
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
        // animation
        animator.Play(Constants.BossMap1_Idle);

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
        // animation
        animator.Play(Constants.BossMap1_SpawnEnemy);

        timePool += Time.deltaTime;
        if(timePool >= 0.5f)
        {
            for (int i = 0; i < spawnEnemy; i++)
            {
                ManagerScript.Ins.poolEnemy.SpawnEnemy(transform);
            }
            timePool = 0f;
            useAttack3++;
            spawnEnemy = useAttack3;
            timeSkill = -10f;
            currentState = BossMap1State.Move;
        }
    }

    public void ResetPoperties()
    {
        time = 0f;
        timeSkill = 0f;
        timeStart = true;
        useDefense = true;
        dem = 0;
        timeDie = 0f;
        healCurrent = data.healMax;
        ObjectPool.Ins.enemyList.Remove(this.gameObject);
    }

    private float timeDie = 0f;
    private void Die()
    {
        timeDie += Time.deltaTime;
        animator.Play(Constants.BossMap1_Die);

        if(timeDie >= 2f)
        {
            ResetPoperties();

            time = 0f;
            timeSkill = 0f;
            timeStart = true;
            useDefense = true;
            dem = 0;
            timeDie = 0f;
            ManagerTimeSet.Ins.NextLevel();
            this.gameObject.SetActive(false);
        }
    }

    private float timeBatTu = 0f;
    private int spawnEnemyDef = 10;
    private bool checkSpawnDef = true;
    private void Defense()
    {
        // animation
        animator.Play(Constants.BossMap1_Idle);

        battu = true;
        timeBatTu += Time.deltaTime;
        //StartCoroutine(Flash());

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

    IEnumerator Flash()
    {
        for (int i = 0; i < 3; i++)
        {
            // Tắt hiển thị Renderer của đối tượng.
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);

            // Bật hiển thị Renderer của đối tượng.
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
