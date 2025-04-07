using System.Collections;
using UnityEngine;

public class MinableCliff : MonoBehaviour
{
    [SerializeField] private GameObject spawnedBlockPrefab;
    [SerializeField] private GameObject spawnedPoint;
    [SerializeField] private float minSpeed;

    private bool canBePunched = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out XRControllerSpeed controller))
        {
            HandAnimator handAnimator = other.GetComponentInChildren<HandAnimator>();
            if (!handAnimator || !canBePunched) return;
            if (controller.GetControllerSpeed().magnitude > minSpeed && handAnimator.ClosedFist())
            {
                handAnimator.Rumble(1, 0.2f);
                GetPunched();
                StartCoroutine(PunchCoolDown());
            }
        }
    }

    private void GetPunched()
    {
        spawnedBlockPrefab = Instantiate(spawnedBlockPrefab, spawnedPoint.transform.position, Quaternion.identity);
    }

    private IEnumerator PunchCoolDown()
    {
        canBePunched = false;
        yield return new WaitForSeconds(0.4f);
        canBePunched = true;
    }
}
