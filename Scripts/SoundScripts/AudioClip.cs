using UnityEngine;
using System;

[Serializable]
public enum BGMSource
{
    RoomBGM,OutsideBGM
}
[Serializable]
public enum SESource
{
    GetKeySE, FinishMoveTimeSE, OnTheWayMoveTimeSE, DriveCarSE, StopCarSE, StopIdleCarSE, FootSE, ButtonMove,
    AlarmClockSE, BreakerTripsSE, BreathingSE, ChoiceSE, DecisionSoundSE, DogSE, DraggingTrashSE, FallTrashSE,
    GoalSE, JumpSE, OpenClockSE, OpenDoorSE, OtherThanKeySE, PutOnBookSE, RoombaSE, SinkSE, SinkPlatesSE, StartSE,
    ToastBakingSE, ToasterPopOutSE,SignalSE
}