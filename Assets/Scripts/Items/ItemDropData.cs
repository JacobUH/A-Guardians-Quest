using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SphereCollider))]
public class ItemDropData : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] public int itemID;
    [SerializeField] public int quantity;
    [SerializeField] public bool isFloating;

    [SerializeField] protected string itemGuid;

    private float gravity = -9.81f;
    private Vector3 dropVelocity;
    private Vector3 movement;

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        InventoryBox.Instance.AddItem(itemGuid, quantity);
        EventHandler.OnPickUpItemEvent(itemGuid);
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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
}
