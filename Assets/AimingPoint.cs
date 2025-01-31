using UnityEngine;

public class AimingPoint : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private float smoothSpeed = 10f;

    void LateUpdate()
    {
        Vector3 targetPosition = cameraPos.position + cameraPos.forward * maxDistance;

        if (Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hit, maxDistance))
        {
            targetPosition = hit.point;
        }
        
        target.position = Vector3.Lerp(target.position, targetPosition, Time.deltaTime * smoothSpeed);
        //Debug.Log(hit.collider.name);
    }
}