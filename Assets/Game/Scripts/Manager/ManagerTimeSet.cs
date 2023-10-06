using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTimeSet : Singleton<ManagerTimeSet>
{
    public float timeEndTurn;
    public bool endTurn;

    private int timeSet;
    private float timeStart;

    public int level;
    public int turn;

    private void Start()
    {
        level = 1;
        turn = 1;
        timeEndTurn = EndTurn();
    }

    private void Update()
    {
        timeStart += Time.deltaTime;
        if(timeStart >= 1f)
        {
            timeSet++;
            timeStart = 0f;
        }

        if (timeSet >= EndTurn())
        {
            endTurn = true;
            NextTurn();
        }

        if (timeSet >= 1f)
        {
            if (turn > 3)
            {
                NextLevel();
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
    }

    // Next Turn
    private void NextTurn()
    {
        if(turn < 4)
        {
            turn++;
        }
        timeEndTurn = EndTurn();
        timeSet = 0;
        ManagerScript.Ins.player.healCurrent = ManagerScript.Ins.player.data.healMax;
    }

    // Next Level
    private void NextLevel()
    {
        timeEndTurn = EndTurn();
        level++;
        turn = 1;
        timeSet = 0;
        ManagerScript.Ins.player.healCurrent = ManagerScript.Ins.player.data.healMax;
    }
}
