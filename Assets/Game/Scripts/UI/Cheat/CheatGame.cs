using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatGame : MonoBehaviour
{
    #region Poperties
    [SerializeField] private GameObject status;

    [SerializeField] private TextMeshProUGUI levelPlayer;
    [SerializeField] private TextMeshProUGUI ware;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI subware;
    [SerializeField] private TextMeshProUGUI enemySpawn;
    [SerializeField] private PoolEnemy poolEnemy;
    private ManagerScript manager;
    private ManagerTimeSet timeSet;
    #endregion

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

    #region Click Cheat
    public void ClickCheat()
    {
        
        levelPlayer.text = manager.player.data.level.ToString();
        ware.text = timeSet.level.ToString();
        subware.text = timeSet.turn.ToString();
        enemySpawn.text = poolEnemy.enemyMax.ToString();
    }
    #endregion

    #region Click Level
    public void ClickLevel()
    {
        ManagerTimeSet.Ins.data.level += 1;
        if(ManagerTimeSet.Ins.data.level > 20)
        {
            ManagerTimeSet.Ins.data.level = 1;
        }
    }
    #endregion

    #region Click Level Player
    public void ClickLevelPlayer()
    {
        ManagerScript.Ins.player.uplevel.UpLevel();
    }
    #endregion
}
