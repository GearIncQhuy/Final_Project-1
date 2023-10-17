using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    #region Poperties
    [SerializeField] private GameObject sceneNextLevel;
    [SerializeField] private GameObject sceneLevelAgain;
    [SerializeField] private TextMeshProUGUI time;
    #endregion

    #region Button Next Level
    public void ClickNextLevel()
    {
        ManagerTimeSet.Ins.data.level++;
        if(ManagerTimeSet.Ins.data.level > 20)
        {
            ManagerMap.Ins.NextMap();
            ManagerTimeSet.Ins.data.level = 1;
        }
        SceneManager.LoadScene(Constants.Scene_GamePlay);
    }
    #endregion

    #region Button Return Level
    public void ClickReturnLevel()
    {
        ManagerTimeSet.Ins.timeSet = 0;
        ManagerTimeSet.Ins.checkSpawn = true;
        ManagerTimeSet.Ins.data.level = 1;
        sceneNextLevel.SetActive(false);
        sceneLevelAgain.SetActive(false);
        ManagerScript.Ins.player.checkBatTu = false;
    }
    #endregion
}
