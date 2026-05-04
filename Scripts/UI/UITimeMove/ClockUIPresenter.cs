using UnityEngine;
using UnityEngine.UIElements;

public class ClockUIPresenter: MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("時計の回転をUIで表示させる")]
    [SerializeField] private ClockUIView view;

    //コンポーネント参照
    //UIの計算などを行うクラス
    private ClockUIModel model;

    //パラメータ
    //円一周の角度
    private const float DEGREES_IN_CIRCLE = 360f;
    //Spriteがずれているため直す(90は12時の方向)
    private const float SPRITE_ORIENTATION_OFFSET = 90f; 

    private void Awake()
    {
        if(view == null) { Debug.LogError("viewが参照されていません"); return; }
        model = new ClockUIModel();

        view.OnDragEvent += OnDragInput;
    }

    private void OnDestroy()
    {
        view.OnDragEvent -= OnDragInput;
    }

    /// <summary>
    /// ドラッグ入力を行い、可能であれば回転をさせる
    /// </summary>
    /// <param name="_centerPosition"></param>
    /// <param name="_pointerPosition"></param>
    public void OnDragInput(Vector2 _centerPosition, Vector2 _pointerPosition)
    {
        //角度計算
        Vector2 _direction = _pointerPosition - _centerPosition;
        float _rawAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        float _targetAngle = (_rawAngle - SPRITE_ORIENTATION_OFFSET + DEGREES_IN_CIRCLE) % DEGREES_IN_CIRCLE;

        //現在の状態を取得
        float _currentAngle = view.CurrentRotation;
        bool _isPast = TimeStateManager.Instance.IsPastState();
        bool _isFuture = TimeStateManager.Instance.IsFutureState();

        //Modelに累積回転量を計算
        float _newTotalAngle = model.CalculateNewAngle(_targetAngle, _isPast, _isFuture);

        //回転をUIに反映
        view.UpdateRotation(_newTotalAngle);

        // 音の判定
        if (model.ShouldPlaySound(_newTotalAngle))
        {
            SoundManager.Instance.PlayMoveTimeSE();
        }

        //回転量が360に近づいた時
        if (_newTotalAngle >= 355f && !TimeStateManager.Instance.IsPastState())
        {
            //未来の時間軸に
            StartCoroutine(view.BloomFlashEffect());
            TimeStateManager.Instance.ToPastTimeline();
            view.ToPastImage();
        }

        //回転量が-360に近づいた時
        else if (_newTotalAngle <= -355f && !TimeStateManager.Instance.IsFutureState())
        {
            //未来の時間軸に
            StartCoroutine(view.BloomFlashEffect());
            TimeStateManager.Instance.ToFutureTimeline();
            view.ToFutureImage();
        }

        //回転量が0に近づいた時
        else if (Mathf.Abs(_newTotalAngle) <= 5f && !TimeStateManager.Instance.IsNormalState())
        {
            //現在の時間軸に
            StartCoroutine(view.BloomFlashEffect());
            TimeStateManager.Instance.ToNormalTimeline();
            view.ToNormalImage();
        }
    }
}
