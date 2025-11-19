using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombatManager : MonoBehaviour
{
    //[Header("References")]
    //public DeckManager deckManager;
    //public Player player;
    //public Enemy enemy;
    //public HandView handView;
    //public CardViewCreator cardViewCreator;

    //[Header("Gameplay")]
    //public int maxPlaysPerTurn = 3;

    //private List<Card> currentHand = new();
    //private List<Card> playedCardsThisTurn = new();
    //private List<CardView> currentCardViews = new();

    //private int cardsPlayed = 0;
    //private bool playerTurn = true;

    //private void Start()
    //{
    //    deckManager.InitializeDeck();
    //    StartCoroutine(StartPlayerTurn());
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) EndPlayerTurn();
    //}

    //private IEnumerator StartPlayerTurn()
    //{
    //    playerTurn = true;
    //    cardsPlayed = 0;
    //    playedCardsThisTurn.Clear();
    //    player.block = 0;

    //    int cardsToDraw = currentHand.Count == 0 ? 5 : 1;
    //    List<Card> drawnCards = deckManager.Draw(cardsToDraw);
    //    currentHand.AddRange(drawnCards);

    //    // Agregar cartas visualmente una por una
    //    foreach (Card card in drawnCards)
    //    {
    //        //CardConstructor constructor = new CardConstructor(card);
    //        CardView cardView = cardViewCreator.CreateCardView(constructor, Vector3.zero, Quaternion.identity);
    //        currentCardViews.Add(cardView);

    //        // Agregar click handler
    //        var handler = cardView.gameObject.AddComponent<CardViewClickHandler>();
    //        handler.Initialize(this, cardView);

    //        // Esperar a que se agregue y se posicione antes de la siguiente
    //        yield return handView.StartCoroutine(handView.AddCard(cardView));
    //    }

    //    Debug.Log("----- PLAYER TURN -----");
    //    BattleLogUI.Instance.AddMessage("----- PLAYER TURN -----");
    //    foreach (var c in currentHand)
    //    {
    //        Debug.Log($"Drew: {c.Title}");
    //        BattleLogUI.Instance.AddMessage($"Drew: {c.Title}");
    //    }

    //}

    //public void PlayCardVisual(CardView cardView)
    //{
    //    if (!playerTurn) return;

    //    Card card = cardView.CardConstructor.SourceCard;

    //    int index = currentCardViews.IndexOf(cardView);
    //    if (index < 0)
    //    {
    //        Debug.LogWarning("CardView not found in hand.");
    //        BattleLogUI.Instance.AddMessage("CardView not found in hand.");

    //        return;
    //    }

    //    if (cardsPlayed >= maxPlaysPerTurn)
    //    {
    //        Debug.Log("Already played maximum cards this turn.");
    //        BattleLogUI.Instance.AddMessage("Already played maximum cards this turn.");
    //        return;
    //    }

    //    // Aplicar efecto
    //    switch (card.EffectType)
    //    {
    //        case CardEffectType.Damage:
    //            enemy.TakeDamage(card.Value);
    //            break;
    //        case CardEffectType.Heal:
    //            player.Heal(card.Value);
    //            break;
    //        case CardEffectType.Block:
    //            player.AddBlock(card.Value);
    //            break;
    //    }

    //    cardsPlayed++;
    //    playedCardsThisTurn.Add(card);
    //    currentHand.Remove(card);

    //    // Quitamos la carta de la lista antes de animar para evitar referencias rotas
    //    currentCardViews.RemoveAt(index);

    //    // Animación y desactivación en vez de destruir
    //    StartCoroutine(AnimateAndRemoveCard(cardView));

    //    Debug.Log($"Played {card.Title}. Remaining plays: {maxPlaysPerTurn - cardsPlayed}");
    //    BattleLogUI.Instance.AddMessage($"Played {card.Title}. Remaining plays: {maxPlaysPerTurn - cardsPlayed}");

    //    if (currentHand.Count > 0)
    //    {
    //        string remaining = string.Join(", ", currentHand.ConvertAll(c => c.Title));
    //        Debug.Log($"Cards remaining in hand: {remaining}");
    //        BattleLogUI.Instance.AddMessage($"Cards remaining in hand: {remaining}");
    //    }
    //    else
    //    {
    //        Debug.Log("No cards left in hand this turn.");
    //        BattleLogUI.Instance.AddMessage("No cards left in hand this turn.");
    //    }
    //}

    //private IEnumerator AnimateAndRemoveCard(CardView cardView)
    //{
    //    if (cardView == null) yield break;

    //    // Animación de desaparición
    //    yield return cardView.transform.DOScale(Vector3.zero, 0.15f).WaitForCompletion();

    //    // Solo desactivar en vez de destruir
    //    cardView.gameObject.SetActive(false);

    //    // Reorganizar solo si quedan cartas activas
    //    if (currentCardViews.Count > 0)
    //    {
    //        yield return handView.StartCoroutine(handView.UpdateCardPositions(0.15f));
    //    }
    //}

    //[ContextMenu("End Turn")]
    //public void EndPlayerTurn()
    //{
    //    if (!playerTurn) return;
    //    playerTurn = false;

    //    // Combine all cards played and unplayed
    //    List<Card> allCardsThisTurn = new List<Card>();
    //    allCardsThisTurn.AddRange(currentHand);
    //    allCardsThisTurn.AddRange(playedCardsThisTurn);

    //    // Return all cards (played + unplayed) to the deck
    //    deckManager.ReturnCards(allCardsThisTurn);

    //    currentHand.Clear();
    //    playedCardsThisTurn.Clear();

    //    // Deactivate all remaining visuals
    //    foreach (var cv in currentCardViews)
    //    {
    //        if (cv != null)
    //            cv.gameObject.SetActive(false);
    //    }
    //    currentCardViews.Clear();

    //    Debug.Log("Player turn ended. All cards returned to deck.");
    //    BattleLogUI.Instance.AddMessage("Player turn ended. All cards returned to deck.");

    //    StartCoroutine(EnemyTurnRoutine());
    //}


    //private IEnumerator EnemyTurnRoutine()
    //{
    //    Debug.Log("----- ENEMY TURN -----");
    //    BattleLogUI.Instance.AddMessage("----- ENEMY TURN -----");
    //    yield return new WaitForSeconds(1f);

    //    enemy.EnemyTurn(player);

    //    yield return new WaitForSeconds(1f);
    //    StartCoroutine(StartPlayerTurn());
    //}
}