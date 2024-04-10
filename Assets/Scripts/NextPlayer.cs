using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlayer : MonoBehaviour
{
    [SerializeField] private GameEvent onPlayerChange;    

    //Figure out a better solution
    public void GoToNextPlayer ()
    {
        if(GameManager.currentPlayer < 4)
        {
            GameManager.currentPlayer++;
        }
        else
        {
            GameManager.currentPlayer = 1;
        }
        onPlayerChange.Raise();
    }
}
