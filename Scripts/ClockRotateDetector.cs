using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ClockRotateDetector : MonoBehaviour
{
    private Transform handTransform;

    private float lastAngle;
    private float totalRotation = 0f;

    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private Image changeGearUI;
    [SerializeField] private Sprite budImage; //現在
    [SerializeField] private Sprite seedImage; //過去
    [SerializeField] private Sprite treeImage; //未来
    private Bloom bloom;

    void Start()
    {
        handTransform = GetComponent<RectTransform>();
        lastAngle = handTransform.eulerAngles.z;
        changeGearUI.sprite = budImage;
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out bloom);
        }
    }

    void Update()
    {
        float _currentAngle = handTransform.eulerAngles.z;
        float _delta = Mathf.DeltaAngle(lastAngle, _currentAngle); // -180～180 の範囲で差分を取得

        totalRotation += _delta;

        if (totalRotation >= 360f)
        {
            Debug.Log("反時計回りに1回転！");
            TimeBackChange();
            totalRotation = 0f; // リセット
        }

        // 反時計回りに1回転（-360度）
        if (totalRotation <= -360f)
        {
            Debug.Log("時計回りに1回転！");
            TimeForwardChange();
            totalRotation = 0f; // リセット
        }

        lastAngle = _currentAngle;
    }
    private void TimeForwardChange()
    {
        if (bloom != null && !TimeStateManager.Instance.IsFastForwardState())
            StartCoroutine(BloomFlashEffect());
        if (TimeStateManager.Instance.IsNormalState())
        {
            changeGearUI.sprite = treeImage;
            TimeStateManager.Instance.SetState(TimeStateManager.TimeState.FastForward);
        }
        else if (TimeStateManager.Instance.IsRewindingState())
        {
            changeGearUI.sprite = budImage;
            TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Normal);
        }
    }
    private void TimeBackChange()
    {
        if (bloom != null && !TimeStateManager.Instance.IsRewindingState())
            StartCoroutine(BloomFlashEffect());

        if (TimeStateManager.Instance.IsNormalState())
        {
            changeGearUI.sprite = seedImage;
            TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Rewinding);
        }
        else if (TimeStateManager.Instance.IsFastForwardState())
        {
            changeGearUI.sprite = budImage;
            TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Normal);
        }
    }

    private IEnumerator BloomFlashEffect()
    {
        float durationUp = 0.2f; // 上昇時間
        float durationDown = 1f; // 下降時間
        float maxIntensity = 40f;

        float time = 0f;

        while (time < durationUp)
        {
            time += Time.deltaTime;
            float t = time / durationUp;
            bloom.intensity.value = Mathf.Lerp(0f, maxIntensity, t);
            yield return null;
        }

        // 数秒維持したいならここに待機
        //yield return new WaitForSeconds(0.5f);

        time = 0f;
        while (time < durationDown)
        {
            time += Time.deltaTime;
            float t = time / durationDown;
            bloom.intensity.value = Mathf.Lerp(maxIntensity, 0f, t);
            yield return null;
        }

        bloom.intensity.value = 0f;
    }
}