using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLevelPlayer : MonoBehaviour
{
    private PlayerController player;
    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }
    public void UpLevel()
    {
        player.data.manaMax += 100;
        player.data.healMax += 500;
        player.data.expMax += 500;
        player.data.level += 1;
        player.data.tamdanh += 2;
        player.data.dameMax += 50;
        player.tamdanh = player.data.tamdanh;
        player.Calculate();
    }

}
