using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Card")]
public class ScriptTableCard : ScriptableObject
{
    [Header("Icon")]
    public Sprite icon;
    [Header("Poperties")]
    public float dame;
    public float speedFire;
    public float speed;
    public float mana;
    public float heal;
    public int checkUse;
    [Header("Elemental")]
    public Phases phases;
    public Sprite sprite;
    [Header("Price")]
    public int price;
}
