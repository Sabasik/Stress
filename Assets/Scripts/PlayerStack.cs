using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStack : MonoBehaviour
{
    public Card current;
    public Card empty;
    public bool canBePlaced;
    public Game game;
    private Card rememberCurrent;
    private HashSet<string> usedCards;
    
    void Start()
    {
        usedCards = new HashSet<string>();
        addEmpty();
    }

    public void addEmpty() {
        changeCurrent(empty, true);
    }

    // when a card is removed from here then empty should be added back

    private void changeCurrent(Card newCard, bool isEmpty) {
        current = newCard;

        if (current != empty) {
            var child = new GameObject("ChildWithImage");
            child.transform.SetParent(gameObject.transform, false);
            child.AddComponent<RectTransform>();
            child.GetComponent<RectTransform>().sizeDelta = new Vector2(98, 153);
            child.AddComponent<Image>();
            child.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
        }
        
        gameObject.GetComponent<Image>().sprite = current.GetComponent<SpriteRenderer>().sprite;
        canBePlaced = isEmpty;
    }

    public void changeCurrent(Card newCard) {
        changeCurrent(newCard, false);
    }

    public void placeCardToMiddle() {
        if (canBePlaced) return;
        Stack stack = game.getActiveStack();

        if (stack.canBePlaced(current)) {
            var rememberCurrent = current;

            var vecTo = stack.gameObject.transform.position;
            var name = current.GetComponent<SpriteRenderer>().sprite.name;
            usedCards.Add(name);
            getChildBySprite(name).GetComponent<RectTransform>().transform.DOMove(vecTo, 0.1f).onComplete = FinishedMove;
            addEmpty();

            stack.placeIfPossible(rememberCurrent);
        }
    }

    public bool placeCardToMiddle(Stack stack) {
        if (canBePlaced) return false;

        if (stack.canBePlaced(current)) {
            rememberCurrent = current;

            var vecTo = stack.gameObject.transform.position;
            var name = current.GetComponent<SpriteRenderer>().sprite.name;
            usedCards.Add(name);
            getChildBySprite(name).GetComponent<RectTransform>().transform.DOMove(vecTo, 0.1f).onComplete = FinishedMove;
            addEmpty();

            stack.placeIfPossible(rememberCurrent);
            return true;
        }
        return false;
    }

    public void FinishedMove() {
        foreach(var name in usedCards) {
            var child = getChildBySprite(name);
            if (child != null) {
                Object.Destroy(child);
                usedCards.Remove(name);
            }
        }
        
    }

    private GameObject getChildBySprite(string name) {
        int children = gameObject.transform.childCount;
        for (int i = 0; i < children; i++) {
            var child = gameObject.transform.GetChild(i);
            if (child == null) continue;
            if (child.GetComponent<Image>().sprite.name == name) {
                return child.gameObject;
            }
        }
        return null;
    }
}
