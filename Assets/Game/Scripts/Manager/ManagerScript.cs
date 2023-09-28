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
    public ShootFunction shootFunction;
}
