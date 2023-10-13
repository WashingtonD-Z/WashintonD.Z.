using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Speed at which the enemy moves

    private void Update()
    {
        // Move the enemy forward in its local space
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
