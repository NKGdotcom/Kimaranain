using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : OnButtonMouse 
{
    [SerializeField] private GameObject optionPage; //Optionページを開く

    /// <summary>
    /// Optionをクリックしたら
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        optionPage.SetActive(true);
    }
}
