using UnityEngine;

/// <summary>
/// 鳩時計の動きを統括する処理
/// </summary>
public class CuckooClockController : MonoBehaviour
{
    [Header("構造体")]
    [Tooltip("このギミックを使うために必要なアイテム")]
    [SerializeField] private ItemType item;

    [Header("コンポーネント参照")]
    [Tooltip("インタラクションの範囲を取るクラス")]
    [SerializeField] private BaseInteraction interact;
    [Tooltip("アニメーションを管理するクラス")]
    [SerializeField] private CuckooClockAnimation animator;

    //状態
    //ギミックの処理が実行可能かどうか
    private bool canExcute = false;

    private void Awake()
    {
        if(interact == null) { Debug.LogError("interactが参照されていません"); return; }
        if(animator == null) { Debug.LogError("animatorが参照されていません"); return; }

        interact.OnTryUseItem += TryUseItem;
        TimeStateManager.Instance.OnFutureTimeline += OpenClockDoor;
        TimeStateManager.Instance.OnNormalTimeline += CloseClockDoor;
    }

    private void OnDestroy()
    {
        interact.OnTryUseItem -= TryUseItem;
        TimeStateManager.Instance.OnFutureTimeline -= OpenClockDoor;
        TimeStateManager.Instance.OnNormalTimeline -= CloseClockDoor;
    }

    /// <summary>
    /// アイテムを使ってみる処理
    /// </summary>
    /// <param name="_playerHaveItem"></param>
    private void TryUseItem(PlayerItem _playerHaveItem)
    {
        //もし設定した構造体のアイテムを所有していたらアイテムを使う
        foreach(var _item in _playerHaveItem.HaveItemsList)
        {
            if(item == _item.Item)
            {
                _item.ItemUse();
                canExcute = true;
                break;
            }
        }
    }

    /// <summary>
    /// アニメーションで鳩時計の扉を開け、鍵をゲットできるように
    /// </summary>
    private void OpenClockDoor()
    {
        if (canExcute)
        {
            animator.OpenClockDoor();
        }
    }

    /// <summary>
    /// アニメーションで鳩時計の扉を閉め、鍵をしまう
    /// </summary>
    private void CloseClockDoor()
    {
        if (canExcute)
        {
            animator.CloseClockDoor();
        }
    }
}
