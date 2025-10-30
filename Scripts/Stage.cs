using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public enum StageType
    {
        Park,Road,Kitchen,Living,Room
    }
    [SerializeField] private StageClearCheck stageClearCheck;
    [SerializeField] private StageType stageType;
    [SerializeField] private Fade fade;
    [SerializeField] private Animator stageAnimator;
    [SerializeField] private Image crayonImage;

    private void Start()
    {
        switch (stageType)
        {
            case StageType.Park:
                if(stageClearCheck.IsParkStageCleared)
                {
                    stageAnimator.SetBool("Clear", true);
                }
                break;
            case StageType.Road:
                if (stageClearCheck.IsRoadStageCleared)
                {
                    stageAnimator.SetBool("Clear", true);
                }
                break;
            case StageType.Kitchen:
                if (stageClearCheck.IsKitchenStageCleared)
                {
                    stageAnimator.SetBool("Clear", true);
                }
                break;
            case StageType.Living:
                if (stageClearCheck.IsLivingStageCleared)
                {
                    stageAnimator.SetBool("Clear", true);
                }
                break;
            case StageType.Room:
                if (stageClearCheck.IsRoomStageCleared)
                {
                    stageAnimator.SetBool("Clear", true);
                }
                break;

        }
    }
    public void StageSelect()
    {
        stageAnimator.SetBool("Select", true);
        crayonImage.enabled = true;
    }

    public void StageDeselect()
    {
        stageAnimator.SetBool("Select", false);
        crayonImage.enabled = false;
    }
    public void StageTransition()
    {
        switch (stageType)
        {
            case StageType.Park:
                Debug.Log("Park Stage Selected");
                fade.FadeIn(3f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1"));
                break;
            case StageType.Road:
                fade.FadeIn(3f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("Stage2"));
                Debug.Log("Road Stage Selected");
                break;
            case StageType.Kitchen:
                fade.FadeIn(3f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("Stage3"));
                Debug.Log("Kitchen Stage Selected");
                break;
            case StageType.Living:
                fade.FadeIn(3f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("Stage4"));
                Debug.Log("Living Stage Selected");
                break;
            case StageType.Room:
                fade.FadeIn(3f,() => UnityEngine.SceneManagement.SceneManager.LoadScene("Stage5"));
                Debug.Log("Room Stage Selected");
                break;

        }
    }
}
