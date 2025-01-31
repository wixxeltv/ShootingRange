using UnityEngine;

public class SmoothFollowTarget : MonoBehaviour
{
    public Transform player; // Le joueur à suivre
    public float followSpeed = 5f; // Vitesse de suivi
    public float rotationSpeed = 5f; // Vitesse de rotation
    public Vector3 offset = new Vector3(0, 2, -3); // Décalage positionnel

    void LateUpdate()
    {
        if (player == null) return;

        // Position lissée
        Vector3 targetPosition = player.position + player.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotation lissée
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}