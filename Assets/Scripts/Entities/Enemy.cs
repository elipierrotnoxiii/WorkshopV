using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Slime";
    public int maxHP = 30;
    public int currentHP;

    [Header("Attack Settings")]
    public int minDamage = 4;
    public int maxDamage = 8;

    [Header("UI")]
    public TextMeshProUGUI hpText;

    private void Awake()
    {
        currentHP = maxHP;
        UpdateHPText();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"{enemyName} takes {amount} damage! HP left: {currentHP}/{maxHP}");
        BattleLogUI.Instance.AddMessage($"{enemyName} takes {amount} damage! HP left: {currentHP}/{maxHP}");
        UpdateHPText();

        if (currentHP <= 0)
        {
            Debug.Log($"{enemyName} is defeated!");
            BattleLogUI.Instance.AddMessage($"{enemyName} is defeated!");
        }
    }

    public void EnemyTurn(Player player)
    {
        if (currentHP <= 0)
        {
            Debug.Log($"{enemyName} is defeated and skips turn.");
            BattleLogUI.Instance.AddMessage($"{enemyName} is defeated and skips turn.");
            return;
        }

        int damage = Random.Range(minDamage, maxDamage + 1);
        Debug.Log($"{enemyName} attacks for {damage} damage!");
        BattleLogUI.Instance.AddMessage($"{enemyName} attacks for {damage} damage!");
        player.TakeDamage(damage);
    }

    public void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"{enemyName}\nHP: {currentHP}/{maxHP}";
        }
    }
}
