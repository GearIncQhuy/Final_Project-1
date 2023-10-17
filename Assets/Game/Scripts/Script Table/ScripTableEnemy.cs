using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Enemy")]
public class ScripTableEnemy : ScriptableObject
{
    [Header("Poperties")]
    public float speed;
    public float tamdanh;
    public float speedFire;

    public float dameMax;
    public float healMax;

    public float expEnemy;

    [Header("Elemental")]
    public Phases phases;

    [Header("Category")]
    public EnemyCategory category;
    public string tag;
}
