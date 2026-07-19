using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRocketMovement : MonoBehaviour
{

    [Header("Input Action")]
    [SerializeField] private InputAction inpThrustRotationDoubleBind;
    [SerializeField] private InputAction inpThrust;
    
    
    [Header("Component Ref")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audioSource;


    [Header("Power value")]
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotationPower;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // mistake object would enable  after awake but it wont be all the axis like how would it enable fxn ran after awake but it doesn't have the ref for the same rb = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        inpThrust.Enable();
        inpThrustRotationDoubleBind.Enable();
        
    }

    void Start()
    {
         rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (inpThrust != null) {
        //    D
        //        }

        ProcessThrust();
    }

    private void Update()
    {
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (inpThrust.IsPressed())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }


           // Debug.Log("Is Pressed");

            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);

        } else { audioSource.Stop(); }
      
    }
    private void ProcessRotation()
    {
        float rotationValue = inpThrustRotationDoubleBind.ReadValue<float>();
       /* if (rotationValue == -1)
        {
            transform.Rotate(-Vector3.forward * rotationPower * Time.deltaTime);
        }
        else if (rotationValue == 1)
        {
            transform.Rotate(Vector3.forward * rotationPower * Time.deltaTime);
        } */


        if (rotationValue != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationValue * rotationPower * Time.deltaTime);

            rb.freezeRotation = false;

        }

                /*
                     Physics is controlling rotation.
                            ↓
                    I temporarily tell Unity:
                    "Stop rotating this Rigidbody using physics."
                            ↓
                    I rotate it myself using transform.Rotate().
                            ↓
                    I tell Unity:
                    "Okay, you can control rotation with physics again."
                */

    }


}
