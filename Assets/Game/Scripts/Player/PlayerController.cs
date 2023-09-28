using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // get data poperties
    public ScripTablePlayer data;
    // get PreFab bullet
    [SerializeField]
    private GameObject bulletPreFab;

    private ManagerScript manager;
    private Rigidbody rigid;
    public float dame;

    private void Awake()
    {
        manager = ManagerScript.Ins;
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
        //Debug.Log("move Player");
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hori, 0.0f, ver);
        rigid.velocity = movement * data.speed * Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            manager.shootFunction.Shoot(bulletPreFab, 20f, this.gameObject);
            Debug.Log(dame);
        }
    }

    private void Calculate()
    {
        if(manager.nourishmentRestraintFuction.checkMutualNourishment(data.phases, manager.bullet.data.phases)){
            dame = data.baseDame + manager.bullet.data.baseDame;
        }else if(manager.nourishmentRestraintFuction.checkMutualRestraint(manager.bullet.data.phases, data.phases))
        {
            dame = data.baseDame - manager.bullet.data.baseDame;
        }
        else
        {
            dame = data.baseDame;
        }
    }
}
