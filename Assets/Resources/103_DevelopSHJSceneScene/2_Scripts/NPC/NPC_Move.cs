﻿using UnityEngine;
using System.Collections;

public class NPC_Move : MonoBehaviour // NPC의 움직임
{
    public GameObject NPC = null;
    public float Speed = 10;
    float pos_x = 0;
    public bool Chek = false;
    public AudioSource audo = null;
    public Animator ani = null;
    

    private NPC_Probability rand = null;

    private GameTimer time = null;
    

    // Use this for initialization
    void Start()
    {
        rand = NPC_Probability.Randomes;
        time = GameTimer.Instance;

        StartCoroutine(UpdateStatus());
    }

    int Rand_go; 

    IEnumerator UpdateStatus()
    {
        Rand_go = rand.Rand_GO;
        int SDay = time.Day;
        float STime = time.Hour;
        while (true)
        {
            if (time.getTimeGap(SDay, STime) >= 1.0f)
            {
                SDay = time.Day;
                STime = time.Hour;

            }
            pos_x = transform.localPosition.x;

            if (Rand_go <= SDay && Chek == false)
            {
                ani.SetBool("NPC_Walk", true);
                transform.localPosition = new Vector3(pos_x += Speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            else ani.SetBool("NPC_Walk", false);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Dor")
        {
            audo.Play();
            Chek = true;
            Rand_go += rand.Rand_GO;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
