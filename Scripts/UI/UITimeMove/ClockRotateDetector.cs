using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
/// <summary>
/// 時計UIを回す処理を統括したクラス
/// </summary>
public class ClockRotateDetector : MonoBehaviour
{
    [Header("コンポーネント参照")]
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

    void Update()
    {
        //現在のZ回転軸
        float _currentAngle = handTransform.eulerAngles.z;
        
        // -180～180 の範囲で差分を取得し加算
        float _delta = Mathf.DeltaAngle(lastAngle, _currentAngle);
        totalRotation += _delta;

        // 時計回りに1回転（+360度）
        if (totalRotation >= 360f)
        {
            Debug.Log("反時計回りに1回転！");
            //一つ過去の時間軸に戻る
            TimeBackChange();
            totalRotation = 0f;
        }

        // 反時計回りに1回転（-360度）
        if (totalRotation <= -360f)
        {
            Debug.Log("時計回りに1回転！");
            //一つ先の時間軸に進む
            TimeForwardChange();
            totalRotation = 0f;
        }

        lastAngle = _currentAngle;
    }

    /// <summary>
    /// 一つ先の時間軸を進める
    /// </summary>
    private void TimeForwardChange()
    {
        //未来の時間軸なら処理を走らせない
        if (TimeStateManager.Instance.IsFutureState()) return;
        //ポストプロセスを用いて、Bloomを変更する
        StartCoroutine(BloomFlashEffect());

        //通常なら未来の時間軸に
        if (TimeStateManager.Instance.IsNormalState())
        {
            changeGearUI.sprite = futureTimelineImage;
            TimeStateManager.Instance.ToFutureTimeline();
        }

        //過去なら通常の時間軸に
        else if (TimeStateManager.Instance.IsPastState())
        {
            changeGearUI.sprite = normalTimelineImage;
            TimeStateManager.Instance.ToNormalTimeline();
        }
    }

    /// <summary>
    /// ひとつ前の時間軸に戻る
    /// </summary>
    private void TimeBackChange()
    {
        //過去の時間軸なら処理を走らせない
        if (TimeStateManager.Instance.IsPastState()) return;
        //ポストプロセスを用いて、Bloomを変更する
        StartCoroutine(BloomFlashEffect());

        //通常なら過去の時間軸に
        if (TimeStateManager.Instance.IsNormalState())
        {
            changeGearUI.sprite = pastTimelineImage;
            TimeStateManager.Instance.ToPastTimeline();
        }

        //未来なら通常の時間軸に
        else if (TimeStateManager.Instance.IsFutureState())
        {
            changeGearUI.sprite = normalTimelineImage;
            TimeStateManager.Instance.ToNormalTimeline();
        }
    }

    /// <summary>
    /// ポストプロセスで、時間軸を切り替えたらゆっくり白飛びをして元に戻す
    /// </summary>
    /// <returns></returns>
    private IEnumerator BloomFlashEffect()
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
}