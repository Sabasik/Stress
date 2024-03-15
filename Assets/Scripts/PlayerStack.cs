using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStack : MonoBehaviour
{
    public Card current;
    public Card empty;
    
    void Start()
    {
        addEmpty();
    }

    void Update()
    {
    }

    public void addEmpty() {
        changeCurrent(empty);
    }

    // when a card is removed from here then empty should be added back

    public void changeCurrent(Card newCard) {
        current = newCard;
        gameObject.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
    }
}
