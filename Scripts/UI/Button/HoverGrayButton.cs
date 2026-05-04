using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ボタンの上にマウスを置いた時、グレーにするボタン
/// </summary>
public class HoverGrayButton : BaseButton
{
    /// <summary>
    /// ボタンの上にマウスを置いたらグレー色にする
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        ButtonImage.color = Color.gray;
    }

    /// <summary>
    /// ボタンの上からマウスを離したら元の色に戻す
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        ButtonImage.color= Color.white;
    }
}
