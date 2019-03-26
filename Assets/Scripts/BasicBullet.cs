using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public Vector3 target;
    private Camera cam;
    private float startTime;
    public float lifespan = 3.0f;
    Vector3 mousePosition;
    Vector3 direction;
    Vector3 directionVector;

    public float speed = 70f;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0F;
        direction = (mousePosition - transform.position).normalized;
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
}
