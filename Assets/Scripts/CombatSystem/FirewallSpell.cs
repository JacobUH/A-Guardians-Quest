using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallSpell : MonoBehaviour
{
    [SerializeField] private List<GameObject> fireWalls;
    [SerializeField] private float duration = 2.5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float knockback;
    [SerializeField] private float launchForce;
    [SerializeField] private float hitLagDuration;
    [SerializeField] private float hitLagStrength;

    private void Start()
    {
        Invoke(nameof(DestroySelf), duration);
        foreach (GameObject firewall in fireWalls)
        {
            firewall.GetComponent<AttackCollider>().SetAttack(damage, knockback, launchForce, hitLagDuration, hitLagStrength);
        }
    }

    private void Update()
    {
        foreach (GameObject firewall in fireWalls)
        {
            firewall.transform.Translate(firewall.transform.forward * Time.deltaTime * speed);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
