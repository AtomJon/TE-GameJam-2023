using System.Collections;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomMovement : MonoBehaviour, IMortalEntity
{
    public float movementSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    private Vector3 targetPosition;
    private float pauseTimeRemaining;
    private Rigidbody rb;

    private AudioSource audioSource;

    [SerializeField]
    public int remainingHealthPoints { get; private set; } = 12;

    [SerializeField]
    public bool IsAlive => remainingHealthPoints > 0;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        SetNewTargetPosition();
        pauseTimeRemaining = Random.Range(1f, 3f);

        StartCoroutine(WaitAndStartSound());
    }

    IEnumerator WaitAndStartSound()
    {
        yield return new WaitForSeconds(Random.Range(0, 4));
        audioSource.Play();
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
        Vector3 randomPosition = new Vector3(transform.position.x + Random.Range(minX, maxX), transform.position.y, transform.position.z + Random.Range(minZ, maxZ));
        targetPosition = randomPosition;
    }

    void IMortalEntity.dealDamage(int damage)
    {
        remainingHealthPoints -= damage;

        if (IsAlive == false)
        {
            this.enabled = false;
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);
        }
    }
}