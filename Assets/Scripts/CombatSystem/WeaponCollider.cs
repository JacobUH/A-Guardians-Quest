using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private Collider myCollider;
    [SerializeField] private float hitLagDuration = 0.1f;
    [SerializeField] [Range(0f, 1f)] private float hitLagStrength = 0f;

    private int damage;
    private float knockback;

    private List<Collider> alreadyCollideWith = new List<Collider>();

    private bool isActive = false;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (other == myCollider) return;
        if (alreadyCollideWith.Contains(other)) return;

        alreadyCollideWith.Add(other);

        GameObject hitEffect = Instantiate(hitEffectPrefab);
        hitEffect.transform.position = other.ClosestPoint(transform.position);

        if (other.TryGetComponent<IDamageable>(out IDamageable damageableTarget))
        {
            StartCoroutine(HitLag());
            damageableTarget.DealDamage(damage);
        }
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.ApplyImpact(direction * knockback);
        }
        isActive = false;
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
    }

    public void SetAttack(int damage, float knockback, float hitLagDuration, float hitLagStrength)
    {
        this.damage = damage;
        this.knockback = knockback;
        this.hitLagDuration = hitLagDuration;
        this.hitLagStrength = Mathf.Clamp01(hitLagStrength);
        EnableHitBox();
    }

    public void EnableHitBox()
    {
        alreadyCollideWith.Clear();
        isActive = true;
    }

    public void DisableHitBox()
    {
        isActive = false;
    }
}
