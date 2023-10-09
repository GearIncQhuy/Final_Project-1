using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image phase;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private NextLevel NextLevel;

    private Card cardCurrent;

    public void SetCard(Card card)
    {
        if(card != null)
        {
            icon.sprite = card.icon;
            phase.sprite = card.sprite;
            text.text = PopertiesText(card);
            cardCurrent = card;
        }
    }
    public void RandomCard()
    {
        if (ManagerScript.Ins.player.data.coin > 0)
        {
            Card card = CardManager.Ins.RandomCard();
            SetCard(card);
            CardManager.Ins.UpdateCoin();
        }
    }
    private string PopertiesText(Card card)
    {
        string textString = "";
        if(card.dame != 0)
        {
            textString += "Dame: " + card.dame + "\n";
        }
        if(card.speedFire != 0)
        {
            textString += "Toc danh: " + card.speedFire + "\n";
        }
        if (card.speed != 0)
        {
            textString += "Speed: " + card.speed + "\n";
        }
        if (card.mana != 0)
        {
            textString += "Mana: " + card.mana + "\n";
        }
        if (card.heal != 0)
        {
            textString += "heal: " + card.heal + "\n";
        }
        return textString;
    }

    public void ClickCard()
    {
        cardCurrent.UpdateUse();
        ManagerScript.Ins.player.healCurrent += cardCurrent.heal;
        ManagerScript.Ins.player.dame += cardCurrent.dame;
        ManagerScript.Ins.player.speedCurrent += cardCurrent.speed;
        ManagerScript.Ins.player.tamdanh += cardCurrent.speedFire;
        ManagerScript.Ins.player.manaCurrent += cardCurrent.mana;
        ManagerScript.Ins.player.data.phases = cardCurrent.phases;
        ManagerScript.Ins.player.Calculate();
        NextLevel.ClickNextLevel();
    }
}
