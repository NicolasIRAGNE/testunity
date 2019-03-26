using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public float speed;
    public AudioClip damage_sound;
    public AudioClip death_sound;
    public int damage;
    public GameObject drone;
    public float m_DronePos;
    public float m_DroneSpeed;

    private GameObject audioManager;
    private Transform player;
    private Vector3 direction;
    private List<GameObject> droneList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        maxHp = hp;
        player = GameObject.FindWithTag("Player").transform;
        audioManager = GameObject.FindWithTag("AudioManager");
        Debug.Assert(audioManager);
        SpawnDrones(8);
    }

    // Update is called once per frame
    void Update()
    {
        m_DronePos += Time.deltaTime * 2 * Mathf.PI * m_DroneSpeed;
        //m_DronePos += 0.01f;
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

    void OnDroneDown(GameObject drone)
    {
        int index = droneList.IndexOf(drone);
        droneList.RemoveAt(index);
        for (int i = 0; i < droneList.Count; i++)
        {
            droneList[i].SendMessage("SetPos", i * Mathf.PI * 2 / (float)(droneList.Count));
        }
        Debug.Log(droneList.Count);
    }

    void SpawnDrones(int number)
    {
        GameObject clone;
        for (int i = 0; i < number; i++)
        {
            clone = Instantiate(drone, transform.position, transform.rotation);
            droneList.Add(clone);
        }
        for (int i = 0; i < droneList.Count; i++)
        {
            droneList[i].SendMessage("SetPos", i * Mathf.PI * 2 / (float)(droneList.Count));
        }
    }

    private void OnDestroy()
    {
        if (audioManager != null)
            audioManager.SendMessage("PlayAudioAsync", death_sound);
    }

}
