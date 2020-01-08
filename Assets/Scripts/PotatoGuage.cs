using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoGuage : MonoBehaviour
{
    [SerializeField]
    private float zoneRotationDelta;
    [SerializeField]
    private RectTransform pointerPivotRectTransform;

    [SerializeField]
    private float guageMoveTime;

    private float currentZRotation;
    private float targetZRotation;

    private Coroutine changePointerCoroutine = null;

    public void SetPotatoGauge(int potatoCount, bool snap = false)
    {
        if(changePointerCoroutine != null)
        {
            StopCoroutine(changePointerCoroutine);
        }

        float targetRotation = 0f;

        if(potatoCount >= 3)
        {
            targetRotation = -zoneRotationDelta;

        }
        else if(potatoCount == 2)
        {
            targetRotation = 0f;
        }
        else if(potatoCount <= 1)
        {
            targetRotation = zoneRotationDelta;
        }

        if(snap)
        {
            currentZRotation = targetRotation;
            SetZRotation(targetRotation);
        }
        else
        {
            changePointerCoroutine = StartCoroutine(ChangePointerRotationRoutine(targetRotation));
        }
    }

    private void SetZRotation(float zRot)
    {
        Vector3 rotation = pointerPivotRectTransform.localRotation.eulerAngles;
        rotation.z = zRot;

        pointerPivotRectTransform.localRotation = Quaternion.Euler(rotation);
    }

    private IEnumerator ChangePointerRotationRoutine(float targetZRotation)
    {
        float t = 0;

        float startZRotation = currentZRotation;

        while(t < guageMoveTime)
        {
            currentZRotation = Mathf.Lerp(startZRotation, targetZRotation, t / guageMoveTime);
            SetZRotation(currentZRotation);

            yield return null;

            t += Time.deltaTime;
        }
        
        currentZRotation = targetZRotation;
        SetZRotation(currentZRotation);

        changePointerCoroutine = null;
    }
}
