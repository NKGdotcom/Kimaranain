using UnityEngine;

public class TitlePage : MonoBehaviour
{
    [SerializeField] private TitleSelectText[] titleSelectText;
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private GameObject stageSelectPage;
    private int currentIndex = 0;
    private int titleSelectTextIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleSelectTextIndex = titleSelectText.Length - 1;
        titleSelectText[currentIndex].SelectTextButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            UpTextSelect();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DownTextSelect();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterText();
        }
    }
    private void EnterText()
    {
        titleSelectText[currentIndex].SelectText(titleAnimator, stageSelectPage, this.gameObject);
    }
    private void UpTextSelect()
    {
        if(currentIndex == 0)
        {
            titleSelectText[currentIndex].DeselectTextButton();
            currentIndex = titleSelectTextIndex;
            titleSelectText[currentIndex].SelectTextButton();
        }
        else
        {
            titleSelectText[currentIndex].DeselectTextButton();
            currentIndex--;
            titleSelectText[currentIndex].SelectTextButton();
        }
    }
    private void DownTextSelect()
    {
        if(currentIndex == titleSelectTextIndex)
        {
            titleSelectText[currentIndex].DeselectTextButton();
            currentIndex = 0;
            titleSelectText[currentIndex].SelectTextButton();
        }
        else
        {
            titleSelectText[currentIndex].DeselectTextButton();
            currentIndex++;
            titleSelectText[currentIndex].SelectTextButton();
        }
    }
}
