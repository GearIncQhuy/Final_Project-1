using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjTest : MonoBehaviour
{
    private ManagerScript manager;
    public GameObject sliderObj;
    public Slider slider;
    public ScripTableEnemy data;

    private float heal;
    private float dame;

    private bool checkActive;
    private float timeActive;

    private void Start()
    {
        heal = data.healMax;
        manager = ManagerScript.Ins;
        Calculate();
        checkActive = false;
        timeActive = 0f;
    }

    private void Update()
    {
        if (checkActive)
        {
            timeActive += Time.deltaTime;
            if(timeActive >= 0.5f)
            {
                checkActive = false;
                timeActive = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Bullet))
        {
            UpdateHealEnemy(heal, manager.player.dame);
            checkActive = true;
            sliderObj.SetActive(true);
        }
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            manager.healPlayer.UpdateHealPlayer(manager.player.healCurrent, dame);
        }
    }

    private void Calculate()
    {
        if (manager.nourishmentRestraintFuction.checkMutualNourishment(data.phases, manager.player.data.phases))
        {
            dame = data.dameMax * 0.5f;
        }
        else if (manager.nourishmentRestraintFuction.checkMutualRestraint(data.phases, manager.player.data.phases))
        {
            dame = data.dameMax * 1.5f;
        }
        else
        {
            dame = data.dameMax;
        }
    }

    private void UpdateHealEnemy(float healCurrentEnemy, float dame)
    {
        float healCurrent = (healCurrentEnemy - dame) / data.healMax;
        slider.value = healCurrent;
        heal = healCurrentEnemy - dame;
    }
}
