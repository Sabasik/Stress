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

    public void addCards(List<Card> newCards) {
        foreach(Card newCard in newCards) {
            cards.Add(newCard);
        }
    }

    public void placeNewCard() {
        if (cards.Count == 0) return;

        var first = cards[0];
        
        for (var i = 0; i < playerStacks.Count; i++) {
            if (playerStacks[i].canBePlaced) {
                playerStacks[i].changeCurrent(first);

                // if successfully added then 
                cards.RemoveAt(0);
                break;
            }
        }
    }
}
