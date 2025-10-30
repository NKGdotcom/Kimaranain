using UnityEngine;

[CreateAssetMenu(fileName = "StageClearCheck", menuName = "ScriptableObjects/StageClearCheck")]
public class StageClearCheck : ScriptableObject
{
    public bool IsParkStageCleared { get; set; }
    public bool IsRoadStageCleared { get; set; }
    public bool IsKitchenStageCleared { get; set; }
    public bool IsLivingStageCleared { get; set; }
    public bool IsRoomStageCleared { get; set; }
}
