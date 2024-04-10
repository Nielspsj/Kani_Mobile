using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]
public class Card_SO : ScriptableObject
{
    public string suit;
    public int number;
    public Texture cardTexture;
}
