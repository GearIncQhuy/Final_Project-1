using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class Enemy : MonoBehaviour
{
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
        Calculate();

        // Set Enemy Fly
        if(data.tag == Constants.EnemyFly)
        {
            data.tamdanh = ManagerScript.Ins.player.data.tamdanh - 1;
            if (data.tamdanh > 20)
            {
                data.tamdanh = 20;
            }
        }
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

    public DamageNumber expUIPlayer;
    public DamageNumber coinUI;
    private void FixedUpdate()
    {
        // Enemy die => reset poperties Enemy
        if (heal == 0f)
        {
            ResetEnemy();

            Vector3 numberPosition = ManagerScript.Ins.player.transform.position;
            numberPosition.y += 1.3f;
            DamageNumber damageNumber = expUIPlayer.Spawn(numberPosition, data.expEnemy);
            damageNumber.SetScale(1.5f);
            ManagerScript.Ins.expPlayer.UpdateExpPlayerCurrent(data.expEnemy);
            // Random Coin
            if (RandomCoin())
            {
                DamageNumber damage = coinUI.Spawn(numberPosition, 1);
                damage.SetScale(1.5f);
                ManagerScript.Ins.player.data.coin++;
            }
            ObjectPool.Ins.ReturnToPool(data.tag, this.gameObject);
        }

        UpdatePoperties();
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
        if (ManagerScript.Ins.nourishmentRestraintFuction.checkMutualNourishment(data.phases, ManagerScript.Ins.player.data.phases))
        {
            dame = data.dameMax * 0.9f;
        }
        // Trường hợp 2: Enemy tương khắc Player
        else if (ManagerScript.Ins.nourishmentRestraintFuction.checkMutualRestraint(data.phases, ManagerScript.Ins.player.data.phases))
        {
            dame = data.dameMax * 1.5f;
        }
        // Trường hợp 3: Player tương khắc Enemy
        else if (ManagerScript.Ins.nourishmentRestraintFuction.checkMutualRestraint(ManagerScript.Ins.player.data.phases, data.phases))
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
     * Hàm reset lại máu cho enemy để trả lại Pool và nếu Player có tăng level thì quái bay sẽ tăng tầm đánh
     */
    int levelPlayer = 1;
    private void ResetEnemy()
    {
        if(levelPlayer < ManagerScript.Ins.player.data.level)
        {
            if(data.tag == Constants.EnemyFly)
            {
                data.tamdanh += (ManagerScript.Ins.player.data.level - levelPlayer);
                if(data.tamdanh > 20)
                {
                    data.tamdanh = 20;
                }
            }
            levelPlayer = ManagerScript.Ins.player.data.level;
        }
        heal = data.healMax;
    }

    /**
     * Up level Enemy -> +  ware
     */
    private void UpdatePoperties()
    {
        data.healMax = 1000 + 250 * ManagerTimeSet.Ins.level;
        data.dameMax = 100 + 25 * ManagerTimeSet.Ins.level;
        data.expEnemy = 100 + 25 * ManagerTimeSet.Ins.level;
    }

    private bool RandomCoin()
    {
        float number = Random.Range(0f, 100f);
        if(number < 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

/**
 * Định nghĩa loại Enemy
 */
public enum EnemyCategory
{
    Fly,
    Run,
    BOSS
}