using UnityEngine;
using System.Collections;

public class PlayerHealthBarScript : MonoBehaviour
{

    public GUIStyle progress_empty;
    public GUIStyle progress_full;

    //current progress
    public float barDisplay;

    public int posX;
    public int posY;
    Vector2 size = new Vector2(250, 50);

    public Texture2D emptyTex;
    public Texture2D fullTex;
    public GameObject user;
    private float hp;
    private float maxHp;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(posX, posY, size.x, size.y), emptyTex, progress_empty);

        GUI.Box(new Rect(posX, posY, size.x, size.y), fullTex, progress_full);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {

        //the player's health
        if (user != null)
        {
            hp = user.GetComponent<PlayerScript>().hp;
            maxHp = user.GetComponent<PlayerScript>().maxHp;
            barDisplay = hp / maxHp;
        }
    }
}