using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    private Vector3 targetPosition;
    private float pauseTimeRemaining;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewTargetPosition();
        pauseTimeRemaining = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if (pauseTimeRemaining > 0f)
        {
            pauseTimeRemaining -= Time.deltaTime;
        }
        else
        {
            targetPosition.y = transform.position.y;
            Vector3 direction = targetPosition - transform.position;

            // Perform raycast to check for obstacles before each movement
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude + .5f))
            {;
                if (hit.collider.CompareTag("Obstacle"))
                {
                    // Obstacle in the way, set new target position
                    SetNewTargetPosition();
                    return;
                }
            }

            rb.velocity = direction.normalized * movementSpeed;

            if (direction.magnitude < 0.1f)
            {
                pauseTimeRemaining = Random.Range(1f, 3f);
                rb.velocity = Vector3.zero;
                SetNewTargetPosition();
            }
        }
    }

    private void SetNewTargetPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        targetPosition = randomPosition;
    }
}