using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/New Card")]
public class Card : ScriptableObject
{
    public string cardName = "Strike";
    public int damage = 0;
    public int block = 0;
    public int heal = 0;

    public void Play(Player player, Enemy enemy)
    {
        if (damage > 0) enemy.TakeDamage(damage);
        if (block > 0) player.AddBlock(block);
        if (heal > 0) player.Heal(heal);
        Debug.Log($"{cardName} played! (Dmg:{damage} Blk:{block} Heal:{heal})");
    }
}

