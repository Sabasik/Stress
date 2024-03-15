using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    public Card current;
    
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
    }

    public void changeCurrent(Card newCard) {
        current = newCard;
        gameObject.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
    }

    public bool placeIfPossible(Card newCard) {
        if (canBePlaced(newCard)) {
            changeCurrent(newCard);
            return true;
        }
        return false;
    }

    public bool canBePlaced(Card newCard) {
        if (current.value < 0) return true;
        if (current.value + 1 == newCard.value) return true;
        if (current.value == newCard.value + 1) return true;
        // äss ja kuningas
        if (current.value == 13 && newCard.value == 1) return true;
        if (current.value == 1 && newCard.value == 13) return true;

        return false;
    }
}
