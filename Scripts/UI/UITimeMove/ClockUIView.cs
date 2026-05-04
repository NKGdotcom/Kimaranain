using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

/// <summary>
/// 時計UIの見た目の動きを行う
/// </summary>
public class ClockUIView : MonoBehaviour, IDragHandler
{
    [Header("コンポーネント参照")]
    [Tooltip("時計UIを親として置いてあるキャンバス")]
    [SerializeField] private Canvas canvas;
    [Tooltip("実際に動かす時計の針")]
    [SerializeField] private RectTransform longHandRect;
    [Tooltip("左の歯車UI")]
    [SerializeField] private RectTransform lGearRect;
    [Tooltip("右の歯車UI")]
    [SerializeField] private RectTransform rGearRect;
    [Tooltip("カメラのポストプロセスの処理")]
    [SerializeField] private PostProcessVolume postProcessVolume;
    [Tooltip("時間軸を教える元となる画像")]
    [SerializeField] private Image changeGearUI;
    [Tooltip("通常の時間軸を示す画像")]
    [SerializeField] private Sprite normalTimelineImage;
    [Tooltip("過去の時間軸を示す画像")]
    [SerializeField] private Sprite pastTimelineImage;
    [Tooltip("未来の時間軸を示す画像")]
    [SerializeField] private Sprite futureTimelineImage;

    [Header("パラメータ")]
    [Tooltip("intensityを最大まで上げるのにかかる時間")]
    [SerializeField] private float durationUp = 0.2f;
    [Tooltip("intensityが0まで下がるのにかかる時間")]
    [SerializeField] private float durationDown = 0.8f;
    [Tooltip("intensityをマックスまで上げる値")]
    [SerializeField] private float maxIntensity = 40f;

    //コンポーネント参照
    //ポストプロセスのBloomを調整する
    private Bloom bloom;

    //パラメータ
    //回転をさせる対象のコンポーネント参照
    private Transform handTransform;
    //1フレーム前の角度
    private float lastAngle;
    //回転した料の累計を保存するパラメータ
    private float totalRotation = 0f;
    private const float THRESHOLD = 15.0f;
    //パラメータ
    //現在の回転位置
    public float CurrentRotation => longHandRect.localEulerAngles.z;

    //イベント
    //ドラッグをしたときに検知する
    public event Action<Vector2, Vector2> OnDragEvent;

    private void Awake()
    {
        if (postProcessVolume == null) { Debug.LogError("postProsessVolumeが参照されていません"); return; }
        if (changeGearUI == null) { Debug.LogError("changeGearUIが参照されていません"); return; }
        if (normalTimelineImage == null) { Debug.LogError("normalTimelineImageが参照されていません"); return; }
        if (pastTimelineImage == null) { Debug.LogError("pastTimelineImageが参照されていません"); return; }
        if (futureTimelineImage == null) { Debug.LogError("futureTimelineImageが参照されていません"); return; }

        handTransform = GetComponent<RectTransform>();
        lastAngle = handTransform.eulerAngles.z;

        changeGearUI.sprite = normalTimelineImage;
        postProcessVolume.profile.TryGetSettings(out bloom);
    }
    /// <summary>
    /// ドラッグの位置を設定
    /// </summary>
    /// <param name="_eventData"></param>
    public void OnDrag(PointerEventData _eventData)
    {
        Vector2 _localCenterPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            longHandRect.parent as RectTransform,
            _eventData.position,
            canvas.worldCamera,
            out _localCenterPoint);

        OnDragEvent?.Invoke(longHandRect.anchoredPosition, _localCenterPoint);
    }

    /// <summary>
    /// 回転位置を更新
    /// </summary>
    /// <param name="_angle"></param>
    public void UpdateRotation(float _angle)
    {
        Vector3 _rot = Vector3.zero;

        _rot.z = _angle;
        longHandRect.localEulerAngles = _rot;
        lGearRect.localEulerAngles = _rot;

        _rot.z = -_angle;
        rGearRect.localEulerAngles = _rot;
    }

    /// <summary>
    /// ポストプロセスで、時間軸を切り替えたらゆっくり白飛びをして元に戻す
    /// </summary>
    /// <returns></returns>
    public IEnumerator BloomFlashEffect()
    {
        SoundManager.Instance.PlayFinishMoveTime();

        float _time = 0f;

        //durationUpに到達するまで線形で加算
        while (_time < durationUp)
        {
            _time += Time.deltaTime;
            float _t = _time / durationUp;
            bloom.intensity.value = Mathf.Lerp(0f, maxIntensity, _t);
            yield return null;
        }

        _time = 0f;

        //durationDownに到達するまで線形で減算
        while (_time < durationDown)
        {
            _time += Time.deltaTime;
            float t = _time / durationDown;
            bloom.intensity.value = Mathf.Lerp(maxIntensity, 0f, t);
            yield return null;
        }

        bloom.intensity.value = 0f;
    }

    /// <summary>
    /// 未来の時間軸の画像変換
    /// </summary>
    public void ToFutureImage()
    {
        changeGearUI.sprite = futureTimelineImage;
    }

    /// <summary>
    /// 現在の時間軸の画像変換
    /// </summary>
    public void ToNormalImage()
    {
        changeGearUI.sprite = normalTimelineImage;
    }

    /// <summary>
    /// 過去の時間軸の画像変換
    /// </summary>
    public void ToPastImage()
    {
        changeGearUI.sprite= pastTimelineImage;
    }
}
