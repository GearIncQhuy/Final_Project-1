using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : MonoBehaviour
{
    private PlayerController player;
    private ManaPlayer manaPlayer;

    [SerializeField] GameObject targetCircle;
    Vector3 transCircle;

    private float timeStart;
    private bool startTime;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        manaPlayer = gameObject.GetComponent<ManaPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // thời gian hồi chiêu 20s
        if (startTime)
        {
            timeStart += Time.deltaTime;
            if(timeStart >= 20f)
            {
                startTime = false;
                timeStart = 0f;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha3) && !startTime)
        {
            if (manaPlayer.UseMana(player.manaCurrent, 3, player.data.level))
            {
                UseSkill();
                startTime = true;
            }
        }
    }
    /**
     * Tạo ra vòng tròn lửa để xác định vị trí rơi
     */
    private void UseSkill()
    {
        transCircle = transform.position;
        transCircle.y -= transform.position.y;
        GameObject circle = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill3, transCircle, Quaternion.identity);
        //player.DontMove = true;

    }
}
