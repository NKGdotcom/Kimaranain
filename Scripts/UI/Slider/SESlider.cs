using UnityEngine;

public class SESlider : BaseSlider
{
    [Header("コンポーネント参照")]
    [Tooltip("音をScriptableObjectで保存し、別のシーンでも読み取り可能")]
    [SerializeField] private SoundVolumeData soundVolumeData;

    public override void Awake()
    {
        base.Awake();
        sliderUI.value = soundVolumeData.SEVolume;
    }

    /// <summary>
    /// SEの音量をSliderに応じて変更
    /// </summary>
    /// <param name="_value"></param>
    public override void SliderUpdate(float _value)
    {
        base.SliderUpdate(_value);
        soundVolumeData.SEVolume = _value;
        SoundManager.Instance.SetSEVolume();
        Debug.Log("SEの音量を変更しました");
    }
}

