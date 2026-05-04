using UnityEngine;
/// <summary>
/// タイトルとステージセレクトに関する情報を保持しておく
/// </summary>
[CreateAssetMenu(fileName = "CheckTitleInformation", menuName = "ScriptableObjects/CheckTitleInformation")]
public class CheckTitleInformation : ScriptableObject
{ 
    /// <summary>
    /// 既にオープニングのアニメーションを再生したかどうか
    /// </summary>
    public bool IsPlayedStoryAnimation { get; set; }

    /// <summary>
    /// ステージのオプションからステージに移動
    /// </summary>
    public bool StageToStageSelect { get; set; }
}
