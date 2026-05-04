using System.Collections;
using UnityEngine;

/// <summary>
/// タイトルUIを統括するクラス
/// </summary>
public class TitleUIManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    [Tooltip("ステージ選択するときに使用するオブジェクト")]
    [SerializeField] private GameObject stageSelectObj;
    [Tooltip("オプション画面のオブジェクト")]
    [SerializeField] private GameObject optionObj;

    [Header("コンポーネントボタン参照")]
    [Tooltip("ゲームスタートでオープニング、もしくはステージ選択に移るボタン")]
    [SerializeField] private TitleButton gameStartButton;
    [Tooltip("タイトル画面でオプションに移るボタン")]
    [SerializeField] private TitleButton optionButton;
    [Tooltip("ゲーム終了に移るボタン")]
    [SerializeField] private TitleButton exitButton;

    [Header("コンポーネント参照")]
    [Tooltip("オープニングのアニメーションを再生したかどうかを判定")]
    [SerializeField] private CheckTitleInformation checkTitleInfo;
    [Tooltip("ステージをクリアしているかを知っているクラス")]
    [SerializeField] private StageClearCheck stageClearCheck;
    [Tooltip("ステージ選択画面に映るアニメーション")]
    [SerializeField] private Animator stageSelectAnimator;

    //パラメータ
    //リセットをする際に使用するタイマー
    private float inputTime; //3秒を超えたらリセット完了
    //アニメーションが最大まで到達したことを示す
    private float maxAnimPer = 1;
    //オープニングシーンの名前
    private string openingSceneName = "Opening";
    //目的のアニメーション状態の名前
    private const string ANIM_STATE_NAME = "ToStageSelect";
    //ステージ選択に移る際に使用するアニメーションのトリガー名
    private const string ANIM_BOOL_NAME = "TitleToStageSelect";

    void Awake()
    {
        if(stageSelectObj == null) { Debug.LogError("stageSelectObjが参照されていません"); return; }
        if(optionObj == null) { Debug.LogError("optionObjが参照されていません"); return; }

        if(gameStartButton == null) { Debug.LogError("gameStartButtonが参照されていません"); return; }
        if(optionButton == null) { Debug.LogError("optionButtonが参照されていません"); return; }
        if(exitButton == null) { Debug.LogError("exitButtonが参照されていません"); return; }

        if (checkTitleInfo == null) { Debug.LogError("checkTitleInfoが参照されていません"); return; }
        if (stageClearCheck == null) { Debug.LogError("stageClearCheckが参照されていません"); return; }
        if (stageSelectAnimator == null) { Debug.LogError("stageSelectAnimatorが参照されていません"); return; }

        Time.timeScale = 1f;
        if (checkTitleInfo.StageToStageSelect)
        {
            stageSelectObj.SetActive(true);
        }

        gameStartButton.OnClicked += OpenGameStart;
        optionButton.OnClicked += OpenOption;
        exitButton.OnClicked += GameExit;
    }

    private void OnDestroy()
    {
        gameStartButton.OnClicked -= OpenGameStart;
        optionButton.OnClicked -= OpenOption;
        exitButton.OnClicked -= GameExit;
    }


    // Update is called once per frame
    void Update()
    {
        //オープニング再生をリセット
        if (Input.GetKey(KeyCode.Tab))
        {
            inputTime += Time.deltaTime;
            if (inputTime > 3f)
            {
                checkTitleInfo.IsPlayedStoryAnimation = false;
                inputTime = 0f;
            }
        }

        //ステージのクリア状態をリセット
        if (Input.GetKey(KeyCode.R))
        {
            inputTime += Time.deltaTime;
            if (inputTime > 3f)
            {
                stageClearCheck.ResetAllClearStatus();
                inputTime = 0f;
            }
        }
    }

    /// <summary>
    /// ゲームスタートボタンを押す
    /// </summary>
    private void OpenGameStart()
    {
        //既にストーリーを再生していたら
        if (checkTitleInfo.IsPlayedStoryAnimation)
        {
            StartCoroutine(ToStageSelectCorutine());
        }
        //ストーリーをまだ流していなかったら
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(openingSceneName);
        }
    }

    /// <summary>
    /// オプションボタンを押し、オプション画面を開く
    /// </summary>
    private void OpenOption()
    {
        optionObj.SetActive(true);
    }

    /// <summary>
    /// ゲーム終了ボタンを押して、ゲームを終了する
    /// </summary>
    private void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    /// <summary>
    /// 横から絵が差し込み、ステージ選択画面へ移る
    /// </summary>
    /// <returns></returns>
    private IEnumerator ToStageSelectCorutine()
    {
        //アニメーションの実行
        stageSelectAnimator.SetBool(ANIM_BOOL_NAME, true);
        //目的のアニメーションが開始されるまで待つ
        yield return new WaitUntil(() => stageSelectAnimator.GetCurrentAnimatorStateInfo(0).IsName(ANIM_STATE_NAME));
        //アニメーションが終わるまで待機
        yield return new WaitUntil(() =>
        stageSelectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= maxAnimPer && //アニメーションが終わるまで待ち
            !stageSelectAnimator.IsInTransition(0));
        //ステージ選択画面を表示
        stageSelectObj.SetActive(true);
    }
}
