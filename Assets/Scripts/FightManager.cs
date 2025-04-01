using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fightManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject canvas;
    private Button basicAttack, heavyAttack, heal;
    private Fight fight;
    private float time;
    private float wait = 2f;
    private short atkType = -1;

    void Start()
    {
        fight = new Fight(player, enemy, canvas);

        //layout of canvas children detailed in Fight.cs
        basicAttack = canvas.transform.GetChild(6).gameObject.GetComponent<Button>();
        heavyAttack = canvas.transform.GetChild(7).gameObject.GetComponent<Button>();
        heal = canvas.transform.GetChild(8).gameObject.GetComponent<Button>();

        //add listeners to manage button inputs
        basicAttack.onClick.AddListener(() => atkType=0);
        heavyAttack.onClick.AddListener(() => atkType=1);
        heal.onClick.AddListener(() => atkType=2);
    }

    void FixedUpdate()
    {
        if(timeCheck() && !this.fight.playerTurn())
        {
            // monster takes a basic attack
            fight.Turn(0);

            //reset timer
            this.time=0f;
        }
        else if (timeCheck() && this.fight.playerTurn())
        {
            // it's the player's turn, choose an attack
            // 0 for basic attack, 1 for heavy attack, 2 for heal
            if (atkType >=0)
            {
                fight.Turn(atkType);
                atkType=-1;

                //reset timer
                this.time=0f;
            }
        }
        time+=Time.deltaTime;
    }

    private bool timeCheck()
    {
        //using this.time, returns true if >=3s, else returns false
        if (this.time>=wait)
        {
            return true;
        }
        return false;
    }
}