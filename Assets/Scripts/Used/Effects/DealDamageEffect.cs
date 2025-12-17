using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    [SerializeField] private AudioClip hitSFX;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        DealDamageGA dealDamageGA = new(damageAmount, targets, caster);
        dealDamageGA.SetSound(hitSFX);
        return dealDamageGA;
    }
}
