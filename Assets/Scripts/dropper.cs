using UnityEngine;

public class dropper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // [SerializeField] private Rigidbody rbDropper; // could skip serialized field
    [SerializeField] private float time;
    private Rigidbody rbDropper;
    private void Awake()
    {
        rbDropper = GetComponent<Rigidbody>();
    }
    void Update()
    { 
        if (Time.timeSinceLevelLoad > time)
        {
            rbDropper.useGravity = true;
        }
    }
}
