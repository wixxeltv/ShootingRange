using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowInputs : MonoBehaviour
{
   [SerializeField]private GameObject img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnShowInputs(InputValue value)
    {
        img.SetActive(value.isPressed);
    }
}
