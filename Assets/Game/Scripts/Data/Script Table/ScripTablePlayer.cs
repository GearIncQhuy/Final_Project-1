using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Player")]
public class ScripTablePlayer : ScriptableObject
{
    [Header("Poperties")]
    public float speed;
    public float jumpForce;

    public float dameMax;
    public float healMax;
    public float manaMax;

    public int level;
    [Header("Elemental")]
    public Phases phases;
}
