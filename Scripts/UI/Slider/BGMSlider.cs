using UnityEngine;

/// <summary>
/// BGMの音量を制御するクラス
/// </summary>
public class BGMSlider : BaseSlider
{
    [Header("コンポーネント参照")]
    [Tooltip("音をScriptableObjectで保存し、別のシーンでも読み取り可能")]
    [SerializeField] private SoundVolumeData soundVolumeData;

    public override void Awake()
    {
        base.Awake();
        sliderUI.value = soundVolumeData.BGMVolume;
    }

    /// <summary>
    /// BGMの音量をSliderに応じて変更
    /// </summary>
    /// <param name="_value"></param>
    public override void SliderUpdate(float _value)
    {
        base.SliderUpdate(_value);
        soundVolumeData.BGMVolume = _value;
        SoundManager.Instance.SetBGMVolume();
        Debug.Log("BGMの音量を変更しました");
    }
}
