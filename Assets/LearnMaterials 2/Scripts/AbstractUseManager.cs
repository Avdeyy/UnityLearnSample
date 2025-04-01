using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractUseManager : MonoBehaviour
{
    [SerializeField]
    private List<AbstractUse> abstractUses = new List<AbstractUse>();

    [ContextMenu("����� ����� ���������")]
    public void ActivateAll()
    {
        foreach (var use in abstractUses)
        {
            if (use != null)
            {
                use.Use();
            }
            else
            {
                Debug.LogWarning("� ������ ���� NULL");
            }
        }
    }
}
