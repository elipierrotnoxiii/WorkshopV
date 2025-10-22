using UnityEngine;
using TMPro;

public class CardView : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public SpriteRenderer imageSR;
    public GameObject wrapper;

    public CardConstructor CardConstructor { get; private set; }

    public void Setup(CardConstructor card)
    {
        CardConstructor = card;
        titleText.text = card.Title;
        descriptionText.text = card.Description;
    }

    void OnMouseEnter()
    {
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(CardConstructor, pos);
    }
    
    void OnMouseExit()
    {
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
}
