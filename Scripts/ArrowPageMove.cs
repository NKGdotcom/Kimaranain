using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowPageMove : OnButtonMouse
{
    [SerializeField] private GameObject hideObj; //”ñ•\Ž¦‚É‚·‚éObj
    [SerializeField] private GameObject displayObj; //•\Ž¦‚·‚éObj

    public override void OnPointerClick(PointerEventData eventData)
    {
        hideObj.SetActive(false);
        displayObj.SetActive(true);

    }
}
