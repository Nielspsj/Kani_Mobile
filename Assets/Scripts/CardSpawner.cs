using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private List<Card_SO> card_SOs = new List<Card_SO>();
    [SerializeField] private Card_SO cardSOtospawn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("card suit: " + cardSOtospawn.suit);
        Debug.Log("card nr: " + cardSOtospawn.number);
        Debug.Log("card texture: " + cardSOtospawn.cardTexture);
        int nr = cardSOtospawn.number;
        Debug.Log("nr: " + nr);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
