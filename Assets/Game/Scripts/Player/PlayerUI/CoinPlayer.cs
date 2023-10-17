using UnityEngine;
using System.Collections;
using TMPro;

public class CoinPlayer : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI level;
    private void Update()
    {
        coin.text = ManagerScript.Ins.player.data.coin.ToString();
        level.text = ManagerScript.Ins.player.data.level.ToString();
    }
}

