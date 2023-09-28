using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTest : MonoBehaviour
{
    private float heal = 1000;
    private ManagerScript manager;
    private void Start()
    {
        manager = ManagerScript.Ins;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            heal -= manager.player.dame;
            Debug.Log("Heal test: " + heal);
        }
    }
}
