using UnityEngine;

/// <summary>
/// ポーズ画面を統括するクラス
/// </summary>
public class PauseUIManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    [Tooltip("ゲームのオプション画面")]
    [SerializeField] private GameObject optionObj;

    [Header("コンポーネントボタン参照")]
    [Tooltip("ゲームシーンに戻るボタン")]
    [SerializeField] private HoverWhiteButton backGameButton;
    [Tooltip("オプション画面に映るボタン")]
    [SerializeField] private HoverWhiteButton optionButton;
    [Tooltip("ステージ選択画面に戻るボタン")]
    [SerializeField] private HoverWhiteButton stageSelectButton;
    [Tooltip("タイトル画面に戻るボタン")]
    [SerializeField] private HoverWhiteButton backTitleButton;

    [Header("コンポーネント参照")]
    [Tooltip("ステージ選択画面に直接移る")]
    [SerializeField] private CheckTitleInformation checkTitleInfo;

    //パラメータ
    //タイトルとステージ選択のシーン名
    private string titleSceneName = "TitleAndStageSelect";

    private void Awake()
    {
        if(optionObj == null) { Debug.LogError("optionObjが参照されていません"); return; }

        if (backGameButton == null) { Debug.LogError("backGameButtonが参照されていません"); return; }
        if (optionButton == null) { Debug.LogError("optionButtonが参照されていません"); return; }
        if (stageSelectButton == null) { Debug.LogError("stageSelectButtonが参照されていません"); return; }
        if (backTitleButton == null) { Debug.LogError("backTitleButtonが参照されていません"); return; }

        if (checkTitleInfo == null) { Debug.LogError("checkTitleInfoが参照されていません"); return; }

        backGameButton.OnClicked += BackGameScene;
        optionButton.OnClicked += OpenOption;
        stageSelectButton.OnClicked += ToStageSelect;
        backTitleButton.OnClicked += ToTitle;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        backGameButton.OnClicked -= BackGameScene;
        optionButton.OnClicked -= OpenOption;
        stageSelectButton.OnClicked -= ToStageSelect;
        backTitleButton.OnClicked -= ToTitle;
    }

    /// <summary>
    /// ゲームに戻るボタンを押したらゲームに戻る
    /// </summary>
    private void BackGameScene()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// オプションボタンを押したらオプション画面を開く
    /// </summary>
    private void OpenOption()
    {
        optionObj.SetActive(true);
    }

    /// <summary>
    /// ステージセレクトに戻るボタンを押したらステージ選択画面に戻る
    /// </summary>
    private void ToStageSelect()
    {
        checkTitleInfo.StageToStageSelect = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneName);
    }

    /// <summary>
    /// タイトルに戻るボタンを押したらタイトル画面に戻る
    /// </summary>
    private void ToTitle()
    {
        checkTitleInfo.StageToStageSelect = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneName);
    }
}
