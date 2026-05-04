using System;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    MAP,
    KEY,
    BATTERY
}

/// <summary>
/// 取得できるアイテムを管理するクラス
/// </summary>
public interface IItem
{
    ItemType Item { get; }
    /// <summary>
    /// アイテムを持っているかどうか表示する際に使う画像
    /// </summary>
    Image ItemImage { get; }

    /// <summary>
    /// そのアイテムを現在所有しているか
    /// </summary>
    bool IsPlayerHas { get; }

    /// <summary>
    /// アイテムを使用してみる
    /// </summary>
    event Action OnTryUseItem;

    /// <summary>
    /// このアイテムを取得した
    /// </summary>
    void ItemGet() { }

    /// <summary>
    /// このアイテムを使用
    /// </summary>
    void ItemUse() { }
}
