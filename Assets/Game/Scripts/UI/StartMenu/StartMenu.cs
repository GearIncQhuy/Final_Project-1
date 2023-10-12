using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private ScripTablePlayer playerData;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI level;
    // Start is called before the first frame update
    void Start()
    {
        coin.text = playerData.coin.ToString();
        level.text = playerData.level.ToString();
    }
}
