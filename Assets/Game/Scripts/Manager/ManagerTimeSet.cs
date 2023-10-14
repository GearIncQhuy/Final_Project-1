using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class ManagerTimeSet : Singleton<ManagerTimeSet>
{
    [SerializeField] private GameObject sceneNextLevel;
    [SerializeField] private GameObject sceneLevelAgain;
    public ScriptTableGame data;

    public float timeEndTurn;
    public bool endTurn;

    public int timeSet;
    private float timeStart;

    public int level;
    public int turn;

    public bool checkSpawn;

    private void Start()
    {
        checkSpawn = true;
        turn = 1;
    }

    private void Update()
    {
        if (timeSet >= 0f)
        {
            if (turn > 3)
            {
                endTurn = true;
                NextLevel();
                endTurn = false;
            }
        }
        if (checkSpawn && data.level < 20)
        {
            level = data.level;  
            timeEndTurn = EndTurn(); 
            timeStart += Time.deltaTime; 
            if (timeSet >= EndTurn()) 
            {
                endTurn = true;
                NextTurn();
            }
            if (timeStart >= 1f && !endTurn)
            {
                timeSet++;
                timeStart = 0f;
            }
        }
    }

    // End Turn (sub ware)
    private float EndTurn()
    {
        float timeTurnDefault = 40f;
        float timeAddTurn = turn * 5;
        float timeAddLevel = level * 5;
        return timeTurnDefault + timeAddTurn + timeAddLevel;
        //return 15f;
    }

    [SerializeField] private DamageNumber nextTurn;
    // Next Turn
    private void NextTurn()
    {
        if(turn < 4)
        {
            turn++;
            if(turn <= 3)
            {
                Vector3 newPosition = ManagerScript.Ins.player.transform.position;
                newPosition.y += 4f;
                DamageNumber damageNumber = nextTurn.Spawn(newPosition, turn);
                damageNumber.SetScale(4f);
            }
        }
        timeEndTurn = EndTurn();
        timeSet = 0;
        ManagerScript.Ins.player.healCurrent = ManagerScript.Ins.player.data.healMax;
        ManagerScript.Ins.player.manager.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, 0);
    }

    // Next Level
    public void NextLevel()
    {
        checkSpawn = false;
        ManagerScript.Ins.player.checkBatTu = true;
        
        if(data.level > 0 && data.level < 20)
        {
            sceneNextLevel.SetActive(true);
        }
        else if(data.level == 20)
        {
            sceneLevelAgain.SetActive(true);
        }

        timeEndTurn = EndTurn();
        turn = 1;
        if(data.level > 20)
        {
            data.level = 1;
        }
        ManagerScript.Ins.player.healCurrent = ManagerScript.Ins.player.data.healMax;
        ManagerScript.Ins.player.manager.healPlayer.UpdateHealPlayer(ManagerScript.Ins.player.healCurrent, 0);
    }
}
