using UnityEngine;

/// <summary>
/// ステージのクリア状態を管理するクラス
/// </summary>
[CreateAssetMenu(fileName = "StageClearCheck", menuName = "ScriptableObjects/StageClearCheck")]
public class StageClearCheck : ScriptableObject
{
    public enum StageType
    {
        Park,Road,Kitchen,Living,Room
    }
    public bool GetClearStatus(StageType type)
    {
        switch (type)
        {
            case StageType.Park: return IsParkStageCleared;
            case StageType.Road: return IsRoadStageCleared;
            case StageType.Kitchen : return IsKitchenStageCleared;
            case StageType.Living: return IsLivingStageCleared;
            case StageType.Room: return IsRoomStageCleared;
            default: return false;
        }
    }

    [Header("状態")]
    [Tooltip("公園ステージをクリアしているかどうか")]
    [SerializeField] private bool isParkStageCleared;
    public bool IsParkStageCleared { get => isParkStageCleared; set => isParkStageCleared = value; }
    [Tooltip("道ステージをクリアしているかどうか")]
    [SerializeField] private bool isRoadStageCleared;
    public bool IsRoadStageCleared { get => isRoadStageCleared; set => isRoadStageCleared = value; }
    [Tooltip("キッチンステージをクリアしているかどうか")]
    [SerializeField] private bool isKitchenStageCleared;
    public bool IsKitchenStageCleared { get => isKitchenStageCleared; set => isKitchenStageCleared = value; }
    [Tooltip("リビングステージをクリアしているかどうか")]
    [SerializeField] private bool isLivingStageCleared;
    public bool IsLivingStageCleared { get => isLivingStageCleared; set => isLivingStageCleared = value; }
    [Tooltip("部屋ステージをクリアしているかどうか")]
    [SerializeField] private bool isRoomStageCleared;
    public bool IsRoomStageCleared { get => isRoomStageCleared; set => isRoomStageCleared = value; }

    /// <summary>
    /// すべてのクリア状態をリセットする
    /// </summary>
    public void ResetAllClearStatus()
    {
        isParkStageCleared = false;
        isRoadStageCleared = false;
        isKitchenStageCleared = false;
        isLivingStageCleared = false;
        isRoomStageCleared = false;
    }
}
