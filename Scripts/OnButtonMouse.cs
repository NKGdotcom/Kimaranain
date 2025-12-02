using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnButtonMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image displayImage; //マウスの上にのっけたときに表示させる画像

    private bool isHovering = false; //マウスが乗っているか
    private static bool isPressed = false; //マウスを押している最中か

    /// <summary>
    /// マウスがUIの中に入ったら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if(displayImage != null && !isPressed)
        {
            displayImage.enabled = true;
            SoundManager.Instance.ChoiceSE();
        }
    }
    /// <summary>
    /// マウスから離れたら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if(!isPressed && displayImage != null) displayImage.enabled = false;

    }

    /// <summary>
    /// マウスをクリックして離したら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("クリックしました");
        SoundManager.Instance.DecisionSoundSE();
    }

    /// <summary>
    /// マウスを押したら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    /// <summary>
    /// マウスクリックを離したら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        if (!isHovering && displayImage != null) displayImage.enabled = false;
    }
}
