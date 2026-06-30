using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    [SerializeField] private InputAction inpThrust;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speedThrust;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        inpThrust.Enable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (inpThrust != null) {
        //    D
        //        }

        if (inpThrust.IsPressed())
        {
            Debug.Log("Is Pressed");

            rb.AddRelativeForce(Vector3.up * speedThrust * Time.deltaTime);

        }
    }
}
