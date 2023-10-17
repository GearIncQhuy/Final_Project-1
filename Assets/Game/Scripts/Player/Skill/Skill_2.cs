using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_2 : MonoBehaviour
{
    #region Poperties
    private PlayerController player;
    [SerializeField] private GameObject circle;
    [SerializeField] private Image imgSkill;
    private ManaPlayer manaPlayer;
    private Animator animator;
    private bool startTime;
    private float timeStart;
    #endregion

    private void Awake()
    {
        startTime = false;
        timeStart = 0f;
    }
    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        manaPlayer = player.GetComponent<ManaPlayer>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        // time hồi chiêu sau 10s
        if (startTime)
        {
            timeStart += Time.deltaTime;
            if(timeStart >= 5f)
            {
                player.checkHeal = false;
            }
            if(timeStart >= 10f)
            {
                timeStart = 0f;
                startTime = false;
            }
            imgSkill.fillAmount = 1 - timeStart / 10f;
            if(imgSkill.fillAmount <= 0)
            {
                imgSkill.fillAmount = 0;
            }
        }
        else
        {
            imgSkill.fillAmount = 0f;
        }

        //
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSkill();
        }
    }

    #region Button Skill 2
    public void UseSkill()
    {
        if (player.checkPlayerLife && timeStart == 0f)
        {
            if (manaPlayer.UseMana(player.manaCurrent, 2, player.data.level))
            {
                imgSkill.fillAmount = 1f;
                animator.SetBool(Constants.Tag_Player_Run, false);
                GameObject skill2 = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill2, transform.position, Quaternion.identity);
                player.checkHeal = true;
                player.DontMove = true;
                startTime = true;
            }
        }
    }
    #endregion
}
