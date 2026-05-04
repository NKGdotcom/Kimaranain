using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ボタンのクリックを統括するクラス
/// </summary>
public class BaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("コンポーネント参照")]
    [Tooltip("ボタンの画像を用いてボタン処理を行う")]
    [SerializeField] private Image buttonImage;
    public Image ButtonImage => buttonImage;

    //イベント
    //クリックしたことを検知する
    public event Action OnClicked;

    public virtual void Awake()
    {
        if (buttonImage == null) { Debug.LogError("buttonImageが参照されていません"); return; }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke();
    }
}