using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject addNewCardsButton;
    public TextMeshProUGUI addNewCardsText;
    public GameObject stressButton;
    public TextMeshProUGUI stressText;
    private bool canCheckStress;
    private float timeLeft;
    private bool isCountDown;

    void Start()
    {
        activeStack = 0;
        rightGlow.enabled = false;
        canCheckStress = true;
        timeLeft = 0;
        isCountDown = false;

        Shuffle(cards);

        var firstList = new List<Card>();
        var secondList = new List<Card>();
        for(var i = 0; i < cards.Count; i++) {
            if (i % 2 == 0) firstList.Add(cards[i]);
            else secondList.Add(cards[i]);
        }
        player.addCards(firstList);
        computer.addCards(secondList);

        newCardsToTheMiddle();
        makeButtonHidden(stressButton, stressText);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            changeActiveStack();
        }
        if (noPossibleMoves()) {
            if (checkWin()) {
                Debug.Log("Game over");
                SceneManager.LoadScene("Menu");
            }
            makeButtonVisible(addNewCardsButton, addNewCardsText, "Add new cards");
        }

        if (isCountDown) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                isCountDown = false;
                doStress(player);
            }
        }

        if (checkStress()) {
            if (canCheckStress) {
                canCheckStress = false;
                makeButtonVisible(stressButton, stressText, "STRESS");
                timeLeft = Random.Range(1 / Menu.info.getSpeed() * 0.4f, 1 / Menu.info.getSpeed() * 3f);
                isCountDown = true;
            }
        } else {
            makeButtonHidden(stressButton, stressText);
            canCheckStress = true;
            isCountDown = false;
        }
    }

    private bool checkStress() {
        Card leftCard = left.current;
        Card rightCard = right.current;
        return leftCard.value == rightCard.value;
    }

    private bool checkWin() {
        var playerCardCount = player.cards.Count;
        var computerCardCount = computer.cards.Count;
        // check if they have cards in front of them
        var playerStackCardCount = player.stackCardsCount();
        var computerStackCardCount = computer.stackCardsCount();

        // both have cards to play more
        if (playerCardCount > 0 && computerCardCount > 0) return false;
        if (playerCardCount > 1 || computerCardCount > 1) return false;

        // not enough cards, one wins or draw
        if (playerCardCount == 0 && playerStackCardCount == 0 && computerCardCount == 0 && computerStackCardCount == 0) {
            Debug.Log("Draw");
            Menu.info.setText("DRAW");
            return true;
        }
        if (playerCardCount == 0 && playerStackCardCount == 0) {
            Debug.Log("Player wins");
            Menu.info.setText("PLAYER WON");
            return true;
        }
        if (computerCardCount == 0 && computerStackCardCount == 0) {
            Debug.Log("Computer wins");
            Menu.info.setText("COMPUTER WON");
            return true;
        }
        
        Debug.Log("Draw");
        Menu.info.setText("DRAW");
        return true;
    }

    public void newCardsToTheMiddle() {
        var playerCardCount = player.cards.Count;
        var computerCardCount = computer.cards.Count;
        // both players have cards
        if (playerCardCount > 0 && computerCardCount > 0) {
            player.placeCardToStack(left);
            computer.placeCardToStack(right);
        }
        // one player has cards for both stacks
        else if (playerCardCount > 1) {
            player.placeCardToStack(left);
            player.placeCardToStack(right);
        }
        else if (computerCardCount > 1) {
            computer.placeCardToStack(left);
            computer.placeCardToStack(right);
        }
        // not enough cards, one wins or draw
        else if (playerCardCount > 0) {
            Debug.Log("Computer wins");
            Menu.info.setText("COMPUTER WON");
            SceneManager.LoadScene("Menu");
        }
        else if (computerCardCount > 0) {
            Debug.Log("Player wins");
            Menu.info.setText("PLAYER WON");
            SceneManager.LoadScene("Menu");
        }
        else {
            // check if they have cards in front of them
            Debug.Log("Draw or someone won");
            checkWin();
            SceneManager.LoadScene("Menu");
        }

        makeButtonHidden(addNewCardsButton, addNewCardsText);
    }

    private void makeButtonVisible(GameObject gameObject, TextMeshProUGUI textObject, string textInfo) {
        gameObject.GetComponent<Button>().enabled = true; 
        gameObject.GetComponent<Image>().enabled = true;
        textObject.text = textInfo;
    }

    private void makeButtonHidden(GameObject gameObject, TextMeshProUGUI textObject) {
        gameObject.GetComponent<Button>().enabled = false; 
        gameObject.GetComponent<Image>().enabled = false;
        textObject.text = "";
    }

    private bool noPossibleMoves() {
        if (computer.canPlaceCard(left, right)) return false;
        if (player.canPlaceCard(left, right)) return false;
        return true;
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

    public void stressPressed() {
        doStress(computer);
    }

    private void doStress(Deck whoGetsCards) {
        isCountDown = false;
        makeButtonHidden(stressButton, stressText);
        List<Card> usedCards = getUsedCards();
        Shuffle(usedCards);
        whoGetsCards.addCards(usedCards);

        // check if win
        Deck presser = (whoGetsCards == player ? computer : player);
        Debug.Log(whoGetsCards);
        Debug.Log(presser);
        var cardCount = presser.cards.Count;
        var stackCardCount = presser.stackCardsCount();
        Debug.Log(cardCount);
        Debug.Log(stackCardCount);        
        if (cardCount == 0 && stackCardCount == 0) {
            Debug.Log("Game over");
            if (presser == player) Menu.info.setText("PLAYER WON");
            else Menu.info.setText("COMPUTER WON");
            SceneManager.LoadScene("Menu");
        }

        newCardsToTheMiddle();
        canCheckStress = true;
    }

    private List<Card> getUsedCards() {
        List<Card> usedCards = new List<Card>();
        foreach(Card card in cards) {
            bool found = false;
            // cards in decks
            foreach(Card card2 in player.cards) {
                if (card.gameObject.name.Equals(card2.gameObject.name)) {
                    found = true;
                    break;
                };
            }
            if (found) continue;
            foreach(Card card2 in computer.cards) {
                if (card.gameObject.name.Equals(card2.gameObject.name)) {
                    found = true;
                    break;
                }
            }
            if (found) continue;
            // cards in front of players
            foreach(PlayerStack ps in player.playerStacks) {
                if (!ps.canBePlaced && card.gameObject.name.Equals(ps.current.gameObject.name)) {
                    found = true;
                    break;
                }
            }
            if (found) continue;
            foreach(PlayerStack ps in computer.playerStacks) {
                if (!ps.canBePlaced && card.gameObject.name.Equals(ps.current.gameObject.name)) {
                    found = true;
                    break;
                }
            }
            if (found) continue;

            usedCards.Add(card);
        }

        return usedCards;
    }
}
