using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public AudioClip damage_sound;
    public AudioClip death_sound;
    private GameObject audioManager;
    public int damage;

    private Transform player;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager");
        Debug.Assert(audioManager);
        player = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void Damage(int dmg)
    {
        //AudioSource.PlayClipAtPoint(damage_sound, this.gameObject.transform.position);
        audioManager.SendMessage("PlayAudioAsync", damage_sound);
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.SendMessage("Damage", damage);
        }
    }

    private void OnDestroy()
    {
        audioManager.SendMessage("PlayAudioAsync", death_sound);
    }
}
