using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDroneScript : MonoBehaviour
{
    public int hp;
    public float speed;
    public AudioClip damage_sound;
    public AudioClip death_sound;
    public int damage;

    private GameObject m_AudioManager;
    private float m_RelativePos;
    private GameObject m_Parent;
    private Vector3 m_Target;

    // Start is called before the first frame update
    void Start()
    {
        m_Parent = GameObject.FindWithTag("Boss");
        m_AudioManager = GameObject.FindWithTag("AudioManager");
        Debug.Assert(m_AudioManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Parent)
        {
            Destroy(gameObject);
        }
        float pos = m_RelativePos + m_Parent.GetComponent<Boss1>().m_DronePos;
        float x = m_Parent.transform.position.x + (Mathf.Cos(pos) * 2);
        float y = m_Parent.transform.position.y + (Mathf.Sin(pos) * 2);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, 0), speed * Time.deltaTime);
    }

    void SetPos(float pos)
    {
        m_RelativePos = pos;
    }

    void ChangePos(float add)
    {
        m_RelativePos -= (2 * Mathf.PI) / add;
    }

    void Damage(int dmg)
    {
        //AudioSource.PlayClipAtPoint(damage_sound, this.gameObject.transform.position);
        m_AudioManager.SendMessage("PlayAudioAsync", damage_sound);
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
        if (m_AudioManager != null)
            m_AudioManager.SendMessage("PlayAudioAsync", death_sound);
        if (m_Parent)
        {
            m_Parent.GetComponent<Boss1>().SendMessage("OnDroneDown", gameObject);
        }
    }
}
