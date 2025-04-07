using UnityEngine;

public class MoonParent : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private MeshRenderer meshRenderer;
    private Vector3 originalRotation;

    private void Start()
    {
        originalRotation = transform.eulerAngles;
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(originalRotation.x, originalRotation.y, transform.localEulerAngles.z);
    }

    private void OnSelect()
    {
        meshRenderer.enabled = false;
    }

    private void OnDeselect()
    {
        meshRenderer.enabled = true;
    }
}
