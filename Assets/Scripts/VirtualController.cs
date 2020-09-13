using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image background;
    public Image analog;
    Vector3 inputVector;

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / background.rectTransform.sizeDelta.x);
            pos.y = (pos.y / background.rectTransform.sizeDelta.y);
            inputVector = new Vector3(pos.x * 2f, 0f, pos.y * 2f);
            if (inputVector.magnitude > 1.0f)
            {
                inputVector = inputVector.normalized;
            }
            //inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            analog.rectTransform.anchoredPosition = new Vector3(inputVector.x * (background.rectTransform.sizeDelta.x / 3), inputVector.z * (background.rectTransform.sizeDelta.y / 3));
            //Debug.Log(inputVector);
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        analog.rectTransform.anchoredPosition = inputVector;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
        {
            return inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
        {
            return inputVector.z;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
