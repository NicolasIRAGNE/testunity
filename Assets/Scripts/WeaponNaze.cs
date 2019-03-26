using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNaze : MonoBehaviour
{
    private GameObject audioManager;
    public GameObject bullet;
    public AudioClip shot_sound;
    public float fireRate = 0.1f;
    public int damage;
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
        if (Input.GetMouseButton(0) && Time.time > lastShot + fireRate)
        {
            audioManager.SendMessage("PlayAudioAsync", shot_sound);
            lastShot = Time.time;

            GameObject clone;
            clone = Instantiate(bullet, transform.position, transform.rotation);
            clone.SendMessage("setDamage", damage);
            // Give the cloned object an initial velocity along the current
            // object's Z axis
        }

    }
}
