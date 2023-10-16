using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Poperties
    private PlayerController player;
    private Rigidbody rigid;
    [SerializeField] private FloatingJoystick joystick;
    private Animator animator;
    #endregion

    //public float rotationSpeed;
    private void Awake()
    {
        player = gameObject.GetComponent<PlayerController>();
        rigid = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        MovePlayer();
    }

    public float rotationSpeed;
    #region Player Move Use PC, Laptop
    /**
     * Hàm di chuyển Player trên PC
     */
    //private void MovePlayer()
    //{
    //    float hori = Input.GetAxis("Horizontal");
    //    float ver = Input.GetAxis("Vertical");
    //    Vector3 movement = new Vector3(hori, 0.0f, ver);
    //    movement.Normalize();

    //    if (movement != Vector3.zero)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);

    //        // Sử dụng Quaternion.Slerp để xoay từ góc hiện tại đến góc mục tiêu
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //    }
    //    if (!player.DontMove)
    //    {
    //        // Check move cho attack
    //        if (hori != 0 || ver != 0)
    //        {
    //            player.checkMove = true;
    //        }
    //        else if (hori == 0 && ver == 0)
    //        {
    //            player.checkMove = false;
    //        }

    //        // Di chuyển đối tượng
    //        transform.Translate(movement * player.data.speed * Time.deltaTime, Space.World);
    //    }
    //}
    #endregion

    #region Player Move Use Phone
    /**
     * Hàm di chuyển theo joystick
     */
    private Vector3 _movement;
    private void MovePlayer()
    {
        _movement = Vector3.zero;
        _movement.x = joystick.Horizontal * player.data.speed * Time.deltaTime;
        _movement.z = joystick.Vertical * player.data.speed * Time.deltaTime;
        if (!player.DontMove)
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                animator.SetBool(Constants.Tag_Player_Run, true);
                Vector3 direction = Vector3.RotateTowards(transform.forward, _movement, player.speedCurrent * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
                player.checkMove = true;
                // Animation Run
            }
            else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                animator.SetBool(Constants.Tag_Player_Run, false);
                player.checkMove = false;
                // Animation Idle
            }
            rigid.MovePosition(rigid.position + _movement);
        }
    }
    #endregion
}
