using System.Collections;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
    }
    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGA)
    {
        if (addStatusEffectGA.Sound != null && addStatusEffectGA.Targets.Count > 0)
        {
            AudioSource.PlayClipAtPoint(
                addStatusEffectGA.Sound,
                addStatusEffectGA.Targets[0].transform.position
            );
        }
        foreach (var target in addStatusEffectGA.Targets)
        {
            target.AddStatusEffect(addStatusEffectGA.StatusEffectType, addStatusEffectGA.StackCount);
            yield return null;
        }
    }
}
