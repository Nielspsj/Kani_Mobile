using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    [SerializeField] private Card_SO cardSO;
    [field: SerializeField] public int cardNr { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        cardNr = cardSO.number;
    }   
}
