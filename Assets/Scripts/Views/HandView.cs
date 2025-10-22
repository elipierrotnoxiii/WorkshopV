using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class HandView : MonoBehaviour
{
    public SplineContainer splineContainer;

    private readonly List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        if (!cards.Contains(cardView))
            cards.Add(cardView);

        yield return UpdateCardPositions(0.15f);
    }

    public IEnumerator UpdateCardPositions(float duration)
    {
        // Filtrar solo cartas activas
        List<CardView> activeCards = cards.FindAll(c => c.gameObject.activeSelf);

        if (activeCards.Count == 0) yield break;

        float cardSpacing = 1f / 10f;
        float firstCardPosition = 0.5f - (activeCards.Count - 1) * cardSpacing / 2f;
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < activeCards.Count; i++)
        {
            float targetPosition = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(targetPosition);
            Vector3 forward = spline.EvaluateTangent(targetPosition);
            Vector3 up = spline.EvaluateUpVector(targetPosition);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

            activeCards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            activeCards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}