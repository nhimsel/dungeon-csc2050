using System;
using UnityEngine;
using TMPro;

public class fightManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject canvas;
    private Button basicAttack;
    private Button heavyAttack;
    private Button heal; 
    private Fight fight;
    private float time;
    private float wait = 1f;

    void Start()
    {
        fight = new Fight(player, enemy, canvas);

        //layout of canvas children detailed in Fight.cs
        basicAttack = canvas.gameObject.GetChild(5).gameObject.GetComponent<Button>();
        heavyAttack = canvas.gameObject.GetChild(6).gameObject.GetComponent<Button>();
        heal = canvas.gameObject.GetChild(7).gameObject.GetComponent<Button>();
    }

    void FixedUpdate()
    {
        if(timeCheck() && !this.fight.playerTurn())
        {
            // monster takes a basic attack
            fight.Turn(0);
        }
        else
        {
            // it's the player's turn, choose an attack
            // 0 for basic attack, 1 for heavy attack, 2 for heal
            short atkType;
            if (/* basicAttack button was pressed*/) atkType = 0;
            else if (/*heavyAttack button was pressed*/) atkType = 1;
            else if (/*heal button was pressed*/) atkType = 2;
            fight.Turn(atkType);
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