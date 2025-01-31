using UnityEngine;
using UnityEngine.InputSystem;

public class RifleController : Shooter
{
    private bool wantToFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wantToFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wantToFire.ToString());
        if (canFire && wantToFire)
        {
            StartCoroutine(fire());
        }
    }
    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            wantToFire = true;
        }
        else
        {
            wantToFire = false;
        }
    }
}
