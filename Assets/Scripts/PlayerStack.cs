using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStack : MonoBehaviour
{
    public Card current;
    public Card empty;
    public bool canBePlaced;
    public Game game;
    
    void Start()
    {
        addEmpty();
    }

    public void addEmpty() {
        changeCurrent(empty, true);
    }

    // when a card is removed from here then empty should be added back

    private void changeCurrent(Card newCard, bool isEmpty) {
        current = newCard;
        gameObject.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
        canBePlaced = isEmpty;
    }

    public void changeCurrent(Card newCard) {
        changeCurrent(newCard, false);
    }

    public void placeCardToMiddle() {
        if (canBePlaced) return;
        Stack stack = game.getActiveStack();

        if (stack.placeIfPossible(current)) {
            addEmpty();
        }
    }

    public bool placeCardToMiddle(Stack stack) {
        if (canBePlaced) return false;

        if (stack.placeIfPossible(current)) {
            addEmpty();
            return true;
        }
        return false;
    }
}
