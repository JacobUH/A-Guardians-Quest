using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SphereCollider))]
public class ItemDropData : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] public int itemID;
    [SerializeField] public int quantity;
    [SerializeField] public bool isFloating;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private float collectSpeed;
    [SerializeField] private float collectRange;

    protected string itemGuid;

    private float gravity = -9.81f;
    private Vector3 dropVelocity;
    private Vector3 movement;

    private float scaleDamping;
    private Coroutine collecting;
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collecting != null) return;
        if (!other.gameObject.CompareTag("Player")) return;
        collecting = StartCoroutine(CollectCoroutine());
    }

    private IEnumerator PlayAudio()
    {
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        Initialize();
        movement.y = UnityEngine.Random.Range(3f, 6f);
    }

    private void Update()
    {
        if (!isFloating)
        {
            movement.y = Mathf.Lerp(movement.y, 0f, Time.deltaTime);
            if (controller.isGrounded)
            {
                dropVelocity.y = 0f;
            }
            else
            {
                dropVelocity.y += gravity * Time.deltaTime;
            }
            controller.Move((movement + dropVelocity) * Time.deltaTime);
        }
    }

    private void Initialize()
    {
        ItemData itemData = ItemDatabase.Instance.GetItemData(itemID);
        if (itemData.isStackable)
        {
            itemGuid = $"{itemID}";
        }
        else
        {
            Guid guid = Guid.NewGuid();
            itemGuid = $"{itemID}-{guid}";
        }
    }

    private IEnumerator CollectCoroutine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        while (Vector3.Distance(player.transform.position + Vector3.up * 0.5f, transform.position) > 0.25f)
        {
            Vector3 moveDirection = (player.transform.position + Vector3.up * 0.5f - transform.position).normalized;
            transform.Translate(moveDirection * Time.deltaTime * collectSpeed);
            yield return null;
        }
        InventoryBox.Instance.AddItem(itemGuid, quantity);
        EventHandler.OnPickUpItemEvent(itemGuid);
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(PlayAudio());
    }
}
