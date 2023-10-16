using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Khoang cach den Player
    private Vector3 offSet;

    void Start()
    {
        offSet = transform.position - ManagerScript.Ins.player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = ManagerScript.Ins.player.transform.position + offSet;
    }
}
