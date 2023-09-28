using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Player")]
public class ScripTablePlayer : ScriptableObject
{
    [Header("Poperties")]
    public float speed;
    public float baseDame;
    [Header("Elemental")]
    public Phases phases;
}
