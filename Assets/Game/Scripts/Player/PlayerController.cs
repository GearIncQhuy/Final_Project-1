using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // get data poperties
    public ScripTablePlayer data;
    // get PreFab bullet
    //public GameObject bulletPreFab;

    public ManagerScript manager;
    public Rigidbody rigid;

    public float manaCurrent;
    public float healCurrent;
    public float dame;

    public bool checkMove;

    private void Awake()
    {
        manager = ManagerScript.Ins;

        manaCurrent = data.manaMax;
        healCurrent = data.healMax;
        dame = data.dameMax;
    }

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        Calculate();
    }

    private void LateUpdate()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Kiểm tra xem còn Mana để xử dụng chiêu không
            if(manager.manaPlayer.UseMana(manaCurrent, 1, data.level))
            {
                //manager.shootFunction.Shoot(bulletPreFab, 20f, this.gameObject);
            }
        }
    }

    /**
     * Check xem Player đang dùng vũ khí tương sinh với mình không
     * Nếu tương sinh -> dame tăng 10% (vũ khí tương sinh)
     *     tương khắc -> dame giảm 10% (vũ khí tương khắc)
     *     không tương sinh không tương khắc giữ nguyên
     */
    private void Calculate()
    {
        // Trường hợp 1: Vũ khí tương sinh
        if(manager.nourishmentRestraintFuction.checkMutualNourishment(manager.bullet.data.phases, data.phases)){
            dame = data.dameMax + (manager.bullet.data.baseDame * 1.1f);
        }
        // Trường hợp 2: Vũ khí tương khắc
        else if(manager.nourishmentRestraintFuction.checkMutualRestraint(manager.bullet.data.phases, data.phases))
        {
            dame = data.dameMax + (manager.bullet.data.baseDame * 0.9f);
        }
        // Trường hợp 3: Vũ khí không tương sinh cũng không tương khắc
        else
        {
            dame = data.dameMax + manager.bullet.data.baseDame;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.DrawRay(transform.position, transform.forward * 10f); 
    }

}
