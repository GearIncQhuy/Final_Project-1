using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : MonoBehaviour
{
    [SerializeField] private Transform hard;
    private PlayerController player;
    private ManaPlayer manaPlayer;
    [SerializeField] private GameObject fireBallPreFab;

    public int checkUse;
    private float timeStart;
    private bool timeReuse;
    private bool startTime;

    private void Awake()
    {
        checkUse = 0;
        timeStart = 0f;
        timeReuse = false;
        startTime = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        manaPlayer = gameObject.GetComponent<ManaPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            timeStart += Time.deltaTime;
            timeReuse = true;
            if(timeStart >= 2f)
            {
                timeReuse = false;
            }
            if(timeStart >= 5f)
            {
                timeStart = 0f;
                startTime = false;
                checkUse = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && timeStart <= 4f)
        {
            if (checkUse == 0)
            {
                startTime = true;
                UseSkill();
                checkUse++;
            }
            if(checkUse == 1 && timeReuse)
            {
                timeReuse = false;
                checkUse++;
            }
        }

        if(checkUse == 2 && !startTime)
        {
            checkUse = 0;
        }
    }

    /**
     * Sử dụng chiêu ở lần bắn thứ nhất
     */
    private void UseSkill()
    {
        if (manaPlayer.UseMana(player.manaCurrent, 1, player.data.level))
        {
            InstanFireBall();
        }
    }

    /**
     * Hàm sinh ra Cầu lửa
     */
    private void InstanFireBall()
    {
        //GameObject fireBall = Instantiate(fireBallPreFab, hard.transform.position, Quaternion.identity);
        GameObject fireBall = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill1, hard.transform.position, Quaternion.identity);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = hard.transform.forward * 20f;
        }
    }
}
