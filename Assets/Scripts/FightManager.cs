using System;
using UnityEngine;

public class fightManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private Fight fight;
    private float time;
    private float wait = 1f;
    void Start()
    {
        fight = new Fight(player, enemy);
    }

    void FixedUpdate()
    {
        if(timeCheck())
        {
            fight.Turn();
        }
        time+=Time.deltaTime;
    }

    private bool timeCheck()
    {
        //using this.time, returns true if >=3s, else returns false
        if (this.time>=wait)
        {
            time -= wait;
            return true;
        }
        return false;
    }
}