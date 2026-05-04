using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;

    [SerializeField] private SoundVolumeData soundVolumeData;
    [SerializeField] private SoundList soundList;

    private float bgmVolume = 1f;
    private float seVolume = 1f;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void PlayBGM(BGMSource bgmSource)
    {
        SoundList.BGMSoundData bgmData = soundList.GetBGMData(bgmSource);
        if (bgmData != null && bgmData.BGMAudioClip != null)
        {
            bgmAudioSource.clip = bgmData.BGMAudioClip;
            bgmAudioSource.volume = soundVolumeData.BGMVolume;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM {bgmSource} が見つかりません");
        }
    }
    /// <summary>
    /// BGMの音量調節
    /// </summary>
    public void SetBGMVolume()
    {
        bgmAudioSource.volume = soundVolumeData.BGMVolume;
    }
    /// <summary>
    /// SEの音量調節
    /// </summary>
    public void SetSEVolume()
    {
        seAudioSource.volume = soundVolumeData.SEVolume;
    }
    public void PlaySE(SESource seSource)
    {
        SoundList.SESoundData seData = soundList.GetSEData(seSource);
        if (seData != null && seData.SEAudioClip != null)
        {
            seAudioSource.PlayOneShot(seData.SEAudioClip, soundVolumeData.SEVolume);
        }
        else
        {
            Debug.LogWarning($"SE {seSource} が見つかりません");
        }
    }
    public void PlayFootSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.FootSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEFootVolume);
    }
    public void PlayMoveTimeSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.OnTheWayMoveTimeSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEOnTheWayMoveTimeVolume);
    }
    public void PlayFinishMoveTime()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.FinishMoveTimeSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEFinishMoveTimeVolume);
    }
    public void SignalSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.SignalSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SESignalVolume);
    }
    public void DriveSE()
    {
        if (!seAudioSource.isPlaying)
        {
            seAudioSource.PlayOneShot(soundList.GetSEData(SESource.DriveCarSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEDriveCarVolume);
        }
    }
    public void StopCarSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.StopIdleCarSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEStopCar);
    }
    public void IdleCar()
    {
        if (!seAudioSource.isPlaying)
        {
            seAudioSource.PlayOneShot(soundList.GetSEData(SESource.StopIdleCarSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEStopIdleCar);
        }
    }
    public void GetKeySE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.GetKeySE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEGetKeyVolume);
    }
    public void ChangeBGMVolume()
    {
        bgmAudioSource.volume = soundVolumeData.BGMVolume;
    }
    /// <summary>
    /// アラームを鳴らす音
    /// </summary>
    public void AlarmClockSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.AlarmClockSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEAlarmClockVolume);
    }
    /// <summary>
    /// ブレーカーが落ちる音
    /// </summary>
    public void BreakerTripsVolume()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.BreakerTripsSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEBreakerTripsVolume);
    }
    /// <summary>
    /// 寝息の音
    /// </summary>
    public void BreathingSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.BreathingSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEBreathingVolume);
    }
    /// <summary>
    /// UI選択音
    /// </summary>
    public void ChoiceSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.ChoiceSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEChoiceVolume);
    }
    /// <summary>
    /// 決定音
    /// </summary>
    public void DecisionSoundSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.DecisionSoundSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEDecisionSoundVolume);
    }
    /// <summary>
    /// 犬の鳴き声
    /// </summary>
    public void DogSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.DogSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEDogVolume);
    }
    /// <summary>
    /// ごみを引きづる音
    /// </summary>
    public void DraggingTrashSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.DraggingTrashSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEDraggingTrashVolume);
    }
    /// <summary>
    /// ごみを落とす音
    /// </summary>
    public void FallTrashSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.FallTrashSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEFallTrashVolume);
    }
    /// <summary>
    /// ゴールの音
    /// </summary>
    public void GoalSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.GoalSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEGoalVolume);
    }
    /// <summary>
    /// ジャンプの音
    /// </summary>
    public void JumpSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.JumpSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEJumpVolume);
    }
    /// <summary>
    /// 鳩時計の音
    /// </summary>
    public void OpenClockSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.OpenClockSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEOpenClockVolume);
    }
    /// <summary>
    /// ゴールのドアを開ける音
    /// </summary>
    public void OpenDoorSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.OpenDoorSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEOpenDoorVolume);
    }
    /// <summary>
    /// 鍵以外の獲得音
    /// </summary>
    public void OtherThanKeySE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.OtherThanKeySE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEOtherThanKeysVolume);
    }
    /// <summary>
    /// 本を置く音
    /// </summary>
    public void PutOnBookSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.PutOnBookSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEPutOnBookVolume);
    }
    /// <summary>
    /// ルンバの音
    /// </summary>
    public void RoombaSE()
    {
        if (!seAudioSource.isPlaying)
        {
            seAudioSource.PlayOneShot(soundList.GetSEData(SESource.RoombaSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SERoombaVolume);
        }
    }
    /// <summary>
    /// シンク水湧き上がる音
    /// </summary>
    public void SinkSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.SinkSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SESinkVolume);
    }
    /// <summary>
    /// シンク水湧き上がる皿の音
    /// </summary>
    public void SinkPlatesSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.SinkPlatesSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SESinkPlatesVolume);
    }
    /// <summary>
    /// タイトルスタート音
    /// </summary>
    public void StartSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.StartSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEStartVolume);
    }
    /// <summary>
    /// パンを焼いている音
    /// </summary>
    public void ToastBakingSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.ToastBakingSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEToastBakingVolume);
    }
    /// <summary>
    /// パンが飛び出る音
    /// </summary>
    public void ToasterPopOutSE()
    {
        seAudioSource.PlayOneShot(soundList.GetSEData(SESource.ToasterPopOutSE).SEAudioClip, soundVolumeData.SEVolume * soundVolumeData.SEToasterPopOutVolume);
    }
}