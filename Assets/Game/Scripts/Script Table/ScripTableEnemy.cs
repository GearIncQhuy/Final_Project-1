using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Enemy")]
public class ScripTableEnemy : ScriptableObject
{
    [Header("Poperties")]
    public float dameMax;
    public float healMax;

    [Header("Elemental")]
    public Phases phases;
}
