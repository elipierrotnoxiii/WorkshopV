using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Slime";
    public int maxHP = 30;
    public int currentHP;

    [Header("Attack Settings")]
    public int minDamage = 4;
    public int maxDamage = 8;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"{enemyName} takes {amount} damage! HP left: {currentHP}/{maxHP}");

        if (currentHP <= 0)
        {
            Debug.Log($"{enemyName} is defeated!");
        }
    }

    public void EnemyTurn(Player player)
    {
        if (currentHP <= 0)
        {
            Debug.Log($"{enemyName} is defeated and skips turn.");
            return;
        }

        int damage = Random.Range(minDamage, maxDamage + 1);
        Debug.Log($"{enemyName} attacks for {damage} damage!");
        player.TakeDamage(damage);
    }
}
