using UnityEngine;
using UnityEngine.UI;

public class ManaPlayer : MonoBehaviour
{
    #region Poperties Default 
    public Slider manaSlider;
    private PlayerController player;
    #endregion

    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    #region Use Mana 
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
            player.manaCurrent = (int)(manaPlayer - manaNeed);
            return true;
        }
        return false;
    }
    #endregion

    #region Calculate Mana Skill 
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
    #endregion
}
