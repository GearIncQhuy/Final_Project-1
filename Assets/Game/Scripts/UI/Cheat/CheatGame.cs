using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatGame : MonoBehaviour
{
    [SerializeField] private GameObject status;

    [SerializeField] private TextMeshProUGUI levelPlayer;
    [SerializeField] private TextMeshProUGUI ware;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI subware;
    [SerializeField] private TextMeshProUGUI enemySpawn;
    [SerializeField] private PoolEnemy poolEnemy;
    private ManagerScript manager;
    private ManagerTimeSet timeSet;
    

    // Start is called before the first frame update
    void Start()
    {
        manager = ManagerScript.Ins;
        timeSet = ManagerTimeSet.Ins;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (status.activeInHierarchy)
            {
                status.SetActive(false);
            }
            else
            {
                status.SetActive(true);
            }
            ClickCheat();
        }
    }

    public void ClickCheat()
    {
        
        levelPlayer.text = manager.player.data.level.ToString();
        ware.text = timeSet.level.ToString();
        subware.text = timeSet.turn.ToString();
        enemySpawn.text = poolEnemy.enemyMax.ToString();
    }

    public void ClickLevel()
    {
        ManagerTimeSet.Ins.data.level += 1;
        if(ManagerTimeSet.Ins.data.level > 20)
        {
            ManagerTimeSet.Ins.data.level = 1;
        }
    }

    public void ClickLevelPlayer()
    {
        ManagerScript.Ins.player.uplevel.UpLevel();
    }
}
