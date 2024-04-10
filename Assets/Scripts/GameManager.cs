using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int currentPlayer = 1;
    public static int playedCardsOnTable = 0;
    
    public List<GameObject> playedCards = new List<GameObject>();
    private CardDealer cardDealer;

    // Start is called before the first frame update
    void Start()
    {
        cardDealer = GameObject.FindWithTag("CardDealer").GetComponent<CardDealer>();
    }

    public void ClearPlayedCards()
    {
        foreach (var card in playedCards)
        {
            cardDealer.currentDeckOfCards.Add(card);
            card.transform.parent = cardDealer.transform;
            card.SetActive(false);
        }
        
        playedCards.Clear();
    }
}
