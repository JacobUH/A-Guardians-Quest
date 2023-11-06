using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private AttackCollider weaponCollider;
    [SerializeField] private List<Attack> attacks;
    [SerializeField] private GameObject swordTrailEffect;

    public void EnabledAttack(int index)
    {
        if (index >= attacks.Count())
        {
            Debug.Log($"No attack information at index {index}");
            return;
        }
        weaponCollider.gameObject.SetActive(true);
        weaponCollider.SetAttack(attacks[index].damage, attacks[index].knockBack, attacks[index].launchForce,
                         attacks[index].hitLagDuration, attacks[index].hitLagStrength, attacks[index].hitEffectPrefab);
    }

    public void EnabledSwordTrail()
    {
        if (swordTrailEffect != null) swordTrailEffect.SetActive(true);
    }
    public void DisabledSwordTrail()
    {
        if (swordTrailEffect != null) swordTrailEffect.SetActive(false);
    }

    public void HitboxDisabled()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void EnableProjectile(int index)
    {
        if (index >= attacks.Count())
        {
            Debug.Log($"No attack information at index {index}");
            return;
        }
        if (attacks[index].projectilePrefab != null)
        {
            GameObject projectile = Instantiate(attacks[index].projectilePrefab, this.transform.position + Vector3.up * 1f + this.transform.forward, this.transform.rotation);
            AttackCollider projectileCollider = projectile.GetComponent<AttackCollider>();
            projectileCollider.SetAttack(attacks[index].damage, attacks[index].knockBack, attacks[index].launchForce,
                             attacks[index].hitLagDuration, attacks[index].hitLagStrength, attacks[index].hitEffectPrefab);
            StartCoroutine(ProjectileCoroutine(projectile, attacks[index].projectileSpeed, attacks[index].projectileLifetime));
        }
    }

    private IEnumerator ProjectileCoroutine(GameObject projectile, float speed, float lifetime)
    {
        float timer = 0f;
        while (timer <= lifetime)
        {
            projectile.transform.position += projectile.transform.forward * speed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        GameObject.Destroy(projectile);
    }
}
