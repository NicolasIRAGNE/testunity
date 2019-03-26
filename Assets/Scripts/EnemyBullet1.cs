using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet1 : MonoBehaviour
{
    public Vector3 target;
    private float startTime;
    public float lifespan = 3.0f;
    Vector3 direction;
    Vector3 mousePosition;
    GameObject player;
    public int damage;
    GameObject parent;

    public float speed = 70f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction.x = player.transform.position.x - transform.position.x;
        direction.y = player.transform.position.y - transform.position.y;
        direction = (player.transform.position - transform.position).normalized;
        startTime = Time.time;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + lifespan < Time.time)
        {
            Destroy(gameObject);
        }
        transform.position += direction * Time.deltaTime * speed;
    }

    void setDamage(int dmg)
    {
        damage = dmg;
    }

    void setParent(GameObject p)
    {
        parent = p;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != parent && col.gameObject.tag != "Bullet" && col.gameObject.tag != "Boss" && col.gameObject.tag != "Enemy")
        {
            col.SendMessage("Damage", damage);
            Destroy(gameObject);
        }
    }

}
