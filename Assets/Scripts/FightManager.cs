using System;
using UnityEngine;
using TMPro;

public class fightManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    // public TMP_Text playerNameText;
    // public TMP_Text enemyNameText;
    // public TMP_Text playerHPText;
    // public TMP_Text enemyHPText;
    // public TMP_Text commentaryText;
    public GameObject canvas;
    private Fight fight;
    private float time;
    private float wait = 1f;

    void Start()
    {
        // fight = new Fight(player, enemy, playerHPText, enemyHPText, commentaryText, playerNameText, enemyNameText);
        fight = new Fight(player, enemy, canvas);
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