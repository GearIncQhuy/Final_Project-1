using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : MonoBehaviour
{
    #region Poperties
    private PlayerController player;
    [SerializeField] private GameObject circle;
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
                animator.SetBool("isRun", false);
                GameObject skill2 = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill2, transform.position, Quaternion.identity);
                player.checkHeal = true;
                player.DontMove = true;
                startTime = true;
            }
        }
    }
    #endregion
}
