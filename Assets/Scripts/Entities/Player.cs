using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int block = 0;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void AddBlock(int amount)
    {
        block += amount;
        Debug.Log($"Player gains {amount} block. Total block: {block}");
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(maxHP, currentHP + amount);
        Debug.Log($"Player heals {amount}. HP: {currentHP}/{maxHP}");
    }

    public void TakeDamage(int amount)
    {
        int remaining = amount - block;
        block = Mathf.Max(0, block - amount);
        if (remaining > 0) currentHP -= remaining;
        Debug.Log($"Player takes {amount} damage! HP: {currentHP}/{maxHP}");
    }
}
