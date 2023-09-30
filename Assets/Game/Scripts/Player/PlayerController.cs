using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // get data poperties
    public ScripTablePlayer data;
    // get PreFab bullet
    public GameObject bulletPreFab;

    private ManagerScript manager;
    private Rigidbody rigid;

    public float manaCurrent;
    public float healCurrent;
    public float dame;

    private void Awake()
    {
        manager = ManagerScript.Ins;
        manaCurrent = data.manaMax;
        healCurrent = data.healMax;
        Calculate();
    }

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hori, 0.0f, ver);
        rigid.velocity = movement * data.speed * Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(manager.manaPlayer.UseMana(manaCurrent, 1, data.level))
            {
                manager.shootFunction.Shoot(bulletPreFab, 20f, this.gameObject);
            }
        }
        CheckDistanEnemy();
    }

    private void Calculate()
    {
        if(manager.nourishmentRestraintFuction.checkMutualNourishment(data.phases, manager.bullet.data.phases)){
            dame = data.dameMax + manager.bullet.data.baseDame;
        }else if(manager.nourishmentRestraintFuction.checkMutualRestraint(manager.bullet.data.phases, data.phases))
        {
            dame = data.dameMax - manager.bullet.data.baseDame;
        }
        else
        {
            dame = data.dameMax;
        }
    }

    private void CheckDistanEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);
        Transform playerTransform = transform;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);

            if (distance <= 10f)
            {
                Debug.Log("Enemy " + distance);
            }
        }
    }
}
