using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// タイトル画面で使用されるボタン
/// </summary>
public class TitleButton : BaseButton
{
    //コンポーネント参照
    [Tooltip("ボタンの上にマウスを置いた時、クレヨンを表示する")]
    [SerializeField] private Image crayonImage;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        base.Awake();
        if (crayonImage == null) { Debug.LogError("crayonImageが参照されていません"); return; }

        crayonImage.enabled = false;
    }

    /// <summary>
    /// ボタンの上にマウスを置いたら白くする
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        crayonImage.enabled = true;
    }

    /// <summary>
    /// ボタンの上からマウスを離したら元に戻す
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        crayonImage.enabled = false;
    }
}
