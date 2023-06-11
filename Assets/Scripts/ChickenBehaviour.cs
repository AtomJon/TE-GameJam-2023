using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenBehaviour : MonoBehaviour, IMortalEntity
{
    public int remainingHealthPoints { get; private set; } = 6;

    public bool IsAlive => remainingHealthPoints > 0;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    private float speed;
    private NavMeshAgent agent;

    public void dealDamage(int damage)
    {
        remainingHealthPoints -= damage;

        if (IsAlive == false)
        {
            this.enabled = false;
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = targetTransform.position;
    }
}
