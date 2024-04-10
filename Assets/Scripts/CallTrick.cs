using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallTrick : MonoBehaviour
{
    [SerializeField] private GameEvent onTrickCalled;
    [SerializeField] private TextMeshProUGUI trickCount_Txt;
    private int trickCount;
    
    //Add to trick count and raise the event to alert other scripts.
    public void CollectTrick()
    {
        trickCount++;
        trickCount_Txt.text = trickCount.ToString();
        onTrickCalled.Raise();
        Debug.Log("collecttrick");
    }

}
