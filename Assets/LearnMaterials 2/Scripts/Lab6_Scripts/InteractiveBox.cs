using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.magenta;
        lineRenderer.endColor = Color.magenta;
    }

    public void AddNext(InteractiveBox box)
    {
        next = box;
    }

    private void Update()
    {
        if (next != null)
        {
            Vector3 direction = next.transform.position - transform.position;
            Debug.DrawRay(transform.position, direction, Color.magenta);

            // Обновляем LineRenderer
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, next.transform.position);

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude))
            {
                if (hit.collider.TryGetComponent(out ObstacleItem obstacle))
                {
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
}
