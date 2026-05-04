using System.Collections;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("ゴールのアニメーター")]
    [SerializeField] private Animator goalAnimator;

    //パラメータ
    //鳩時計の扉を開く際に使う定数
    private const string OPEN_TRIGGER = "Open";

    void Awake()
    {
        if(goalAnimator == null) { Debug.LogError("goalAnimatorが参照されていません"); return; }
    }

    /// <summary>
    /// ゴールの扉を開くアニメーション
    /// </summary>
    public IEnumerator OpenGoalDoorCoroutine()
    {
        //アニメーションを再生
        goalAnimator.SetBool(OPEN_TRIGGER, true);
        yield return null;

        //アニメーションが終わるまで待機
        yield return new WaitUntil(() => goalAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }
}
