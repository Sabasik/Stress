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

    public void placeCardToStack(Stack stack) {
        stack.changeCurrent(cards[0]);        
        cards.RemoveAt(0);
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

    public bool canPlaceCard(Stack left, Stack right) {
        for (var i = 0; i < playerStacks.Count; i++) {
            // can put card in fron of person
            if (playerStacks[i].canBePlaced && cards.Count > 0) return true;
            // no card in this slot and cannot add
            if (playerStacks[i].canBePlaced) continue;
            // can card be put to the middle
            if (left.canBePlaced(playerStacks[i].current)) return true;
            if (right.canBePlaced(playerStacks[i].current)) return true;
        }
        return false;
    }
}
