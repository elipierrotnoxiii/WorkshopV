using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")]
    public DeckManager deckManager;
    public Player player;
    public Enemy enemy;

    private List<Card> currentHand = new List<Card>();
    private List<Card> playedCardsThisTurn = new List<Card>();
    private int cardsPlayed = 0;
    private bool playerTurn = true;

    private void Start()
    {
        deckManager.InitializeDeck();
        StartPlayerTurn();
    }

    private void Update()
    {
        if (!playerTurn) return;

        // Press keys 1–5 to play cards
        if (Input.GetKeyDown(KeyCode.Alpha1)) PlayCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlayCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlayCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlayCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) PlayCard(4);

        // End turn
        if (Input.GetKeyDown(KeyCode.Space)) EndPlayerTurn();
    }

    [ContextMenu("Start Turn")]
    void StartPlayerTurn()
    {
        playerTurn = true;
        cardsPlayed = 0;
        playedCardsThisTurn.Clear();
        currentHand = deckManager.Draw(5);
        player.block = 0; // Reset block each turn

        Debug.Log("----- PLAYER TURN -----");
        foreach (var card in currentHand)
            Debug.Log($"Drew: {card.cardName}");
    }

    [ContextMenu("End Turn")]
    void EndPlayerTurn()
    {
        playerTurn = false;

        // Return both played and unplayed cards
        List<Card> allCardsThisTurn = new List<Card>();
        allCardsThisTurn.AddRange(currentHand);
        allCardsThisTurn.AddRange(playedCardsThisTurn);

        deckManager.ReturnCards(currentHand);

        currentHand.Clear();
        playedCardsThisTurn.Clear();

        Debug.Log("Player turn ended. Cards returned to deck.");
        StartCoroutine(EnemyTurnRoutine());
    }

    IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("----- ENEMY TURN -----");
        yield return new WaitForSeconds(1f); // small delay for readability

        enemy.EnemyTurn(player);

        yield return new WaitForSeconds(1f); // pause before next player turn
        StartPlayerTurn();
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

        // Move played card to separate list
        playedCardsThisTurn.Add(card);
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

    //[Header("Debug Controls")]
    //public int testCardIndex = 0;

    //[ContextMenu("Play Test Card")]
    //public void PlayTestCard()
    //{
    //    PlayCard(testCardIndex);
    //}
}
