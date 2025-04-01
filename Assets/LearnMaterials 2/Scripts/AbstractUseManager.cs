using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractUseManager : MonoBehaviour
{
    [SerializeField]
    private List<AbstractUse> abstractUses = new List<AbstractUse>();

    [ContextMenu("ÄÀÂÀÉ ÄÀÂÀÉ ÀÊÒÈÂÈĞÓÉ")]
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
                Debug.LogWarning("Â ñïèñêå åñòü NULL");
            }
        }
    }
}
