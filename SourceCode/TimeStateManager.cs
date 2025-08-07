using UnityEngine;


public class TimeStateManager : MonoBehaviour
{
    public static TimeStateManager Instance { get; private set; }
    public TimeState NowState {  get; private set; }
    public enum TimeState
    {
        Normal,
        Rewinding,
        FastForward,
        StageIntroduce
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetState(TimeState _state)
    {
        NowState = _state;
    }
    /// <summary>
    /// 時を動かしてないとき
    /// </summary>
    /// <returns></returns>
    public bool IsNormalState()
    {
        return NowState == TimeState.Normal;
    }
    /// <summary>
    /// 時間を戻しているか
    /// </summary>
    /// <returns></returns>
    public bool IsRewindingState()
    {
        return NowState == TimeState.Rewinding;
    }
    /// <summary>
    /// 時間を進めているか
    /// </summary>
    /// <returns></returns>
    public bool IsFastForwardState()
    {
        return NowState == TimeState.FastForward;
    }
    /// <summary>
    /// ステージ紹介時間
    /// </summary>
    /// <returns></returns>
    public bool IsStageIntroduce()
    {
        return NowState == TimeState.StageIntroduce;
    }
}
