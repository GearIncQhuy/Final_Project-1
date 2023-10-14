using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealPlayer : MonoBehaviour
{
    public Slider healSlider;
    [SerializeField] private TextMeshProUGUI text;

    /**
     * Hàm update heal cho Player
     * @param: healCurrenPlayer float (lượng máu hiện tại của Player)
     * @param: dame (dame nhận vào từ các phía)
     */
    public void UpdateHealPlayer(float healCurrentPlayer, float dame)
    {
        float healCurrent = (healCurrentPlayer - dame) / ManagerScript.Ins.player.data.healMax;
        healSlider.value = healCurrent;
        ManagerScript.Ins.player.healCurrent = healCurrentPlayer - dame;
        text.text = ManagerScript.Ins.player.healCurrent + "/" + ManagerScript.Ins.player.data.healMax;
    }
}
