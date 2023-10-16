using UnityEngine;
using UnityEngine.UI;

public class ExpPlayer : MonoBehaviour
{
    #region Update Exp Player Current
    [SerializeField] private Image expSlider;
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
        expSlider.fillAmount = sliderExpValue;
    }
    #endregion
}
