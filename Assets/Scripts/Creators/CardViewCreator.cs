using UnityEngine;
using DG.Tweening;

public class CardViewCreator : Singleton<CardViewCreator>
{
    public CardView cardViewPrefab;

    // Crea y configura la carta
    public CardView CreateCardView(CardConstructor cardData ,Vector3 position, Quaternion rotation)
    {
        // Instancia la carta
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);

        // Efecto de aparición
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.15f);

        // Configura los datos de la carta
        cardView.Setup(cardData);

        return cardView;
    }
}