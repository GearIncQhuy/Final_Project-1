using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaPlayer : MonoBehaviour
{
    public Slider manaSlider;
    private ManagerScript manager;
    private void Start()
    {
        manager = ManagerScript.Ins;
    }

    public bool UseMana(float manaPlayer, int numberSkill, int level)
    {
        float manaNeed = ManaSkill(level, numberSkill);
        if(manaPlayer >= manaNeed)
        {
            float manaCurrent = (manaPlayer - manaNeed) / manager.player.data.manaMax;
            manaSlider.value = manaCurrent;
            manager.player.manaCurrent = manaPlayer - manaNeed;
            return true;
        }
        return false;
    }

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
