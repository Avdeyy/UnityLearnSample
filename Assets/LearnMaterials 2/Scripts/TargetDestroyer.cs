using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyer : AbstractUse
{
    [SerializeField]
    private Transform target;

    [SerializeField, Min(0.1f)]
    private float shrinkSpeed = 2f;

    public override void Use()
    {
        if (target != null)
        {
            StartCoroutine(ShrinkAndDestroyChildren());
        }
    }

    private System.Collections.IEnumerator ShrinkAndDestroyChildren()
    {
        int childCount = target.childCount;
        Transform[] children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = target.GetChild(i);
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * shrinkSpeed;
            foreach (var child in children)
            {
                if (child != null)
                {
                    child.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
                }
                else
                {
                    Debug.LogWarning("В качестве таргета выбран NULL");
                }
            }
            yield return null;
        }

        foreach (var child in children)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
