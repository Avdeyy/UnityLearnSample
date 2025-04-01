using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Rotate : AbstractUse
{
    [SerializeField]
    private Vector3 targetRotation = new Vector3(0f, 90f, 0f);

    [SerializeField, Min(0.1f)]
    private float rotationSpeed = 10f;

    private bool isRotating = false;
    public override void Use()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateToTarget());
        }
    }

    private System.Collections.IEnumerator RotateToTarget()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion target = Quaternion.Euler(targetRotation) * startRotation;
        float maxAngle = Quaternion.Angle(startRotation, target);
        float duration = maxAngle / rotationSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, target, t);
            yield return null;
        }

        transform.rotation = target;
        isRotating = false;
    }
}
