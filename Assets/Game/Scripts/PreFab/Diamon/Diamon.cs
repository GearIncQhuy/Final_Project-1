using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            ManagerScript.Ins.player.data.diamon++;
            ObjectPool.Ins.ReturnToPool(Constants.Diamon, this.gameObject);
        }
    }
}