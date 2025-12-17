using UnityEngine;

public class HeroView : CombatantView
{
    public void Setup(HeroData heroData)
    {
         SetUpBase(heroData.Health, heroData.Image, onlyIfZero: true);
    }

    void Awake()
    {
        Debug.Log($"HeroView.Awake registering {name}");
        if (HeroSystem.Instance != null)
            HeroSystem.Instance.RegisterHeroView(this);
    }

    void Start()
    {
        Debug.Log($"HeroView.Start checking registration for {name}");
        if (HeroSystem.Instance != null && HeroSystem.Instance.HeroView != this)
            HeroSystem.Instance.RegisterHeroView(this);
    }
}
