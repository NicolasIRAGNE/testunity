using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyWeapon : MonoBehaviour
{
    public AudioClip sound;
    public GameObject bullet;
    public float fireRate = 1f;
    public int damage;
    private GameObject audioManager;
    float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager");
        Debug.Assert(audioManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShot + fireRate)
        {
            audioManager.SendMessage("PlayAudioAsync", sound);
            lastShot = Time.time;

            GameObject clone;
            clone = Instantiate(bullet, transform.position, transform.rotation);
            clone.SendMessage("setDamage", damage);
            clone.SendMessage("setParent", gameObject);
            // Give the cloned object an initial velocity along the current
            // object's Z axis
        }

    }
}
