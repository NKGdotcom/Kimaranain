using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面を統括したクラス
/// </summary>
public class ResultUIManager : MonoBehaviour
{
    [Header("コンポーネントボタン参照")]
    [Tooltip("タイトル画面(ステージセレクト画面)に映るボタン")]
    [SerializeField] private HoverGrayButton toTitleButton;
    [Tooltip("次のステージに進むボタン")]
    [SerializeField] private HoverGrayButton toNextStageButton;

    [Header("コンポーネント参照")]
    [Tooltip("ステージ選択画面に直接移る")]
    [SerializeField] private CheckTitleInformation checkTitleInfo;

    [Header("パラメータ")]
    [Tooltip("次に進むステージのシーン名を入力")]
    [SerializeField] private string nextSceneName;

    //パラメータ
    //タイトルとステージ選択のシーン名
    private string titleSceneName = "TitleAndStageSelect";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(toTitleButton == null) { Debug.LogError("toTitleButtonが参照されていません"); return; }
        if(toNextStageButton == null) { Debug.LogError("toNextStageButtonが参照されていません"); return; }

        toTitleButton.OnClicked += ToTitle;
        toNextStageButton.OnClicked += ToNextStage;
    }

    /// <summary>
    /// タイトル画面に移る
    /// </summary>
    private void ToTitle()
    {
        checkTitleInfo.StageToStageSelect = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneName);
    }

    /// <summary>
    /// パラメータで設定したステージ(次のステージ)に移動
    /// </summary>
    private void ToNextStage()
    {
        if (nextSceneName == "") return;
        
        SceneManager.LoadScene(nextSceneName);
    }
}
