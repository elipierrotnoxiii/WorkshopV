using Unity.Android.Gradle.Manifest;
using System.Collections.Generic;
using UnityEngine;

public class Perk
{
    public Sprite Image => data.Image;
    private readonly PerkData data;
    private readonly PerkCondition condition;
    private readonly AutoTargetEffect effect;
    private HeroView heroView;
    public Perk(PerkData perkData)
    {
        data = perkData;
        condition = data.PerkCondition;
        effect = data.AutoTargetEffect;
    }
    public void OnAdd()
    {
        condition.SubscribeCondition(Reaction);
    }
    public void OnRemove()
    {
        condition?.UnsubscribeCondition(Reaction);
    }
    private void Reaction(GameAction gameAction)
    {
        heroView = Object.FindFirstObjectByType<HeroView>();
        int cs = heroView.GetStatusEffectStacks(StatusEffectType.COUNTER);
        if (condition.SubConditionIsMet(gameAction) && cs > 0)
        {
            List<CombatantView> targets = new();
            if (data.UseActionCasterAsTarget && gameAction is IHaveCaster haveCaster)
            {
                targets.Add(haveCaster.Caster);
            }
            if (data.UseAutoTarget)
            {
                targets.AddRange(effect.TargetMode.GetTargets());
            }
            GameAction perkEffectAction = effect.Effect.GetGameAction(targets, HeroSystem.Instance.HeroView);
            ActionSystem.Instance.AddReaction(perkEffectAction);
            heroView.RemoveStatusEffect(StatusEffectType.COUNTER, 1);
        }
    }
}
