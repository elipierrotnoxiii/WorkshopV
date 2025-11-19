using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Deck Setup")]
    public List<Card> startingDeck = new List<Card>();
    private List<Card> deck = new List<Card>();
    private System.Random rng = new System.Random();

    public void InitializeDeck()
    {
        deck = new List<Card>(startingDeck);
        Shuffle();
    }

    public void Shuffle()
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (deck[k], deck[n]) = (deck[n], deck[k]);
        }
    }

    public List<Card> Draw(int count)
    {
        List<Card> hand = new List<Card>();
        for (int i = 0; i < count && deck.Count > 0; i++)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
        return hand;
    }

    public void ReturnCards(List<Card> cards)
    {
        deck.AddRange(cards);
        Shuffle();
    }
}

