using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class FireballController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float changeDirectionSpeed = 5f;
    [SerializeField] private float lifeTime = 3.5f;
    [SerializeField] private float stopChaseTime = 3f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float knockback;
    [SerializeField] private float launchForce;
    [SerializeField] private float hitLagDuration;
    [SerializeField] private float hitLagStrength;
    [SerializeField] private GameObject hitEffectPrefab;

    private GameObject player;
    private float timer;
    private float chaseTime;

    private void Start()
    {
        chaseTime = Random.Range(1.6f, 2.4f);
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke(nameof(DestroySelf), lifeTime);
        GetComponent<AttackCollider>().SetAttack(damage, knockback, launchForce, hitLagDuration, hitLagStrength, hitEffectPrefab);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < chaseTime) return;

        controller.Move(Time.deltaTime * speed * transform.forward);

        if (timer < stopChaseTime)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 lookPos = player.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, changeDirectionSpeed * Time.deltaTime);
    }

    private void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }
}
