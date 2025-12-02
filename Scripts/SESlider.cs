using UnityEngine;

public class SESlider : CustomSlider
{
    [SerializeField] private SoundVolumeData soundVolumeData;
    public override void Start()
    {
        base.Start();
        sliderUI.value = soundVolumeData.SEVolume;
    }
    public override void OnSliderUpdate(float _value)
    {
        base.OnSliderUpdate(_value);
        soundVolumeData.SEVolume = _value;
        Debug.Log("SE‚Ì‰¹—Ê‚ğ•ÏX‚µ‚Ü‚µ‚½");
    }
}
