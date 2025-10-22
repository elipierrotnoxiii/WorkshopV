using UnityEngine;

public class TestSystem : MonoBehaviour
{
    public HandView handView;
    public Card cardData;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CardConstructor card = new(cardData);
            CardView cardView = CardViewCreator.Instance.CreateCardView(card, transform.position, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardView));
        }
    }
}
