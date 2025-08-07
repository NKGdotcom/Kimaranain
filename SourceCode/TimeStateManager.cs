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
    /// ���𓮂����ĂȂ��Ƃ�
    /// </summary>
    /// <returns></returns>
    public bool IsNormalState()
    {
        return NowState == TimeState.Normal;
    }
    /// <summary>
    /// ���Ԃ�߂��Ă��邩
    /// </summary>
    /// <returns></returns>
    public bool IsRewindingState()
    {
        return NowState == TimeState.Rewinding;
    }
    /// <summary>
    /// ���Ԃ�i�߂Ă��邩
    /// </summary>
    /// <returns></returns>
    public bool IsFastForwardState()
    {
        return NowState == TimeState.FastForward;
    }
    /// <summary>
    /// �X�e�[�W�Љ��
    /// </summary>
    /// <returns></returns>
    public bool IsStageIntroduce()
    {
        return NowState == TimeState.StageIntroduce;
    }
}
