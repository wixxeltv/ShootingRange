using UnityEngine;

public class Bullets : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public GameObject collisionEffect;
    public float damage = 25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Collision");
        if (collisionEffect != null)
        {
           GameObject temp = Instantiate(collisionEffect, transform.position, Quaternion.identity);
           Destroy(temp, 2);
        }
        Destroy(gameObject);
    }
}