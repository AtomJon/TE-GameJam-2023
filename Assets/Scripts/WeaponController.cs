using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    Transform spawnTransform;

    [SerializeField]
    int damageAmount = 2;

    [SerializeField]
    float hitCastRadius = .3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Bullet bullet = new(spawnTransform.position);
            //bullets.Add(bullet);

            //Camera.main.rect;
            var ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f));
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 2f);

            var contacts = Physics.SphereCastAll(ray.origin, hitCastRadius, ray.direction);
            foreach (var contact in contacts)
            {
                IMortalEntity target;
                if (contact.transform.TryGetComponent<IMortalEntity>(out target))
                {
                    target.dealDamage(damageAmount);
                }
            }

        }
    }
}
