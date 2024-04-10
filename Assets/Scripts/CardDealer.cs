using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public List<GameObject> deckOfCards = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();

    public List<GameObject> currentDeckOfCards = new List<GameObject>();
    [SerializeField] private List<GameObject> tempDeckOfCards = new List<GameObject>();
    private List<int> sortingList = new List<int>();
    private int handSize = 13;
    [SerializeField] private GameEvent OnCardsDealt;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject card in deckOfCards)
        {
            //Rotation when instantiating now redundant
            Vector3 cardRotation = new Vector3(-90, 0, 0);
            GameObject instantiatedCard = Instantiate(card, Vector3.zero, Quaternion.Euler(cardRotation));
            instantiatedCard.transform.tag = "Card";
            currentDeckOfCards.Add(instantiatedCard);
            tempDeckOfCards.Add(instantiatedCard);
        }

        DealCards();
    }   

    //Function to get playing players into a list of players
    private void PlayingPlayers()
    {
    }

    //Function to deal cards to all players
    private void DealCards()
    {
        //int counter = 0;
        //Start with player 0 (payer 1), randomly give 13 cards. Then move to the next.
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < handSize; j++)
            {
                GameObject pickedCard = currentDeckOfCards[Random.Range(0, currentDeckOfCards.Count)];
                players[i].GetComponent<PlayerController>().cardHand.Add(pickedCard);
                currentDeckOfCards.Remove(pickedCard);
                //int randomNr = Random.Range(0, currentDeckOfCards.Count);
                //sortingList.Add(randomNr);
                //currentDeckOfCards.RemoveAt(randomNr);

                //counter++;
                //Debug.Log("counter: " + counter);
            }
            /*
            sortingList.Sort();
            for (int cardNr = 0; cardNr < handSize; cardNr++)
            {
                //Debug.Log("cardNR: " + cardNr);
                //Debug.Log("sortingList[cardNr]: " + sortingList[cardNr]);
                GameObject pickedCard = tempDeckOfCards[sortingList[cardNr]];
                players[i].GetComponent<PlayerController>().cardHand.Add(pickedCard);
                //currentDeckOfCards.Remove(pickedCard);
            }
            sortingList.Clear();
            */
        }
        //Debug.Log("cards dealt");
        OnCardsDealt.Raise();
        //Debug.Log("event called");
    }

    //Try to separate the sorting into its own function. Not used atm
    private void SortingHand(int cardIndex)
    {
        List<int> sortingList = new List<int>();
        sortingList.Add(cardIndex);
    }

    //Shake to shuffle the deck of the cards
    private void ShuffleDeck()
    {

    }
}
