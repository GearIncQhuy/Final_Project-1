using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : Singleton<CardManager>
{
    #region Poperties List Card
    public List<Card> listCard = new List<Card>();
    public List<ScriptTableCard> listData = new List<ScriptTableCard>();
    [SerializeField] private List<CardItem> cardItems = new List<CardItem>();
    [SerializeField] private TextMeshProUGUI coin;
    #endregion

    private void Start()
    {
        for(int i = 0; i < listData.Count; i++)
        {
            Card card = new Card(listData[i]);
            listCard.Add(card);
        }
        SetUp();
    }

    #region Random Card
    public Card RandomCard()
    {
        int index = Random.Range(0, listCard.Count);
        return listCard[index];
    }
    #endregion

    #region Set Up
    public void SetUp()
    {
        if(cardItems.Count == 3)
        {
            for(int i = 0; i < cardItems.Count; i++)
            {
                cardItems[i].SetCard(RandomCard());
            }
        }
        coin.text = ManagerScript.Ins.player.data.coin.ToString();
    }
    #endregion

    #region
    public void UpdateCoin()
    {
        ManagerScript.Ins.player.data.coin -= 2;
        coin.text = ManagerScript.Ins.player.data.coin.ToString();
    }
    #endregion
}
