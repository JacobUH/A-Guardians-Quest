using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private string ignoreTag;

    private GameObject hitEffectPrefab;
    private int damage;
    private float knockback;
    private float launchForce;
    private float hitLagDuration;
    private float hitLagStrength;

    private List<Collider> alreadyCollideWith = new List<Collider>();
    private Coroutine hitLagCoroutine;

    private void OnEnable()
    {
        alreadyCollideWith.Clear();
    }

    private void OnDisable()
    {
        if (hitLagCoroutine != null) StopCoroutine(hitLagCoroutine);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (other.CompareTag(ignoreTag)) return;
        if (alreadyCollideWith.Contains(other)) return;

        alreadyCollideWith.Add(other);

        if (other.TryGetComponent<IDamageable>(out IDamageable damageableTarget))
        {
            if (damageableTarget.TryDealDamage(damage))
            {
                if (hitEffectPrefab != null)
                {
                    GameObject hitEffect = Instantiate(hitEffectPrefab);
                    hitEffect.transform.position = other.ClosestPoint(transform.position);
                }

                hitLagCoroutine = StartCoroutine(HitLag());

                if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
                {
                    Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
                    direction.y = 0f;
                    forceReceiver.ApplyImpact(direction * knockback + Vector3.up * launchForce);
                }
            }
        }
        //this.gameObject.SetActive(false);
    }

    private IEnumerator HitLag()
    {
        Time.timeScale = 1f - hitLagStrength;

        float timer = 0f;
        while (timer < hitLagDuration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        hitLagCoroutine = null;
    }

    public void SetAttack(int damage, float knockback, float launchForce,float hitLagDuration, float hitLagStrength, GameObject hitEffectPrefab = null)
    {
        this.damage = damage;
        this.knockback = knockback;
        this.launchForce = launchForce;
        this.hitLagDuration = hitLagDuration;
        this.hitLagStrength = Mathf.Clamp01(hitLagStrength);
        this.hitEffectPrefab = hitEffectPrefab;
    }
}
