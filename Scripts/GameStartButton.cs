using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStartButton : OnButtonMouse
{
    [SerializeField] private CheckTitleInformation checkTitleInformation; //アニメーションを再生したかどうか
    [SerializeField] private Animator stageSelectAnimator; //ステージ選択画面に映るアニメーション
    [SerializeField] private GameObject stageSelectObj; //ステージ選択画面用のアニメーションのために表示
    [SerializeField] private string openingSceneName = "Opening";

    private int animParamID;
    private const string ANIM_STATE_NAME = "ToStageSelect";

    private float maxAnimPer = 1;
    private void Start()
    {
        animParamID = Animator.StringToHash("TitleToStageSelect");
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (checkTitleInformation.IsPlayedStoryAnimation) //オープニングを見ている場合はアニメーション再生
        {
            StartCoroutine(PlayAndWaitForAnimation(stageSelectAnimator, stageSelectObj));
        }
        else //初回のみオープニングを見せる
        {
            checkTitleInformation.IsSelectStageSelect = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(openingSceneName);
        }
    }

    /// <summary>
    /// ステージ選択へ移るアニメーション
    /// </summary>
    /// <param name="_titleAnimator"></param>
    /// <param name="_stageSelectObj"></param>
    /// <returns></returns>
    private IEnumerator PlayAndWaitForAnimation(Animator _titleAnimator, GameObject _stageSelectObj)
    {
        _titleAnimator.SetBool(animParamID, true);
        checkTitleInformation.IsSelectStageSelect = false;
        yield return new WaitUntil(() => _titleAnimator.GetCurrentAnimatorStateInfo(0).IsName(ANIM_STATE_NAME));

        // 再生終了待ち
        yield return new WaitUntil(() =>
            _titleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= maxAnimPer && //アニメーションが終わるまで待ち
            !_titleAnimator.IsInTransition(0));

        _stageSelectObj.SetActive(true);
        yield break;
    }
}
