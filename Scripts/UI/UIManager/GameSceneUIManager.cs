using UnityEngine;

/// <summary>
/// ゲーム画面でのUIを統括するクラス
/// </summary>
public class GameSceneUIManager : MonoBehaviour
{
    [Header("ゲームオブジェクト参照")]
    [Tooltip("ポーズ画面のオブジェクトを参照")]
    [SerializeField] private GameObject pauseObj;

    [Header("コンポーネントボタン参照")]
    [Tooltip("ポーズ画面を開くためのボタン")]
    [SerializeField] private HoverGrayButton pauseButton;

    private void Awake()
    {
        if(pauseButton == null) { Debug.LogError("pauseButtonが参照されていません"); return; }
        if(pauseObj == null) { Debug.LogError("pauseButtonが参照されていません"); return; }

        pauseButton.OnClicked += OpenPause;
    }

    private void OnDestroy()
    {
        pauseButton.OnClicked -= OpenPause;
    }

    /// <summary>
    /// ポーズ画面を開く処理
    /// </summary>
    private void OpenPause()
    {
        pauseObj.SetActive(true);
    }
}
