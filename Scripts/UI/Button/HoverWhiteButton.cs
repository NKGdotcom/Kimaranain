using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ボタンの上にマウスを置いた時、白くなるボタン
/// </summary>
public class HoverWhiteButton : BaseButton
{
    //コンポーネント参照
    [Tooltip("ボタンの上にマウスを置いたら白くするための画像")]
    [SerializeField] private Image hoverWhiteImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        base.Awake();
        if(hoverWhiteImage == null) { Debug.LogError("hoverWhiteImageが参照されていません"); return; }

        hoverWhiteImage.enabled = false;
    }

    /// <summary>
    /// ボタンの上にマウスを置いたら白くする
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        hoverWhiteImage.enabled = true;
    }

    /// <summary>
    /// ボタンの上からマウスを離したら元に戻す
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        hoverWhiteImage.enabled = false;
    }
}
