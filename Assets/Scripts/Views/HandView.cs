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
        cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }
    
    private IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;
        float cardSpacing = 1f / 10f;
        float firtsCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2f;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float targetPosition = firtsCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(targetPosition);
            Vector3 forward = spline.EvaluateTangent(targetPosition);
            Vector3 up = spline.EvaluateUpVector(targetPosition);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }

}
