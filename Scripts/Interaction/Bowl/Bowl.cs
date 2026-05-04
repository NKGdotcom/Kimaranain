using JetBrains.Annotations;
using System;
using UnityEngine;

/// <summary>
/// お椀を統括するクラス
/// </summary>
public class Bowl : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("お椀を外すためのインタラクション")]
    [SerializeField] private BaseInteraction bowlInteract;

    //イベント
    public event Action OnPickup;

    private void Awake()
    {
        if(bowlInteract == null) { Debug.LogError("bowlInteractが参照されていません"); return; }

        bowlInteract.OnInteractionStart += PickBowl;
    }

    /// <summary>
    /// お椀を取り、皿が浮くようにする
    /// </summary>
    public void PickBowl()
    {
        gameObject.SetActive(false);
        OnPickup?.Invoke();
    }
}
