using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectButton : OnButtonMouse
{
    [SerializeField] private StageClearCheck stageClearCheck;
    [SerializeField] private StageClearCheck.StageType stageType;
    [SerializeField] private Animator stageAnimator;
    [SerializeField] private string stageName;
    [SerializeField] private Fade fade;

    private float fadeDuration = 3f;
    private string stageClear = "Clear";
    private string stageSelect = "Select";
    private void Start()
    {
        if (stageClearCheck.GetClearStatus(stageType))
        {
            stageAnimator.SetBool(stageClear, true);
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        stageAnimator.SetBool(stageSelect, true);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        stageAnimator.SetBool(stageSelect, false);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SoundManager.Instance.DecisionSoundSE();
        fade.FadeIn(fadeDuration, () => UnityEngine.SceneManagement.SceneManager.LoadScene(stageName));
    }
}
