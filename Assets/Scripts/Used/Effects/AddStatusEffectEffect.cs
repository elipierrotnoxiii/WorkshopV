using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffectEffect : Effect
{
    [SerializeField] private StatusEffectType statusEffectType;
    [SerializeField] private int stackCount;
    [SerializeField] private AudioClip applyStatusSFX;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        //return new AddStatusEffectGA(statusEffectType, stackCount, targets);
        AddStatusEffectGA ga = new(statusEffectType, stackCount, targets);
        ga.SetSound(applyStatusSFX);
        return ga;
    }
}
