using UnityEngine;

public class HeroView : CombatantView
{
    public void Setup(HeroData heroData)
    {
        SetUpBase(heroData.Health, heroData.Image);
    }
}
