using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToPlayer : MonoBehaviour
{
    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 target = ManagerScript.Ins.player.transform.position - transform.position;
        transform.Translate(target.normalized * 2f * Time.deltaTime);
    }
}
