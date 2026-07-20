using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRocketMovement : MonoBehaviour
{

    [Header("Input Action")]
    [SerializeField] private InputAction inpThrustRotationDoubleBind;
    [SerializeField] private InputAction inpThrust;
    
    
    [Header("Component Ref")]
    //[SerializeField] we use it jsut as convention with out there wont we issue like here but we doit as a goof practice
    private Rigidbody rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxThrust;


    [Header("Power value")]
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotationPower;

    [Header("Thrusters")]
    [SerializeField] private ParticleSystem engineThruster;
    [SerializeField] private ParticleSystem leftSideThrust;
    [SerializeField] private ParticleSystem rightSideThrust;
    [SerializeField] private ParticleSystem behindSideThrust;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // no need as we are already giving the reference in the inspector but if we forget to give it then it will throw error so this is a good practice to have it here as well
        // mistake object would enable  after awake but it wont be all the axis like how would it enable fxn ran after awake but it doesn't have the ref for the same rbDropper = GetComponent<Rigidbody>();

    }


    private void OnEnable()
    {
        inpThrust.Enable();
        inpThrustRotationDoubleBind.Enable();
        
    }

    void Start()
    {
        // rbDropper = GetComponent<Rigidbody>();
        
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
            engineThruster.Play();
            leftSideThrust.Play();
            rightSideThrust.Play(); 
            behindSideThrust.Play();    

            if (!audioSource.isPlaying) // so that it won't paly 60time each second
            {
                audioSource.PlayOneShot(sfxThrust);
            }

            /*
            if (!engineThruster.isPlaying)
            {
                engineThruster.Play();
            }
             */


           // Debug.Log("Is Pressed");

            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);

        } else { audioSource.Stop();

            engineThruster.Stop();
            leftSideThrust.Stop();
            rightSideThrust.Stop();
            behindSideThrust.Stop(); 
        }
      
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

            if (rotationValue  < 0)
            {
                leftSideThrust.Stop();
                rightSideThrust.Play();
                behindSideThrust.Play();
            } else if (rotationValue > 0) { rightSideThrust.Stop();
                leftSideThrust.Play();
                behindSideThrust.Play();
            }

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
