using UnityEngine;

[CreateAssetMenu(fileName = "SoundVolume", menuName = "ScriptableObjects/Sound/SoundVolume")]
public class SoundVolumeData : ScriptableObject
{
    public float BGMVolume { get => bgmVolume; set => bgmVolume = value; }
    public float SEVolume { get => seVolume; set => seVolume = value; }

    public float SEFootVolume { get => seFootVolume;}
    public float SEDriveCarVolume { get => seDriveCarVolume; }
    public float SEFinishMoveTimeVolume { get => seFinishMoveTimeVolume;}
    public float SEGetKeyVolume { get => seGetKeyVolume;}
    public float SEOnTheWayMoveTimeVolume { get => seOnTheWayMoveTimeVolume;}
    public float SEStopCar { get => seStopCarVolume;}
    public float SEStopIdleCar { get => seStopIdleCarVolume;}
    public float SEButtonMove { get => seButtonMoveVolume; }
    public float SEAlarmClockVolume { get => seAlarmClockVolume; }
    public float SEBreakerTripsVolume { get => seBreakerTripsVolume; }
    public float SEBreathingVolume { get => seBreathingVolume; }
    public float SEChoiceVolume { get => seChoiceVolume; }
    public float SEDecisionSoundVolume { get => seDecisionSoundVolume; }
    public float SEDogVolume { get => seDogVolume; }
    public float SEDraggingTrashVolume { get => seDraggingTrashVolume; }
    public float SEFallTrashVolume { get => seFallTrashVolume; }
    public float SEGoalVolume { get => seGoalVolume; }
    public float SEJumpVolume { get => seJumpVolume; }
    public float SEOpenClockVolume { get => seOpenClockVolume; }
    public float SEOpenDoorVolume { get => seOpenDoorVolume; }
    public float SEOtherThanKeysVolume { get => seOtherThanKeysVolume; }
    public float SEPutOnBookVolume { get => sePutOnBookVolume; }
    public float SERoombaVolume { get => seRoombaVolume; }
    public float SESinkVolume { get => seSinkVolume; }
    public float SESinkPlatesVolume { get => seSinkPlatesVolume; }
    public float SEStartVolume { get => seStartVolume; }
    public float SEToastBakingVolume { get => seToastBakingVolume; }
    public float SEToasterPopOutVolume { get => seToasterPopOutVolume; }
    public float SESignalVolume { get => sesignalVolume; }

    [SerializeField][Range(0, 1)] private float bgmVolume = 1;
    [SerializeField][Range(0, 1)] private float seVolume = 1;
    [SerializeField][Range(0, 1)] private float seFootVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seDriveCarVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seFinishMoveTimeVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seGetKeyVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seOnTheWayMoveTimeVolume =0.2f;
    [SerializeField][Range(0, 1)] private float seStopCarVolume = 0.2f;
    [SerializeField][Range(0, 1)] private float seStopIdleCarVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seButtonMoveVolume = 0.1f;
    [SerializeField][Range(0, 1)] private float seAlarmClockVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seBreakerTripsVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seBreathingVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seChoiceVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seDecisionSoundVolume = 0.2f;
    [SerializeField][Range(0, 1)] private float seDogVolume = 0.2f;
    [SerializeField][Range(0, 1)] private float seDraggingTrashVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seFallTrashVolume = 0.1f;
    [SerializeField][Range(0, 1)] private float seGoalVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seJumpVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seOpenClockVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seOpenDoorVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seOtherThanKeysVolume = 0.2f;
    [SerializeField][Range(0, 1)] private float sePutOnBookVolume = 0.2f;
    [SerializeField][Range(0, 1)] private float seRoombaVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seSinkVolume = 0.1f;
    [SerializeField][Range(0, 1)] private float seSinkPlatesVolume = 0.05f;
    [SerializeField][Range(0, 1)] private float seStartVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seToastBakingVolume = 0.3f;
    [SerializeField][Range(0, 1)] private float seToasterPopOutVolume = 0.2f;
    [SerializeField][Range(0, 1)] private float sesignalVolume = 0.2f;
}
