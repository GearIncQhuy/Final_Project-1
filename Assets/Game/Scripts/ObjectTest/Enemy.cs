using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private ManagerScript manager;

    [SerializeField]
    public GameObject sliderObj;

    public Slider slider;
    public ScripTableEnemy data;

    // Thông số ban đầu
    private float heal;
    private float dame;

    // Quản lý cho thanh máu bật tắt
    private bool checkActive;
    private float timeActive;

    private void Awake()
    {
        // Thông số ban đầu
        heal = data.healMax;
        manager = ManagerScript.Ins;
        checkActive = false;
        timeActive = 0f;
    }

    private void Start()
    {
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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm viên đạn -> hiển thị thanh máu và cập nhật máu
        if (other.gameObject.CompareTag(Constants.Tag_Bullet))
        {
            UpdateHealEnemy(heal, manager.player.dame);
            checkActive = true;
            sliderObj.SetActive(true);
        }

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

    /**
     * Hàm update lại thanh máu cho Enemy
     * @param: healCurrentEnemy : máu hiện tại của Enemy
     * @param: dame : dame nhận vào từ Player hoặc tác nhân bên ngoài
     * note: heal slider = (healCurrentEnemy - dame) / data.healMax
     */
    private void UpdateHealEnemy(float healCurrentEnemy, float dame)
    {
        float healCurrent = (healCurrentEnemy - dame) / data.healMax;
        // update thanh máu
        slider.value = healCurrent;
        // update máu hiện tại của Enemy
        heal = healCurrentEnemy - dame;
    }
}