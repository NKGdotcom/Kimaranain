using UnityEngine;

/// <summary>
/// オプション画面の管理をするUI
/// </summary>
public class OptionUIManager : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("オプション画面を閉じるボタン")]
    [SerializeField] private HoverWhiteButton backButton;

    private void Awake()
    {
        if(backButton == null) { Debug.LogError("backButtonが参照されていません"); return; }

        backButton.OnClicked += ClosePage;
    }

    private void OnDestroy()
    {
        backButton.OnClicked -= ClosePage;
    }

    /// <summary>
    /// 現在開いているページを閉じる
    /// </summary>
    private void ClosePage()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
