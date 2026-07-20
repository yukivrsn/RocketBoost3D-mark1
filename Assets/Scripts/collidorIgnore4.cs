using UnityEngine;

public class IgnoreObstacleCollision : MonoBehaviour
{
    [SerializeField] private Collider obstacle1;
    [SerializeField] private Collider obstacle2;

    private void Start()
    {
        Physics.IgnoreCollision(obstacle1, obstacle2);
    }
}