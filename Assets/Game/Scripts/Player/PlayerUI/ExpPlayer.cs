using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpPlayer : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    /**
     * 
     */
    public float expCurrent = 0f;
    public void UpdateExpPlayerCurrent(float expEnemy)
    {
        expCurrent += expEnemy;
        if (expCurrent > ManagerScript.Ins.player.data.expMax)
        {
            ManagerScript.Ins.player.uplevel.UpLevel();
            expCurrent = 0f;
            ManagerScript.Ins.player.healCurrent = ManagerScript.Ins.player.data.healMax;
            ManagerScript.Ins.player.manager.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, 0);
        }
        float sliderExpValue = expCurrent / ManagerScript.Ins.player.data.expMax;
        expSlider.value = sliderExpValue;
    }
}
