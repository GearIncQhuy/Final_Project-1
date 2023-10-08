using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSetEndTurn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI round;
    private void LateUpdate()
    {
        if (ManagerTimeSet.Ins.checkSpawn && ManagerTimeSet.Ins.level < 20)
        {
            int timeEnd = (int)ManagerTimeSet.Ins.timeEndTurn;
            int timeSet = ManagerTimeSet.Ins.timeSet;
            if(timeSet == 0)
            {
            }
            else
            {
                int timeUI = timeEnd - timeSet;
                time.text = timeUI.ToString();
            }
            round.text = "Round: " + ManagerTimeSet.Ins.data.level.ToString();
        }
            
    }
}
