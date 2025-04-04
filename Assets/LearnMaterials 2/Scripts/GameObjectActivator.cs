﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Задаёт указанным объектам значение activeSalfe, равное state
/// </summary>
[HelpURL("https://docs.google.com/document/d/1GP4_m0MzOF8L5t5pZxLChu3V_TFIq1czi1oJQ2X5kpU/edit?usp=sharing")]
public class GameObjectActivator : AbstractUse
{
    [SerializeField]
    private List<StateContainer> targets;
    [SerializeField]
    private bool debug;

    [SerializeField]
    private List<MonoBehaviour> modulesToActivate;

    private void Awake()
    {
        foreach (var item in targets)
        {
            item.defaultValue = item.targetGO.activeSelf;
        }
    }

    public override void Use()
    {
        ActivateModule();
    }

    [ContextMenu("НУ ДАВАЙ ДАВАЙ НАПАДАЙ")]
    public void ActivateModule()
    {
        SetStateForAll();

        foreach (var module in modulesToActivate)
        {
            if (module != null)
            {
                if (module is DestroyModule destroyModule)
                {
                    destroyModule.ActivateModule();
                }
                else if (module is ScalerModule scalerModule)
                {
                    scalerModule.ActivateModule();
                }
                else if (module is TransparentModule transparentModule)
                {
                    transparentModule.ActivateModule();
                }
            }
            else
            {
                Debug.LogWarning("Один из модулей в списке modulesToActivate равен null", this);
            }
        }
    }
    public void ReturnToDefaultState()
    {
        foreach (var item in targets)
        {
            item.targetState = item.defaultValue;
            item.targetGO.SetActive(item.defaultValue);
        }
    }

    private void SetStateForAll()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                targets[i].targetGO.SetActive(targets[i].targetState);
                targets[i].targetState = !targets[i].targetState;
            }
            else
            {
                Debug.LogError("Элемент " + i + " равен null. Вероятно, была утеряна ссылка. Источник :" + gameObject.name);
            }
        }
    }

    #region Материал ещё не изучен
    private void OnDrawGizmos()
    {
        if(debug)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, 0.3f);

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null && targets[i].targetGO != null)
                {
                    if (targets[i].targetState)
                    {
                        Gizmos.color = Color.green;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawLine(transform.position, targets[i].targetGO.transform.position);
                }
                else
                {
                    Debug.LogError("Элемент " + i + " равен null. Вероятно, была утеряна ссылка. Источник :" + gameObject.name);
                }
            }
        }
    }
    #endregion
}

#region Материал ещё не изучен
[System.Serializable]
public class StateContainer
{
    [Tooltip("Объект, которому нужно задать состояние")] public GameObject targetGO;
    [Tooltip("Целевое состояние. Если отмечено, объект будет включен")] public bool targetState = false;
    [HideInInspector] public bool defaultValue;
}
#endregion
