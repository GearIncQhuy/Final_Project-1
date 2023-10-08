using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject sceneNextLevel;
    [SerializeField] private TextMeshProUGUI time;
    public void ClickNextLevel()
    {
        ManagerTimeSet.Ins.timeSet = 0;
        ManagerTimeSet.Ins.checkSpawn = true;
        ManagerTimeSet.Ins.data.level++;
        if(ManagerTimeSet.Ins.data.level > 20)
        {
            ManagerTimeSet.Ins.data.level = 1;
        }
        sceneNextLevel.SetActive(false);
        ManagerScript.Ins.player.checkBatTu = false;
    }
}
