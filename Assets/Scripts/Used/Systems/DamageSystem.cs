using System.Collections;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public GameObject panelGameOver, panelVictory;
    public int enemiesKilled;

    [SerializeField] private GameObject damageVFX;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        if (dealDamageGA.Sound != null)
        {
            AudioSource.PlayClipAtPoint(
                dealDamageGA.Sound,
                dealDamageGA.Caster.transform.position
            );
        }
        foreach (var target in dealDamageGA.Targets)
        {
            target.Damage(dealDamageGA.Amount);
            Instantiate(damageVFX,target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
            if(target.Currenthealth <= 0)
            {
                if(target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                    //enemiesKilled += 1;
                    //if (enemiesKilled >= 3)
                    //{
                    //    panelVictory.SetActive(true);
                    //}
                }
                else
                {
                    panelGameOver.SetActive(true);
                }
            }
        }
    }
}
