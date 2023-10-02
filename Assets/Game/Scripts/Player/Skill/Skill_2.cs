using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private GameObject circle;
    private ManaPlayer manaPlayer;
    private bool startTime;
    private float timeStart;
    private void Awake()
    {
        startTime = false;
        timeStart = 0f;
    }
    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        manaPlayer = player.GetComponent<ManaPlayer>();
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
        if (Input.GetKeyDown(KeyCode.Alpha2) && timeStart == 0f)
        {
            UseSkill();
        }
    }

    private void UseSkill()
    {
        if(manaPlayer.UseMana(player.manaCurrent, 2, player.data.level))
        {
            GameObject skill2 = Instantiate(circle, transform.position, Quaternion.identity);
            player.checkHeal = true;
            startTime = true;
        }
    }
}
