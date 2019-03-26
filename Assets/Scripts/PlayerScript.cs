using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float hp = 100;
    public float maxHp = 100;
    public float speed = 0.05f;

    public float iFrames = 1;
    // Start is called before the first frame update
    private float lastHit = 0;
    void Start()
    {
        Vector3 position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        Vector3 spritePos = transform.position;
        float XAxis = Input.GetAxisRaw("Horizontal");
        float YAxis = Input.GetAxisRaw("Vertical");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        XAxis = Mathf.Round(XAxis);
        YAxis = Mathf.Round(YAxis);
        speed = Time.deltaTime * 10;
        transform.position = new Vector3(spritePos.x + XAxis * speed, spritePos.y + YAxis * speed, spritePos.z);
        if (hp <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void Damage(int damage)
    {
        if (Time.time > lastHit + iFrames)
        {
            hp -= damage;
            lastHit = Time.time;
        }
    }
}
