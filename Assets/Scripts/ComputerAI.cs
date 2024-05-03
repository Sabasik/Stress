using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    public Deck deck;
    public Stack left;
    public Stack right;
    public float speed;
    private float timeLeft;
    private float minSpeed;
    private float maxSpeed;

    void Start()
    {
        speed = Menu.info.getSpeed();
        timeLeft = untilNextMove();
        minSpeed = 1 / speed * 0.4f;
        maxSpeed = 1 / speed * 4f;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0) {
            var cardsInDeck = deck.cards.Count;
            var cardsInStacks = deck.stackCardsCount();

            while (cardsInStacks < 4 && cardsInDeck > 0) {
                deck.placeNewCard();
                cardsInStacks = deck.stackCardsCount();
                cardsInDeck = deck.cards.Count;
            }
            placeCard();
            timeLeft = untilNextMove();
        }
    }

    private bool placeCard() {
        for (var i = 0; i < deck.playerStacks.Count; i++) {
            if (deck.playerStacks[i].placeCardToMiddle(left)) return true;
            if (deck.playerStacks[i].placeCardToMiddle(right)) return true;
        }
        return false;
    }

    private float untilNextMove() {
        return Random.Range(minSpeed, maxSpeed);
    }
}
