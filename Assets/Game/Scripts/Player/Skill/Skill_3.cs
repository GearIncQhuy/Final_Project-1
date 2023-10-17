using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_3 : MonoBehaviour
{
    #region Poperties
    private PlayerController player;
    private ManaPlayer manaPlayer;

    [SerializeField] private Image imgSkill;
    [SerializeField] GameObject targetCircle;
    Vector3 transCircle;

    private float timeStart;
    private bool startTime;
    #endregion

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
            imgSkill.fillAmount = 1 - timeStart / 20f;
        }
        else
        {
            imgSkill.fillAmount = 0f;
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CheckSkill();
        }
    }

    #region Button Skill 3
    public void CheckSkill()
    {
        if (player.checkPlayerLife && !startTime)
        {
            if (manaPlayer.UseMana(player.manaCurrent, 3, player.data.level))
            {
                UseSkill();
                imgSkill.fillAmount = 1f;
                //startTime = true;
            }
        }
    }
    #endregion

    #region Use Skill
    /**
     * Tạo ra vòng tròn lửa để xác định vị trí rơi
     */
    private void UseSkill()
    {
        
        startTime = true;
        transCircle = transform.position;
        transCircle.y -= transform.position.y;
        GameObject circle = ObjectPool.Ins.SpawnFromPool(Constants.Tag_Skill3, transCircle, Quaternion.identity);
        //player.DontMove = true;

    }
    #endregion
}
