using UnityEngine;

public class BGMSlider : CustomSlider
{
    [SerializeField] private SoundVolumeData soundVolumeData;

    public override void Start()
    {
        base.Start();
        sliderUI.value = soundVolumeData.BGMVolume;
    }
    public override void OnSliderUpdate(float _value)
    {
        base.OnSliderUpdate(_value);
        soundVolumeData.BGMVolume = _value;
        SoundManager.Instance.SetBGMVolume();
        Debug.Log("BGM‚Ì‰¹—Ê‚ğ•ÏX‚µ‚Ü‚µ‚½");
    }
}
