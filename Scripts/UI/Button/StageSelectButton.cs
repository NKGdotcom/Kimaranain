using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ステージ選択画面のボタン
/// </summary>
public class StageSelectButton : BaseButton
{
    //コンポーネント参照
    [Tooltip("選択中にクレヨンを表示させる")]
    [SerializeField] private Image crayonImage;
    [Tooltip("選択中にアニメーションを実行するクラス")]
    [SerializeField] private Animator selectAnimation;

    [Header("パラメータ")]
    [Tooltip("進むステージ名を入力")]
    [SerializeField] private string stageSceneName;

    //パラメータ
    //選択しているときのトリガー
    private string selectTrigger;
    //クリア時のトリガー
    private string clearTrigger;
    public override void Awake()
    {
        base.Awake();
        if (crayonImage == null) { Debug.LogError("crayonImageが参照されていません"); return; }
        if (selectAnimation == null) { Debug.LogError("selectAnimationが参照されていません"); return; }

        crayonImage.enabled = false;
    }

    private void Start()
    {
        selectAnimation.SetBool(clearTrigger, true);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SceneManager.LoadScene(stageSceneName);
    }

    /// <summary>
    /// ボタンの上にマウスを置いたらクレヨンを置き、選択アニメーションを行う
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        crayonImage.enabled = true;
        selectAnimation.SetBool(selectTrigger, true);
    }

    /// <summary>
    /// ボタンの上からマウスを離したらクレヨンを消し、選択アニメーションも終了
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        crayonImage.enabled = false;
        selectAnimation.SetBool(selectTrigger, false);
    }
}
