using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPlayer : MonoBehaviour
{
    public Slider healSlider;
    private ManagerScript manager;

    private void Start()
    {
        manager = ManagerScript.Ins;
    }

    public void UpdateHealPlayer(float healCurrentPlayer, float dame)
    {
        float healCurrent = (healCurrentPlayer - dame) / manager.player.data.healMax;
        healSlider.value = healCurrent;
        manager.player.healCurrent = healCurrentPlayer - dame;
    }
}
