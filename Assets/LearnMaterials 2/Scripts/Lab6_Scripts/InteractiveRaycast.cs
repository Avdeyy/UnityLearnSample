using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab;
    private InteractiveBox selectedBox;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("InteractivePlane"))
                {
                    if (prefab != null)
                    {
                        Vector3 spawnPosition = hit.point + hit.normal * 1f;
                        GameObject newBox = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        newBox.transform.up = hit.normal;
                    }
                }
                else if (hit.collider.TryGetComponent(out InteractiveBox box))
                {
                    if (selectedBox == null)
                    {
                        selectedBox = box;
                    }
                    else if (selectedBox != box)
                    {
                        selectedBox.AddNext(box);
                        selectedBox = null;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out InteractiveBox box))
                {
                    Destroy(box.gameObject);
                }
            }
        }
    }
}
