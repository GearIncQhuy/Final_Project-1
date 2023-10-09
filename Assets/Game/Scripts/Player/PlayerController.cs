using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool checkPlayerLife;
    public bool checkBatTu = false;
    // get data poperties
    public ScripTablePlayer data;
    public ManagerScript manager;
    public UpLevelPlayer uplevel;
    public Rigidbody rigid;

    // Get Slider Canvas
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Slider healSlider;

    // Poperties current
    public float manaCurrent;
    public float healCurrent;
    public float dame;
    public float tamdanh;
    public float speedCurrent;
    //
    [SerializeField] private List<ScriptTableCard> listCards = new List<ScriptTableCard>();
    private float manaBonus;
    private float healBonus;
    private float dameBonus;
    private float tamdanhBonus;
    private float speedBonus;

    // check move
    public bool checkMove;

    // check heal
    public bool checkHeal;

    // check don't move
    public bool DontMove;

    private void Awake()
    {
        // Set default poperties Player -> Continue Game
        data.manaMax = 200 + data.level * 50;
        data.healMax = 2000 + data.level * 200;
        data.dameMax = 200 + data.level * 50;
        data.tamdanh = 15 + data.level * 1;

        if(data.tamdanh > 25)
        {
            data.tamdanh = 25;
        }

        data.expMax = 1000 + data.level * 500;

        // Set new Game
        checkPlayerLife = true;
        DontMove = false;
    }

    private void Start()
    {
        manager = ManagerScript.Ins;
        rigid = gameObject.GetComponent<Rigidbody>();
        uplevel = gameObject.GetComponent<UpLevelPlayer>();
        UpdatePopertiesPlayer();
        Calculate();
    }

    private void LateUpdate()
    {
        if(healCurrent <= 0)
        {
            checkPlayerLife = false;
        }
    }

    private float timeRetoreMana = 0f;
    private float timeRetoreHeal = 0f;
    private void Update()
    {
        timeRetoreMana += Time.deltaTime;
        timeRetoreHeal += Time.deltaTime;
        // Hồi mana theo giây
        if (timeRetoreMana >= 1f)
        {
            RestoreMana();
            timeRetoreMana = 0f;
        }
        // Hồi heal theo giây
        if (checkHeal)
        {
            if(timeRetoreHeal >= 1f)
            {
                RestoreHeal();
                timeRetoreHeal = 0f;
            }
        }
    }

    /**
     * Check xem Player đang dùng vũ khí tương sinh với mình không
     * Nếu tương sinh -> dame tăng 10% (vũ khí tương sinh)
     *     tương khắc -> dame giảm 10% (vũ khí tương khắc)
     *     không tương sinh không tương khắc giữ nguyên
     */
    public void Calculate()
    {
        // Trường hợp 1: Vũ khí tương sinh
        if(manager.nourishmentRestraintFuction.checkMutualNourishment(manager.bullet.data.phases, data.phases)){
            dame = data.dameMax + (manager.bullet.data.baseDame * 1.1f);
        }
        // Trường hợp 2: Vũ khí tương khắc
        else if(manager.nourishmentRestraintFuction.checkMutualRestraint(manager.bullet.data.phases, data.phases))
        {
            dame = data.dameMax + (manager.bullet.data.baseDame * 0.9f);
        }
        // Trường hợp 3: Vũ khí không tương sinh cũng không tương khắc
        else
        {
            dame = data.dameMax + manager.bullet.data.baseDame;
        }
    }

    /**
     */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.DrawRay(transform.position, transform.forward * 10f); 
    }

    /**
     * Hàm tính dame cho Player sử dụng
     */
    public float GetDamePlayer(int skill, int level, bool check)
    {
        if(skill == 1)
        {
            float damePlayer = dame + (dame * 0.4f);
            if (check)
            {
                return damePlayer;
            }
            // dame lan toả (= 50% dame max)
            return damePlayer / 2;
        }else if(skill == 2)
        {
            return dame / 2;
        }
        return 0;
    }

    /**
     * Hàm hồi lại mana mỗi giây
     */
    private void RestoreMana()
    {
        manaCurrent += (data.manaMax * 0.01f);
        if (manaCurrent > data.manaMax)
        {
            manaCurrent = data.manaMax;
        }
        manaSlider.value = manaCurrent / data.manaMax;
    }

    /**
     * 
     */
    private void RestoreHeal()
    {
        healCurrent += (data.healMax * 0.05f);
        if(healCurrent > data.healMax)
        {
            healCurrent = data.healMax;
        }
        healSlider.value = healCurrent / data.healMax;
    }

    public void UpdatePopertiesPlayer()
    {
        if (listCards.Count > 0)
        {
            for (int i = 0; i < listCards.Count; i++)
            {
                dameBonus += listCards[i].dame * listCards[i].checkUse;
                manaBonus += listCards[i].mana * listCards[i].checkUse;
                healBonus += listCards[i].heal * listCards[i].checkUse;
                speedBonus += listCards[i].speed * listCards[i].checkUse;
                tamdanhBonus += listCards[i].speedFire * listCards[i].checkUse;
            }
        }
        manaCurrent = data.manaMax + manaBonus;
        healCurrent = data.healMax + healBonus;
        dame = data.dameMax + dameBonus;
        tamdanh = data.tamdanh + tamdanhBonus;
        speedCurrent = data.speed + speedBonus;
    }
}
