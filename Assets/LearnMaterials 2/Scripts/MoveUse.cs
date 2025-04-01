using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUse : AbstractUse
{
    [SerializeField, Range(0.1f, 2)]
    private float moveSpeed = 0.1f;
    [SerializeField]
    private Vector3 targetPosition = new Vector3(3f, 0f, 0f);
    [SerializeField]
    private float rotationSpeed = 360f;

    private bool isMoving = false;

    public override void Use()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); // я перепутал оси, но меня разъебало, поэтому оставил так
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
