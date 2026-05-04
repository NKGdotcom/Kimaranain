using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 音の変更で使用する基本のスライダークラス
/// </summary>
public class BaseSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("コンポーネント参照")]
    [Tooltip("マウスでスライダーを動かすためのUI")]
    [SerializeField] protected Slider sliderUI;
    [Tooltip("スライダーを満たすための画像")]
    [SerializeField] private Image sliderImage;
    [Tooltip("スライダーを選択している最中に表示させる画像(黄色い枠")]
    [SerializeField] private Image selectImage;

    //状態
    //パラメータを更新中
    private bool updating = false;
    //スライダーの上にマウスを置いているか
    private bool isEnterMouse = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Awake()
    {
        if(sliderUI == null) { Debug.LogError("sliderUIが参照されていません"); return; }
        if(sliderImage == null) { Debug.LogError("sliderImageが参照されていません"); return; }
        if (selectImage == null) { Debug.LogError("selectImageが参照されていません"); return; }

        sliderUI.onValueChanged.AddListener(SliderUpdate);
    }

    /// <summary>
    /// マウスがUIの中に入ったら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //既に更新途中の場合は処理をしない
        if(updating) return;

        isEnterMouse = true;
        selectImage.enabled = true;
        SoundManager.Instance.ChoiceSE();
    }
    /// <summary>
    /// マウスから離れたら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (updating) return;

        isEnterMouse = false;
        selectImage.enabled = false;
    }

    /// <summary>
    /// スライダーをクリック（タップ）し始めたら呼ばれる
    /// </summary>
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        updating = true;
    }

    /// <summary>
    /// スライダーのクリック（タップ）を離したら呼ばれる
    /// </summary>
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        updating = false;
        if (!isEnterMouse)
        {
            selectImage.enabled = false;
        }
    }

    /// <summary>
    /// スライダーのfillを画像に変換し画像が縮んだりしないようにする
    /// </summary>
    /// <param name="_value"></param>
    public virtual void SliderUpdate(float _value)
    {
        updating = true;
        sliderImage.fillAmount = _value;
    }
}
