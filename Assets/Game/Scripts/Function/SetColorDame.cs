using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorDame : MonoBehaviour
{
    public Color colorFire;
    public Color colorEarth;
    public Color colorMetal;
    public Color colorWater;
    public Color colorWood;

    public PlayerController player;

    public Color GetColorPlayerDame()
    {
        if(player.data.phases == Phases.Fire)
        {
            return colorFire;
        }
        else if (player.data.phases == Phases.Earth)
        {
            return colorEarth;
        }
        else if (player.data.phases == Phases.Metal)
        {
            return colorMetal;
        }
        else if (player.data.phases == Phases.Water)
        {
            return colorWater;
        }
        else
        {
            return colorWood;
        }
    }
}
