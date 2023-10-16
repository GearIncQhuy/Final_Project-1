using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    #region Poperties
    [SerializeField] private GameObject ringOfFire;
    
    private Skill_1 skill;

    private float time;
    #endregion

    private void Start()
    {
        skill = ManagerScript.Ins.player.GetComponent<Skill_1>();

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

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Enemy))
        {
            ObjectPool.Ins.ReturnToPool(Constants.Tag_Skill1, this.gameObject);
        }
    }
    #endregion
}
