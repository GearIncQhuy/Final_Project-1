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
        sceneNextLevel.SetActive(false);
    }
}
