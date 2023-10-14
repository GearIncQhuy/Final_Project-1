using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void ResetPoperties()
    {
        ManagerScript.Ins.poolEnemy.Clearreturn();
        ManagerScript.Ins.player.checkPlayerLife = true;
        ManagerScript.Ins.player.endGame = 0;
        ManagerScript.Ins.player.UpdatePopertiesPlayer();
    }
    public void PlayAgain()
    {
        
        ManagerTimeSet.Ins.turn = 1;
        ManagerTimeSet.Ins.timeSet = 0;
        //ManagerTimeSet.Ins.data.level = 1;
        ResetPoperties();
        ObjectPool.Ins.ReturnToPool(Constants.Tag_EndGameUI, this.gameObject);
    }
    public void NewGame()
    {
        
        ManagerTimeSet.Ins.turn = 1;
        ManagerTimeSet.Ins.timeSet = 0;
        ResetPoperties();
        ManagerTimeSet.Ins.data.level = 1;
        ObjectPool.Ins.ReturnToPool(Constants.Tag_EndGameUI, this.gameObject);
    }
    public void HoiSinh()
    {
        //ResetPoperties();
        if(ManagerScript.Ins.player.data.diamon >= 5)
        {
            ResetPoperties();
            ManagerScript.Ins.player.data.diamon -= 5;
            ObjectPool.Ins.ReturnToPool(Constants.Tag_EndGameUI, this.gameObject);
        }
    }
}
