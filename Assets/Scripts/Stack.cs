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

    void Update()
    {
    }

    public void changeCurrent(Card newCard) {
        current = newCard;
        gameObject.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
    }
}
