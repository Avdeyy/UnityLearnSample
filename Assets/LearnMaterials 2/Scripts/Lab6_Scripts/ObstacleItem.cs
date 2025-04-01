using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [Range(0f, 1f)]
    public float currentValue = 1f;
    public UnityEvent onDestroyObstacle;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    public void GetDamage(float value)
    {
        currentValue = Mathf.Clamp01(currentValue - value);
        UpdateColor();

        if (currentValue <= 0)
        {
            onDestroyObstacle?.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        if (objectRenderer != null)
        {
            Color newColor = Color.Lerp(Color.red, Color.white, currentValue);
            objectRenderer.material.color = newColor;
        }
    }
}
