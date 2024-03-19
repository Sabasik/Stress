using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Card> cards;
    public Deck player;
    public Deck computer;
    public Stack left;
    public Stack right;
    private int activeStack;
    public UIOutline rightGlow;
    public UIOutline leftGlow;


    void Start()
    {
        activeStack = 0;
        rightGlow.enabled = false;

        Shuffle(cards);

        var firstList = new List<Card>();
        var secondList = new List<Card>();
        for(var i = 0; i < cards.Count; i++) {
            if (i % 2 == 0) firstList.Add(cards[i]);
            else secondList.Add(cards[i]);
        }
        player.addCards(firstList);
        computer.addCards(secondList);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            changeActiveStack();
        }
    }

    public static void Shuffle(List<Card> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    private void changeActiveStack() {
        if (activeStack == 0) {
            activeStack = 1;
            rightGlow.enabled = true;
            leftGlow.enabled = false;
        } else {
            activeStack = 0;
            rightGlow.enabled = false;
            leftGlow.enabled = true;
        }
    }

    public Stack getActiveStack() {
        return activeStack == 0 ? left : right;
    }
}
