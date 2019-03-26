using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : MonoBehaviour
{
    private Camera cam;
    public int damage;
    private float startTime;
    public float lifespan = 3.0f;
    Vector3 mousePosition;
    Vector3 direction;
    Vector3 directionVector;

    public float speed = 70f;
    // Start is called before the first frame update
    void Start()
    {
        Random rnd = new Random();
        startTime = Time.time;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0F;
        direction = (mousePosition - transform.position).normalized;
        direction.x += +Random.Range(-0.12f, 0.12f);
        direction.y += +Random.Range(-0.12f, 0.12f);
        direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + lifespan < Time.time)
        {
            Destroy(gameObject);
        }
        transform.position += direction * speed * Time.deltaTime;

    }

    void setDamage(int dam)
    {
        damage = dam;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")
        {
            col.transform.SendMessage("Damage", damage);
            Destroy(gameObject);
        }
    }

}
