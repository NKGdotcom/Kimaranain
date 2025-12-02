using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : OnButtonMouse
{
    [SerializeField] private GameObject[] hidePages; //•Â‚¶‚éUI(•¡”ƒy[ƒW‚É‚í‚½‚éê‡‚à‚ ‚é‚½‚ß)
    public override void OnPointerClick(PointerEventData eventData)
    {
        foreach(var page in hidePages)
        {
            page.SetActive(false);
        }
    }
}
