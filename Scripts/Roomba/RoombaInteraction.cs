using System;
using UnityEngine;
using UnityEngine.UI;

public class RoombaInteraction : MonoBehaviour, IInteraction
{
    public Image InteractUI => roombaImage;
    [SerializeField] private Image roombaImage;

    private BoxCollider roombaCollider;
    public event Action OnInteractionStart;
    public bool IsInteractable => isPlayerInRange && TimeStateManager.Instance.IsNormalState();
    private bool isPlayerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        roombaCollider = GetComponent<BoxCollider>();
        if (InteractUI == null) { Debug.LogError("InteractUIがアサインされていません。"); return; }
        HideUI();
    }

    void Update()
    {
        if(IsInteractable && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        roombaCollider.enabled = false;
        HideUI();
        OnInteractionStart?.Invoke();
    }

    public void ResetInteract()
    {
        roombaCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<playermove>(out var _player))
        {
            isPlayerInRange = true;
            ShowUI();
        }
    }

    public void ShowUI()
    {
        roombaImage.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<playermove>(out var _player))
        {
            isPlayerInRange = false;
            HideUI();
        }
    }

    public void HideUI()
    {
        roombaImage.enabled = false;
    }
}
