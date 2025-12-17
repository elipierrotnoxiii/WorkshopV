using UnityEngine;

public class HeroSystem : PersistenSingleton<HeroSystem>
{
   [field: SerializeField] public HeroView HeroView { get; private set; }
    private int persistedCurrentHealth = -1;
    private int persistedMaxHealth = 0;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log($"HeroSystem.Awake instance={(Instance != null ? Instance.name : "null")} gameObject={name} (persistedCurrent={persistedCurrentHealth} persistedMax={persistedMaxHealth})");
    }

    void OnDestroy()
    {
        Debug.Log($"HeroSystem.OnDestroy instance={(Instance != null ? Instance.name : "null")} gameObject={name}");
    }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Setup(HeroData heroData)
    {
        // Initialize persisted values if needed
        if (persistedMaxHealth == 0)
            persistedMaxHealth = heroData.Health;

        if (persistedCurrentHealth <= 0)
            persistedCurrentHealth = persistedMaxHealth;

        if (HeroView != null)
        {
            HeroView.LoadState(persistedCurrentHealth, persistedMaxHealth, heroData.Image);
        }
    }

    // Called by CombatantView (only for hero) to persist health across scenes
    public void SetPersistedHealth(int current, int max)
    {
        persistedCurrentHealth = current;
        persistedMaxHealth = max;
        Debug.Log($"HeroSystem.SetPersistedHealth current={current} max={max}");
    }

    // Register the active HeroView in the current scene so persisted values can be applied
    public void RegisterHeroView(HeroView view)
    {
        HeroView = view;

        Debug.Log($"HeroSystem.RegisterHeroView view={(view != null ? view.name : "null")} persistedCurrent={persistedCurrentHealth} persistedMax={persistedMaxHealth}");

        if (persistedMaxHealth > 0 && HeroView != null)
        {
            int current = persistedCurrentHealth <= 0 ? persistedMaxHealth : persistedCurrentHealth;
            var sprite = view.GetComponent<SpriteRenderer>()?.sprite;
            HeroView.LoadState(current, persistedMaxHealth, sprite);
            Debug.Log($"HeroSystem applied LoadState current={current} max={persistedMaxHealth} to view={view.name}");
        }
    }

    // Reacciones durante el turno enemigo
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        if (HeroView.Currenthealth <= 0)
        {
            BattleFlowController.Instance.EndCombat(BattleResult.Lose);
            return;
        }

        int burnStacks = HeroView.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }

        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }
}
