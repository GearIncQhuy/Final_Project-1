using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : Singleton<CardManager>
{
    public List<Card> listCard = new List<Card>();
    public List<ScriptTableCard> listData = new List<ScriptTableCard>();
    [SerializeField] private List<CardItem> cardItems = new List<CardItem>();
    [SerializeField] private TextMeshProUGUI coin;
    private void Start()
    {
        for(int i = 0; i < listData.Count; i++)
        {
            Card card = new Card(listData[i]);
            listCard.Add(card);
        }
        SetUp();
    }

    public Card RandomCard()
    {
        int index = Random.Range(0, listCard.Count);
        return listCard[index];
    }

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

    public void UpdateCoin()
    {
        ManagerScript.Ins.player.data.coin -= 2;
        coin.text = ManagerScript.Ins.player.data.coin.ToString();
    }
}
