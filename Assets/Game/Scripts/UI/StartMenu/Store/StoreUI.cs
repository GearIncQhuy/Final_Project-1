using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    public List<GameObject> listCard;
    [SerializeField] private List<ScriptTableCard> dataCard = new List<ScriptTableCard>();
    [SerializeField] private GameObject PreFabCard;
    private void Start()
    {
        //if(dataCard.Count > 0)
        //{
        //    foreach (ScriptTableCard data in dataCard)
        //    {
        //        GameObject cardPreFab = Instantiate(PreFabCard);
        //        CardItem cardItem = cardPreFab.GetComponent<CardItem>();
        //        Card card = new Card(data);
        //        if(cardItem != null)
        //        {
        //            cardItem.SetCard(card);
        //        }
        //        listCard.Add(cardPreFab);
        //    }
        //}
    }
}
