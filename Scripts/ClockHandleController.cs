using UnityEngine;
using UnityEngine.EventSystems;

public class ClockHandleController : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform longHandRect;
    [SerializeField] private RectTransform lGearRect;
    [SerializeField] private RectTransform rGearRect;
    [SerializeField] private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            longHandRect.parent as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint
        );

        Vector2 direction = localPoint - longHandRect.anchoredPosition;
        float _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float zAngle = _angle - 90f;

        Vector3 rot = longHandRect.localEulerAngles;
        rot.z = zAngle;
        longHandRect.localEulerAngles = rot;

        rot = lGearRect.localEulerAngles;
        rot.z = zAngle;
        lGearRect.localEulerAngles = rot;

        rot = rGearRect.localEulerAngles;
        rot.z = -zAngle;
        rGearRect.localEulerAngles = rot;
    }
}