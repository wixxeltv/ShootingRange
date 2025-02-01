using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public GameObject deathEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.targets++;
    }
    
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            GameManager.targets--;
            if (deathEffect != null)
            {
                GameObject temp = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(temp, 2);
                Destroy(gameObject);
            }
        }
       
   
    }
}
