using System;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour, IItem
{
    [Header("構造体")]
    [Tooltip("このアイテムがどんなアイテムか")]
    [SerializeField] private ItemType item = ItemType.BATTERY;
    public ItemType Item => item;

    [Header("コンポーネント参照")]
    [Tooltip("アイテムを持っているか判断する用の画像")]
    [SerializeField] private Image itemImage;
    public Image ItemImage => itemImage;

    //状態
    //プレイヤーがこのアイテムを所有しているかどうか
    private bool isPlayerHas;
    public bool IsPlayerHas => isPlayerHas;

    //イベント
    //アイテムを使用してみる
    public event Action OnTryUseItem;

    private void Awake()
    {
        if(itemImage == null) { Debug.LogError("itenImageが参照されていません"); return; }
    }

    /// <summary>
    /// このアイテムを取得した
    /// </summary>
    public void ItemGet()
    {
        isPlayerHas = true;
        itemImage.enabled = true;
    }

    /// <summary>
    /// このアイテムを使用した
    /// </summary>
    public void ItemUse()
    {
        isPlayerHas = false;
        itemImage.enabled = false;
    }
}
