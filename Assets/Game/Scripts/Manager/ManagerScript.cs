using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : Singleton<ManagerScript>
{
    // GameObject
    public PlayerController player;
    public BulletTest bullet;

    // Function
    public NourishmentRestraintFuction nourishmentRestraintFuction;
    public SetColorDame colorDame;

    // Status Player
    public ManaPlayer manaPlayer;
    public HealPlayer healPlayer;
    public ExpPlayer  expPlayer;

    // Pooling Object
    public PoolEnemy poolEnemy;
}