using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSetEndTurn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    private void LateUpdate()
    {
        if (ManagerTimeSet.Ins.checkSpawn)
        {
            int timeEnd = (int)ManagerTimeSet.Ins.timeEndTurn;
            int timeSet = ManagerTimeSet.Ins.timeSet;
            if(timeSet == 0)
            {
                time.text = "0";
            }
            else
            {
                int timeUI = timeEnd - timeSet;
                time.text = timeUI.ToString();
            }
        }
            
    }
}
