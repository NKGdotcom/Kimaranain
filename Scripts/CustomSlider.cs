using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomSlider : OnButtonMouse
{
    [SerializeField] protected Slider sliderUI; //使うスライダー

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        sliderUI.onValueChanged.AddListener(OnSliderUpdate);
    }

    public virtual void OnSliderUpdate(float _value)
    {

    }
    public override void OnPointerClick(PointerEventData eventData)
    {

    }
}
