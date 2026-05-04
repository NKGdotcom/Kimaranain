using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// インタラクトのできるもののアイテムを定義する
/// </summary>
public interface IInteraction
{
    /// <summary>
    /// インタラクト用のUI
    /// </summary>
    Image InteractUI { get; } 

    /// <summary>
    /// インタラクト可能かどうか
    /// </summary>
    bool IsInteractable { get; } 

    /// <summary>
    /// インタラクション開始イベント
    /// </summary>
    event Action OnInteractionStart; 
    
    /// <summary>
    /// インタラクションを実行するメソッド
    /// </summary>
    void Interact(); 
    
    /// <summary>
    /// UIを表示するメソッド
    /// </summary>
    void ShowUI(); 

    /// <summary>
    /// UIを非表示にするメソッド
    /// </summary>
    void HideUI(); 
}
