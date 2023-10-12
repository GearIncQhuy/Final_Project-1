using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Boss : MonoBehaviour
{
    [SerializeField] private BossMap1 boss;
    [SerializeField] private GameObject skill2;
    private float time = 0f;
    private int endTime = 0;
    private bool checkTrigger = false;
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 1)
        {
            endTime++;
            if (checkTrigger)
            {
                ManagerScript.Ins.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, boss.data.dameMax * 2);
            }
            time = 0f;
        }
        if(endTime > 5)
        {
            boss.currentState = BossMap1.BossMap1State.Move;
            endTime = 0;
            time = 0;
            skill2.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            checkTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        checkTrigger = false;
    }
}
