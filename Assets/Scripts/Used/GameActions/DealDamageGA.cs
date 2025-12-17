using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DealDamageGA : GameAction, IHaveCaster
{
    public int Amount { get; set; }
    public List<CombatantView> Targets { get; set; }
    public CombatantView Caster { get; private set; }
    public AudioClip Sound { get; private set; }
    public DealDamageGA(int amount, List<CombatantView> targets, CombatantView caster)
    {
        Amount = amount;
        Targets = new(targets);
        Caster = caster;
    }
    public void SetSound(AudioClip clip)
    {
        Sound = clip;
    }
}
