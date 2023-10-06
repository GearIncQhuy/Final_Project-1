using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private ManagerScript manager;
    [SerializeField] private GameObject ringOfFire;
    
    private Skill_1 skill;

    private float time;

    private void Start()
    {
        manager = ManagerScript.Ins;
        skill = manager.player.GetComponent<Skill_1>();

        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(skill.checkUse == 2)
        {
            GameObject ringFire = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill1_2, transform.position, Quaternion.identity);
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill1, this.gameObject);
        }
        else if(time >= 5f && skill.checkUse == 1)
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill1, this.gameObject);
            time = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill1, this.gameObject);
        }
    }
}
