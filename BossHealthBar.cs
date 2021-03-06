﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public GUIStyle progress_empty;
    public GUIStyle progress_full;

    //current progress
    public float barDisplay;

    Vector2 pos = new Vector2(200, 50);
    Vector2 size = new Vector2(250, 50);

    public Texture2D emptyTex;
    public Texture2D fullTex;
    private GameObject player;
    private float hp;
    private float maxHp;

    void OnGUI()
    {
        player = GameObject.FindWithTag("Player");

        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y), emptyTex, progress_empty);

        GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), fullTex, progress_full);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {

        //the player's health
        if (player != null)
        {
            hp = player.GetComponent<PlayerScript>().hp;
            maxHp = player.GetComponent<PlayerScript>().maxHp;
            barDisplay = hp / maxHp;
        }
    }
}