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

    //public Transform target; // đối tượng player (người chơi)
    //public float smoothSpeed = 0.125f; // tốc độ di chuyển camera
    //public Vector3 offset; // khoảng cách giữa camera và player
    //private void Awake()
    //{
    //}
    //void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        Vector3 desiredPosition = target.position + offset; // vị trí camera cần đến
    //        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // di chuyển camera một cách mượt mà
    //        transform.position = smoothedPosition; // gán vị trí camera hiện tại thành vị trí mới
    //    }
    //}


}
