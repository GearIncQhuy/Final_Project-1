using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMap1U : MonoBehaviour
{
    private BossMap1 boss;
    private float time;
    [SerializeField] private Slider healSlider;
    [SerializeField] private GameObject slider;
    private void Start()
    {
        boss = gameObject.GetComponent<BossMap1>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 0.5f)
        {
            slider.SetActive(false);
            time = 0f;
        }
    }

    public void UpdateHealBoss(float dame) {
        float healCurrentSlider = (boss.healCurrent - dame) / boss.data.healMax;
        boss.healCurrent -= dame;
        healSlider.value = healCurrentSlider;
        slider.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!boss.battu)
        {
            if (other.gameObject.CompareTag(Constants.Tag_Bullet))
            {
                UpdateHealBoss(ManagerScript.Ins.player.dame);
            }
            if (other.gameObject.CompareTag(Constants.Tag_Skill1))
            {
                UpdateHealBoss(ManagerScript.Ins.player.GetDamePlayer(1, ManagerScript.Ins.player.data.level, true));
            }
            if (other.gameObject.CompareTag(Constants.Tag_Skill1_2))
            {
                UpdateHealBoss(ManagerScript.Ins.player.GetDamePlayer(1, ManagerScript.Ins.player.data.level, true) / 2);
            }
            if (other.gameObject.CompareTag(Constants.Tag_Skill3_3))
            {
                UpdateHealBoss(2000);
            }
        }
    }
}
