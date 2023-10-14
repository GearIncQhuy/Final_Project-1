using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpPlayer : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI text;
    /**
     * 
     */
    public float expCurrent = 0f;
    public void UpdateExpPlayerCurrent(float expEnemy)
    {
        if(ManagerScript.Ins.player.data.level <= 50)
        {
            expCurrent += expEnemy;
        }
        if (expCurrent > ManagerScript.Ins.player.data.expMax && ManagerScript.Ins.player.data.level <= 50)
        {
            ManagerScript.Ins.player.uplevel.UpLevel();
            expCurrent = 0f;
            ManagerScript.Ins.player.healCurrent = ManagerScript.Ins.player.data.healMax;
            ManagerScript.Ins.player.manager.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, 0);
        }
        float sliderExpValue = expCurrent / ManagerScript.Ins.player.data.expMax;
        expSlider.value = sliderExpValue;
        text.text = expCurrent + "/" + ManagerScript.Ins.player.data.expMax;
    }
}
