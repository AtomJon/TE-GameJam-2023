using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviour : MonoBehaviour, IMortalEntity
{
    public int remainingHealthPoints { get; private set; } = 6;

    public bool IsAlive => remainingHealthPoints > 0;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    private float speed;

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

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
    }
}
