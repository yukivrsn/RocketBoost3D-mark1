using UnityEngine;

public class dropper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody rb; // could skip serialized field
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    { 
        if (Time.timeSinceLevelLoad > 12f)
        {
            rb.useGravity = true;
        }
    }
}
