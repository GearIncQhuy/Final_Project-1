using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private ManagerScript manager;
    // Khoang cach den Player
    private Vector3 offSet;

    void Start()
    {
        manager = ManagerScript.Ins;
        offSet = transform.position - manager.player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = manager.player.transform.position + offSet;
    }
}
