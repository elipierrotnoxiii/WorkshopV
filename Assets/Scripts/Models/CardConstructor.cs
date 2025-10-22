
public class CardConstructor
{
     private readonly Card data;

    public Card SourceCard => data;
    public string Title => data.Title;
    public string Description => data.GetFormattedDescription();
    public int Damage { get; private set; }
    public int Heal { get; private set; }
    public int Block { get; private set; }

    public CardConstructor(Card cardData)
    {
        data = cardData;

        // Asigna solo el valor correspondiente según el tipo de efecto
        switch (data.EffectType)
        {
            case CardEffectType.Damage:
                Damage = data.Value;
                Heal = 0;
                Block = 0;
                break;
            case CardEffectType.Heal:
                Damage = 0;
                Heal = data.Value;
                Block = 0;
                break;
            case CardEffectType.Block:
                Damage = 0;
                Heal = 0;
                Block = data.Value;
                break;
        }
    }
}
