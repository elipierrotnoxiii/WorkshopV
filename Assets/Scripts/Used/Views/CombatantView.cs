using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int MaxHealth {  get; private set; }
    public int Currenthealth {  get; private set; }
    protected void SetUpBase(int health, Sprite image)
    {
        MaxHealth = Currenthealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
    }
    private void UpdateHealthText()
    {
        healthText.text = "HP: " + Currenthealth;
    }
    public void Damage(int damageAmount)
    {
        Currenthealth -= damageAmount;
        if(Currenthealth < 0)
        {
            Currenthealth = 0;
        }
        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }
}
