using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardItem : MonoBehaviour
{
    #region Poperties
    [SerializeField] private Image icon;
    [SerializeField] private Image phase;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private NextLevel NextLevel;
    #endregion

    private Card cardCurrent;

    #region Set Card
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
    #endregion

    #region Random Card
    public void RandomCard()
    {
        if (ManagerScript.Ins.player.data.coin > 0)
        {
            Card card = CardManager.Ins.RandomCard();
            SetCard(card);
            CardManager.Ins.UpdateCoin();
        }
    }
    #endregion

    #region Poperties Text
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
    #endregion

    #region Button Click Card 
    public void ClickCard()
    {
        if(ManagerScript.Ins.player.data.coin >= cardCurrent.price)
        {
            ManagerScript.Ins.player.data.coin -= cardCurrent.price;
            cardCurrent.UpdateUse();
            ManagerScript.Ins.player.healCurrent    += (int)cardCurrent.heal;
            ManagerScript.Ins.player.dame           += (int)cardCurrent.dame;
            ManagerScript.Ins.player.speedCurrent   += (int)cardCurrent.speed;
            ManagerScript.Ins.player.tamdanh        += (int)cardCurrent.speedFire;
            ManagerScript.Ins.player.manaCurrent    += (int)cardCurrent.mana;
            ManagerScript.Ins.player.data.phases     = cardCurrent.phases;
            ManagerScript.Ins.player.Calculate();
            NextLevel.ClickNextLevel();
        }
    }
    #endregion
}
