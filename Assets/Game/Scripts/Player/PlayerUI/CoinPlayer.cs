using UnityEngine;
using System.Collections;
using TMPro;

public class CoinPlayer : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI coin;
    private void Update()
    {
        coin.text = ManagerScript.Ins.player.data.coin.ToString() + " GOD";
    }
}

