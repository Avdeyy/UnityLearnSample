using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCopier : AbstractUse
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField, Min(1)]
    private int spawnCount = 3;

    [SerializeField]
    private float spawnStep = 1f;

    public override void Use()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(spawnStep * i, 0f, 0f);
            Instantiate(prefab, spawnPosition, transform.rotation);
        }
    }
}
