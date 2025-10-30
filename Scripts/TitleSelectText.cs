using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class TitleSelectText : MonoBehaviour
{
    public enum SelectTextState
    {
        StageSlect,Option,Exit
    }
    [SerializeField] private SelectTextState selectTextState;
    [SerializeField] private Text textButton;
    [SerializeField] private Image crayonImage;
    private Color selectColor = Color.yellow;
    private Color defaultColor = Color.black;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SelectTextButton()
    {
        crayonImage.enabled = true;
        textButton.color = selectColor;
    }
    public void DeselectTextButton()
    {
        crayonImage.enabled = false;
        textButton.color = defaultColor;
    }
    public void SelectText(Animator _titleAnimator,GameObject _stageSelectObj,GameObject _titleObj)
    {
        switch(selectTextState)
        {
            case SelectTextState.StageSlect:
                StartCoroutine(PlayAndWaitForAnimation(_titleAnimator, _stageSelectObj, _titleObj));
                break;
            case SelectTextState.Option:
                Debug.Log("オプションへ");
                break;
            case SelectTextState.Exit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
                break;
        }
    }
    private IEnumerator PlayAndWaitForAnimation(Animator _titleAnimator, GameObject _stageSelectObj, GameObject _titleObj)
    {
        _titleAnimator.SetBool("TitleToStageSelect", true);

        yield return new WaitUntil(() => _titleAnimator.GetCurrentAnimatorStateInfo(0).IsName("ToStageSelect"));

        // 再生終了待ち
        yield return new WaitUntil(() =>
            _titleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            !_titleAnimator.IsInTransition(0));

        _stageSelectObj.SetActive(true);
        _titleObj.SetActive(false);
        yield break;
    }
}
