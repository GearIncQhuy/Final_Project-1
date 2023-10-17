using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    #region Reset Poperties Player
    private void ResetPoperties()
    {
        ManagerScript.Ins.poolEnemy.Clearreturn();
        ManagerScript.Ins.player.checkPlayerLife = true;
        ManagerScript.Ins.player.endGame = 0;
        ManagerScript.Ins.player.UpdatePopertiesPlayer();
    }
    #endregion

    #region Button Play Again
    public void PlayAgain()
    {
        ManagerScript.Ins.poolEnemy.Clearreturn();
        SceneManager.LoadScene(Constants.Scene_GamePlay);
        ObjectPool.Ins.ReturnToPool(Constants.Tag_EndGameUI, this.gameObject);
    }
    #endregion

    #region Button New Game
    public void NewGame()
    {
        ManagerScript.Ins.poolEnemy.Clearreturn();
        ManagerTimeSet.Ins.timeSet = 0;
        StartCoroutine(ResetGame());
    }
    #endregion

    #region Button Revival
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
    #endregion

    #region Reset Game
    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2f);
        ManagerScript.Ins.player.UpdatePopertiesPlayer();
        ManagerTimeSet.Ins.data.level = 1;
        SceneManager.LoadScene(Constants.Scene_GamePlay);
        ObjectPool.Ins.ReturnToPool(Constants.Tag_EndGameUI, this.gameObject);
    }
    #endregion
}
