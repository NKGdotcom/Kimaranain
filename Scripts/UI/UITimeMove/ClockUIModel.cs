using UnityEngine;

/// <summary>
/// 時計を回す計算処理を統括したクラス
/// </summary>
public class ClockUIModel
{
    //パラメータ
    //1フレーム前の回転数
    private float lastTickAngle = 0f;
    //どの位回転したら音を鳴らすか
    private float angleSoundStep = 30f;
    //音を鳴らす間隔
    private float soundInterval = 15f;
    //円一周の角度
    private const float DEGREES_IN_CIRCLE = 360f;
    //累積の回転角度
    private float totalAngle = 0f;
    //時計回りに2回転
    private float limitRightAngle = -360f;
    //反時計回りに2回転
    private float limitLeftAngle = 360f;
    //1フレーム前にマウスが示していた角度
    private float lastRawPointerAngle = -1000f;

    /// <summary>
    /// 新しい回転角度を計算し、制限内に収める（クランプする）
    /// </summary>
    public float CalculateNewAngle(float _targetPointerAngle, bool _isPast, bool _isFuture)
    {
        // 初回のドラッグ開始時など、値が未設定の場合は初期化だけして動かさない
        if (lastRawPointerAngle < -500f)
        {
            lastRawPointerAngle = _targetPointerAngle;
            return totalAngle;
        }

        // 限界値で止まる totalAngle ではなく、指自体の移動量を計算する
        float _delta = Mathf.DeltaAngle(lastRawPointerAngle, _targetPointerAngle);

        // 指を離して画面の反対側をタッチした時などに、UIがワープして大回転するのを防ぐ
        if (Mathf.Abs(_delta) > 90f)
        {
            lastRawPointerAngle = _targetPointerAngle;
            return totalAngle;
        }

        // 次のフレームの計算のために、現在の指の位置を保存
        lastRawPointerAngle = _targetPointerAngle;

        // 純粋な指の移動量だけを累積角度に足す
        totalAngle += _delta;

        // 指定した回転数の範囲内にクランプ(固定)する
        totalAngle = Mathf.Clamp(totalAngle, limitRightAngle, limitLeftAngle);

        return totalAngle;
    }

    /// <summary>
    /// 回転数に応じて音を鳴らすべきかの判定
    /// </summary>
    /// <param name="_targetAngle"></param>
    /// <returns></returns>
    public bool ShouldPlaySound(float _targetAngle)
    {
        float _diff = Mathf.Abs(_targetAngle - lastTickAngle);
        if (_diff >= soundInterval || _diff >= (DEGREES_IN_CIRCLE - soundInterval))
        {
            lastTickAngle = Mathf.Round(_targetAngle / angleSoundStep) * angleSoundStep;
            return true;
        }
        return false;
    }
}
