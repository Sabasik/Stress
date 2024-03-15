using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    public List<Card> cards;
    public TextMeshProUGUI count;
    public List<PlayerStack> playerStacks;

    void Update()
    {
        count.text = "" + cards.Count;        
    }
}
