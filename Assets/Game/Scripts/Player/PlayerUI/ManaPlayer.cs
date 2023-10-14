using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaPlayer : MonoBehaviour
{
    public Slider manaSlider;
    private PlayerController player;
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    /**
     * Hàm sử dụng mana (kiểm tra và thực hiện trừ mana khi sử dụng chiêu thức)
     * @param: manaPlayer lượng mana hiện tại của Player
     * @param: numberSkill chiêu thức sử dụng
     * @param: level hiện tại của Player
     * return true, false
     * Kiểm tra và cập nhật lại thanh máu
     */
    public bool UseMana(float manaPlayer, int numberSkill, int level)
    {
        float manaNeed = ManaSkill(level, numberSkill);
        if(manaPlayer >= manaNeed)
        {
            float manaCurrent = (manaPlayer - manaNeed) / player.data.manaMax;
            manaSlider.value = manaCurrent;
            player.manaCurrent = manaPlayer - manaNeed;
            text.text = player.manaCurrent + "/" + player.data.manaMax;
            return true;
        }
        return false;
    }

    /**
     * Hàm tính toán mana sử dụng
     * @param: level : level hiện tại của Player
     * @param: numberSkill số chiêu để tính
     * mana tính theo: mana - level hiện tại và mana * chiêu thức 
     */
    private float ManaSkill(int level, int numberSkill)
    {
        float manaBase = 25f;
        manaBase *= numberSkill;
        if(level == 1)
        {
            return manaBase;
        }
        manaBase -= level;
        return manaBase;
    }
}
