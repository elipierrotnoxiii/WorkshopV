
using UnityEngine;

public class Card
{
    private readonly CardData data;

    //public Card SourceCard => data;
    public string Title => data.name;
    public string Description => data.Description;

    public Sprite Image => data.Image;

    public int Mana { get; private set; }
    //public int Damage { get; private set; }
    //public int Heal { get; private set; }
    //public int Block { get; private set; }

    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.Mana;

        // Asigna solo el valor correspondiente según el tipo de efecto
        //switch (data.EffectType)
        //{
        //    case CardEffectType.Damage:
        //        Damage = data.Value;
        //        Heal = 0;
        //        Block = 0;
        //        break;
        //    case CardEffectType.Heal:
        //        Damage = 0;
        //        Heal = data.Value;
        //        Block = 0;
        //        break;
        //    case CardEffectType.Block:
        //        Damage = 0;
        //        Heal = 0;
        //        Block = data.Value;
        //        break;
        //}
    }
}
