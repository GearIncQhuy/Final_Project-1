using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public ManagerScript manager;
    public ScripTableEnemy data;
    public GameObject sliderObj;
    // Thông số ban đầu
    public float heal;
    public float dame;

    // Quản lý cho thanh máu bật tắt
    public bool checkActive;
    private float timeActive;

    private void Awake()
    {
        // Thông số ban đầu
        heal = data.healMax;
        checkActive = false;
        timeActive = 0f;
    }

    private void Start()
    {
        manager = ManagerScript.Ins;
        Calculate();
    }

    private void Update()
    {
        // Kiểm tra xem thanh máu đã SetActive true chưa -> đếm tim sau 0.5s thì tắt -> reset time về 0
        if (checkActive)
        {
            timeActive += Time.deltaTime;
            if (timeActive >= 0.5f)
            {
                checkActive = false;
                timeActive = 0f;
                sliderObj.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm Player -> chuyển đánh Player gây dame vào máu Player
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            manager.healPlayer.UpdateHealPlayer(manager.player.healCurrent, dame);
        }
    }

    /**
     * Hàm Calculate tính dame có thể gây ra cho Player
     * note: tương sinh với Player thì dame giảm đi 10%, 
     *       tương khắc với Player tăng 50%, 
     *       bị Player tương khắc giảm 50%,
     *       không tương sinh, không tương khắc giữ nguyên dame mặc định
     */
    private void Calculate()
    {
        // Kiểm tra xem Enemy và Player cái nào sinh khắc cái nào
        // Trường hợp 1: Enemy tương sinh Player
        if (manager.nourishmentRestraintFuction.checkMutualNourishment(data.phases, manager.player.data.phases))
        {
            dame = data.dameMax * 0.9f;
        }
        // Trường hợp 2: Enemy tương khắc Player
        else if (manager.nourishmentRestraintFuction.checkMutualRestraint(data.phases, manager.player.data.phases))
        {
            dame = data.dameMax * 1.5f;
        }
        // Trường hợp 3: Player tương khắc Enemy
        else if (manager.nourishmentRestraintFuction.checkMutualRestraint(manager.player.data.phases, data.phases))
        {
            dame = data.dameMax * 0.5f;
        }
        // Trường hợp 4: Player và Enemy không sinh khắc với nhau
        else
        {
            dame = data.dameMax;
        }
    }
}