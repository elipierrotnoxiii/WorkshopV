using UnityEngine;

public class CardViewClickHandler : MonoBehaviour
{
    private CombatManager combatManager;
    private CardView cardView;

    public void Initialize(CombatManager manager, CardView view)
    {
        combatManager = manager;
        cardView = view;
    }

    private void OnMouseDown()
    {
        //combatManager.PlayCardVisual(cardView);
    }
}
