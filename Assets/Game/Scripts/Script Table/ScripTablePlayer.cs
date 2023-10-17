using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Player")]
public class ScripTablePlayer : ScriptableObject
{
    [Header("Poperties")]
    public int speed;
    public int jumpForce;
    public int expMax;
    public int tamdanh;

    public int dameMax;
    public int healMax;
    public int manaMax;

    public int level;

    [Header("Elemental")]
    public Phases phases;

    [Header("Coin")]
    public int coin;
    public int diamon;
}
