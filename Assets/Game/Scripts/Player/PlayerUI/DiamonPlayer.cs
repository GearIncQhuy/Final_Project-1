using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamonPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamon;
    private void Update()
    {
        diamon.text = ManagerScript.Ins.player.data.diamon.ToString();
    }
}
