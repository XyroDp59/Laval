using UnityEngine;

public class MoonParent : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshRenderer meshRendererVisuals;
    private Vector3 originalRotation;

    private void Start()
    {
        originalRotation = transform.eulerAngles;
        meshRendererVisuals.enabled = false;
        meshRenderer.enabled = true;
    }
    
    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(originalRotation.x, originalRotation.y, transform.localEulerAngles.z);
    }

    public void OnSelect()
    {
        meshRenderer.enabled = false;
        meshRendererVisuals.enabled = true;
    }

    public void OnDeselect()
    {
        meshRenderer.enabled = true;
        meshRendererVisuals.enabled = false;
    }
}
