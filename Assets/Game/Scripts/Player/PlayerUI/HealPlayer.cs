using UnityEngine;
using UnityEngine.UI;

public class HealPlayer : MonoBehaviour
{
    #region Update Heal Player 
    public Slider healSlider;
    /**
     * Hàm update heal cho Player
     * @param: healCurrenPlayer float (lượng máu hiện tại của Player)
     * @param: dame (dame nhận vào từ các phía)
     */
    public void UpdateHealPlayer(float healCurrentPlayer, float dame)
    {
        float healCurrent = (healCurrentPlayer - dame) / ManagerScript.Ins.player.data.healMax;
        healSlider.value = healCurrent;
        ManagerScript.Ins.player.healCurrent = (int)(healCurrentPlayer - dame);
    }
    #endregion
}
