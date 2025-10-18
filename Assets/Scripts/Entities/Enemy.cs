using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Slime";
    public int maxHP = 30;
    public int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"{enemyName} takes {amount} damage! HP left: {currentHP}/{maxHP}");
    }
}
