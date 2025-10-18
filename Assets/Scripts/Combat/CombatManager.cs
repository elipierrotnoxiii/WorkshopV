using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")]
    public DeckManager deckManager;
    public Player player;
    public Enemy enemy;

    private List<Card> currentHand = new List<Card>();
    private int cardsPlayed = 0;

    private void Start()
    {
        deckManager.InitializeDeck();
        StartTurn();
    }

    [ContextMenu("Start Turn")]
    public void StartTurn()
    {
        cardsPlayed = 0;
        currentHand = deckManager.Draw(5);
        Debug.Log("----- NEW TURN -----");
        foreach (var card in currentHand)
            Debug.Log($"Drew: {card.cardName}");
    }

    [ContextMenu("End Turn")]
    public void EndTurn()
    {
        deckManager.ReturnCards(currentHand);
        currentHand.Clear();
        Debug.Log("Turn ended. Cards returned to deck.\n");
        StartTurn();
    }

    public void PlayCard(int index)
    {
        if (index < 0 || index >= currentHand.Count)
        {
            Debug.LogWarning("Invalid card index!");
            return;
        }
        if (cardsPlayed >= 3)
        {
            Debug.Log("Already played 3 cards this turn.");
            return;
        }

        // Play and remove the card
        Card card = currentHand[index];
        card.Play(player, enemy);
        cardsPlayed++;

        // Remove the card from hand so it can't be played again this turn
        currentHand.RemoveAt(index);

        Debug.Log($"Played {card.cardName}. {3 - cardsPlayed} plays remaining this turn.");

        // Show remaining hand for debugging
        if (currentHand.Count > 0)
        {
            string remaining = string.Join(", ", currentHand.ConvertAll(c => c.cardName));
            Debug.Log($"Cards remaining in hand: {remaining}");
        }
        else
        {
            Debug.Log("No cards left in hand this turn.");
        }
    }

    [Header("Debug Controls")]
    public int testCardIndex = 0;

    [ContextMenu("Play Test Card")]
    public void PlayTestCard()
    {
        PlayCard(testCardIndex);
    }
}
