using UnityEngine;

/// <summary>
/// 鳩時計のアニメーションを管理するクラス
/// </summary>
public class CuckooClockAnimation : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("鳩時計のアニメーター")]
    [SerializeField] private Animator clockAnimator;

    //パラメータ
    //鳩時計の扉を開く際に使う定数
    private const string OPEN_TRIGGER = "Open";
    //鳩時計の扉を閉める際に使う定数
    private const string CLOSE_TRIGGER = "Close";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(clockAnimator == null) { Debug.LogError("clockAnimatorが参照されていません"); return; }
    }

    /// <summary>
    /// 鳩時計の扉を開くアニメーション
    /// </summary>
    public void OpenClockDoor()
    {
        clockAnimator.SetTrigger(OPEN_TRIGGER);
    }

    /// <summary>
    /// 鳩時計の扉を閉めるアニメーション
    /// </summary>
    public void CloseClockDoor()
    {
        clockAnimator.SetTrigger(CLOSE_TRIGGER);
    }
}
