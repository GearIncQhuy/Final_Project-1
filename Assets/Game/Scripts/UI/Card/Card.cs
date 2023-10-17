using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public Sprite icon;
    public float dame;
    public float speedFire;
    public float speed;
    public float mana;
    public float heal;
    public int checkUse;
    public Phases phases;
    public int price;
    public Sprite sprite;

    private ScriptTableCard cardCurrent;

    public Card(ScriptTableCard card)
    {
        this.icon = card.icon;
        this.dame = card.dame;
        this.speedFire = card.speedFire;
        this.speed = card.speed;
        this.mana = card.mana;
        this.heal = card.heal;
        this.checkUse = card.checkUse;
        this.phases = card.phases;
        this.sprite = card.sprite;
        this.price = card.price;

        cardCurrent = card;
    }

    public void UpdateUse()
    {
        cardCurrent.checkUse++;
    }
}
