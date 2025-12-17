using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private StatusEffectsUI statusEffectsUI;
    public int MaxHealth {  get; private set; }
    public int Currenthealth {  get; private set; }
    private Dictionary<StatusEffectType, int> statusEffects = new();
    protected void SetUpBase(int health, Sprite image, bool onlyIfZero = false)
{
    MaxHealth = health;

    if (!onlyIfZero || Currenthealth <= 0)
        Currenthealth = MaxHealth;

    spriteRenderer.sprite = image;
    UpdateHealthText();
}
    private void UpdateHealthText()
    {
        healthText.text = "HP: " + Currenthealth;
        // If this is the hero view, update persisted health in the HeroSystem
        if (GetComponent<HeroView>() != null && HeroSystem.Instance != null)
        {
            Debug.Log($"CombatantView.UpdateHealthText hero current={Currenthealth} max={MaxHealth}");
            HeroSystem.Instance.SetPersistedHealth(Currenthealth, MaxHealth);
        }
    }
    public void Damage(int damageAmount)
    {
        int remainingDamage = damageAmount;
        int currentArmor = GetStatusEffectStacks(StatusEffectType.ARMOR);
        if (currentArmor > 0)
        {
            if(currentArmor >= damageAmount)
            {
                RemoveStatusEffect(StatusEffectType.ARMOR, remainingDamage);
                remainingDamage = 0;
            }
            else if (currentArmor < damageAmount)
            {
                RemoveStatusEffect(StatusEffectType.ARMOR, currentArmor);
                remainingDamage -= currentArmor;
            }
        }
        if (remainingDamage > 0)
        {
            Currenthealth -= remainingDamage;
            if (Currenthealth < 0)
            {
                Currenthealth = 0;
            }
        }
        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }
    public void AddStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] += stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }
    public void RemoveStatusEffect(StatusEffectType type, int stackCount)
    {
        if(statusEffects.ContainsKey(type))
        {
            statusEffects[type] -= stackCount;
            if(statusEffects[type] <= 0)
            {
                statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }
    public int GetStatusEffectStacks(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }

    public void HealToMax()
    {
        Currenthealth = MaxHealth;
        UpdateHealthText();
    }

    // Load persisted state (used by HeroSystem to restore hero between scenes)
    public void LoadState(int current, int max, Sprite image)
    {
        MaxHealth = max;
        Currenthealth = current;
        if (spriteRenderer != null)
            spriteRenderer.sprite = image;
        UpdateHealthText();
    }
}
