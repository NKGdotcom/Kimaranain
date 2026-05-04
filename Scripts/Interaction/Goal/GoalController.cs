using System.Collections;
using UnityEngine;

/// <summary>
/// ゴールを統括するクラス
/// </summary>
public class GoalController : MonoBehaviour
{
    [Header("構造体")]
    [Tooltip("このギミックを使うために必要なアイテム")]
    [SerializeField] private ItemType item;

    [Header("コンポーネント参照")]
    [Tooltip("ゴールの扉のアニメーションを管理するクラス")]
    [SerializeField] private GoalAnimation animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(animator == null) { Debug.LogError("animatorが参照されていません"); return; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(TryGetComponent<PlayerItem>(out var _playerHaveItem))
        {
            foreach (var _item in _playerHaveItem.HaveItemsList)
            {
                if (item == _item.Item)
                {
                    _item.ItemUse();
                    StartCoroutine(GoalSequenceCoroutine());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// ゴール演出を見せる処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoalSequenceCoroutine()
    {
        //アニメーションが終わるまで待機
        yield return StartCoroutine(animator.OpenGoalDoorCoroutine());

        //クリア画面に移る
    }
}
