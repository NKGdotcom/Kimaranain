using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : OnButtonMouse
{
    /// <summary>
    ///ゲームをやめるをクリックしたら
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ビルド後のアプリ終了
#endif
    }
}
