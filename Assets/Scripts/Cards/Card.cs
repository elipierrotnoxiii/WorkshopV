using UnityEngine;
public enum CardEffectType
{
    Damage,
    Heal,
    Block
}

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/New Card")]
public class Card : ScriptableObject
{
    public string Title;
    public CardEffectType EffectType;
    public int Value;

    // Genera la descripción en el formato correcto
    public string GetFormattedDescription()
    {
        return EffectType switch
        {
            CardEffectType.Damage => $"Deals {Value} damage.",
            CardEffectType.Heal => $"Heals {Value} HP.",
            CardEffectType.Block => $"Gives {Value} block.",
            _ => "No effect."
        };
    }
}

