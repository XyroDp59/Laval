using UnityEngine;

public class DestroyableBlock : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject constructionBlock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out XRControllerSpeed controller))
        {
            HandAnimator handAnimator = other.GetComponentInChildren<HandAnimator>();
            if (!handAnimator) return;
            if (controller.GetControllerSpeed().magnitude > minSpeed && handAnimator.ClosedFist())
            {
                handAnimator.Rumble(1, 0.2f);
                GetPunched();
            }
        }
    }
    
    private void GetPunched()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(constructionBlock, spawnPoint.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
