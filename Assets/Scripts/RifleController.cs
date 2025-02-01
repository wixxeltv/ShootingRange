using UnityEngine;
using UnityEngine.InputSystem;

public class RifleController : Shooter
{
    private bool wantToFire;
    [SerializeField]private Animator playerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wantToFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire && wantToFire && playerAnimator.GetBool("IsAiming"))
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
