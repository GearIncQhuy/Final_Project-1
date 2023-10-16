using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Check Player Life
    public bool checkPlayerLife;
    public bool checkBatTu = false;
    #endregion

    #region Slider 
    // Get Slider Canvas
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Slider healSlider;
    #endregion

    #region Poperties Player Default
    // get data poperties
    public ScripTablePlayer data;
    public ManagerScript manager;
    public UpLevelPlayer uplevel;
    public Rigidbody rigid;

    // Poperties current
    public int manaCurrent;
    public int healCurrent;
    public int dame;
    public int tamdanh;
    public int speedCurrent;
    //
    [SerializeField] private List<ScriptTableCard> listCards = new List<ScriptTableCard>();
    private int manaBonus;
    private int healBonus;
    private int dameBonus;
    private int tamdanhBonus;
    private int speedBonus;

    // check move
    public bool checkMove;

    // check heal
    public bool checkHeal;

    // check don't move
    public bool DontMove;
    #endregion

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
        if(data.level > 50)
        {
            data.level = 50;
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

    public int endGame = 0;
    private void LateUpdate()
    {
        if(healCurrent <= 0)
        {
            checkPlayerLife = false;
            // PreFab end Game
            if(endGame == 0)
            {
                ObjectPool.Ins.SpawnFromPool(Constants.Tag_EndGameUI, transform.position, Quaternion.identity);
                endGame++;
            }
        }
    }

    private float timeRetoreMana = 0f;
    private float timeRetoreHeal = 0f;
    private void Update()
    {
        if (checkPlayerLife)
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
                if (timeRetoreHeal >= 1f)
                {
                    RestoreHeal();
                    timeRetoreHeal = 0f;
                }
            }
        }
    }

    #region Calculate Dame Player
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
            dame = (int)(data.dameMax + (manager.bullet.data.baseDame * 1.1f));
        }
        // Trường hợp 2: Vũ khí tương khắc
        else if(manager.nourishmentRestraintFuction.checkMutualRestraint(manager.bullet.data.phases, data.phases))
        {
            dame = (int)(data.dameMax + (manager.bullet.data.baseDame * 0.9f));
        }
        // Trường hợp 3: Vũ khí không tương sinh cũng không tương khắc
        else
        {
            dame = (int)(data.dameMax + manager.bullet.data.baseDame);
        }
    }
    #endregion

    #region Gizmos Test
    /**
     */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.DrawRay(transform.position, transform.forward * 10f); 
    }
    #endregion

    #region Get Dame Player
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
    #endregion

    #region Restore Mana Player (1%/s)
    /**
     * Hàm hồi lại mana mỗi giây
     */
    private void RestoreMana()
    {
        manaCurrent += (int)(data.manaMax * 0.01f);
        if (manaCurrent > data.manaMax)
        {
            manaCurrent = data.manaMax;
        }
        manaSlider.value = manaCurrent / data.manaMax;
    }
    #endregion

    #region Retore Heal Player (5%/s)
    /**
     * 
     */
    private void RestoreHeal()
    {
        healCurrent += (int)(data.healMax * 0.05f);
        if(healCurrent > data.healMax)
        {
            healCurrent = data.healMax;
        }
        healSlider.value = healCurrent / data.healMax;
    }
    #endregion

    #region Update Poperties Player 
    public void UpdatePopertiesPlayer()
    {
        if (listCards.Count > 0)
        {
            for (int i = 0; i < listCards.Count; i++)
            {
                dameBonus += (int)(listCards[i].dame * listCards[i].checkUse);
                manaBonus += (int)(listCards[i].mana * listCards[i].checkUse);
                healBonus += (int)(listCards[i].heal * listCards[i].checkUse);
                speedBonus += (int)(listCards[i].speed * listCards[i].checkUse);
                tamdanhBonus += (int)(listCards[i].speedFire * listCards[i].checkUse);
            }
        }
        manaCurrent = data.manaMax + manaBonus;
        healCurrent = data.healMax + healBonus;
        dame = data.dameMax + dameBonus;
        tamdanh = data.tamdanh + tamdanhBonus;
        speedCurrent = data.speed + speedBonus;

        healSlider.value = 1;
        manaSlider.value = 1;
    }
    #endregion
}
