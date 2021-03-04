using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    //fields
    [SerializeField] private RectTransform joystick;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float maxHandleDistance;


    public void SetJoystickPosition(BaseEventData eventData)
    {
        joystick.gameObject.SetActive(true);
        joystick.position = eventData.currentInputModule.input.mousePosition;
        handle.localPosition = Vector3.zero;
    }

    public void DeactivateJoystick()
    {
        joystick.gameObject.SetActive(false);
    }

    public void SetHandlePosition(BaseEventData eventData)
    {
        var pos = eventData.currentInputModule.input.mousePosition;
        handle.position = pos;
        var localPos = handle.localPosition;
        if (localPos.magnitude > maxHandleDistance)
        {
            localPos = localPos.normalized * maxHandleDistance;
            handle.localPosition = localPos;
        }
    }
}