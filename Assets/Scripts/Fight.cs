using UnityEngine;
using TMPro;

public class Fight
{
    private Inhabitant attacker;
    private Inhabitant defender;
    private GameObject attackerGO;
    private GameObject defenderGO;
    private TMP_Text attackerHPText;
    private TMP_Text attackerNameText;
    private TMP_Text defenderHPText;
    private TMP_Text defenderNameText;
    private TMP_Text commentaryText;

    /*
     * children of canvas:
     * current as of 03-31-2025
     * 0 - enemy hp
     * 1 - enemy name label
     * 2 - player hp
     * 3 - player name label
     * 4 - commentary
     * 5 - standard attack button
     * 6 - heavy attack button
     * 7 - heal button
     */

    public Fight(GameObject player, GameObject enemy, GameObject canvas)
    {
        this.attacker = new Player();
        this.defender = new Monster();            
        this.attackerGO = player;
        this.defenderGO = enemy;
        this.attackerHPText = canvas.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        this.defenderHPText = canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        this.commentaryText = canvas.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
        this.attackerNameText = canvas.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        this.defenderNameText = canvas.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        this.firstAttacker();
    }

    public Fight(Inhabitant attacker, Inhabitant defender, GameObject player, GameObject enemy, GameObject canvas) : this(player, enemy, canvas)
    {
        this.attacker = attacker;
        this.defender = defender;
    }

    public bool playerTurn()
    {
        return (this.attacker is Player);
    }

    public void firstAttacker()
    {
        //swap the attacker and defender if the roll is over 10 
        if (Random.Range(1, 21) >= 10)
        {
           swapPlayers(); 
        }
        setText();
    }

    private void swapPlayers()
    {
        //swap the defender and the attacker
        Inhabitant tmp = attacker;
        attacker = defender;
        defender = tmp;
        GameObject tmpGO = this.attackerGO;
        this.attackerGO = this.defenderGO;
        this.defenderGO = tmpGO;
        TMP_Text tmpHPText = this.attackerHPText;
        this.attackerHPText = this.defenderHPText;
        this.defenderHPText = tmpHPText;
        TMP_Text tmpNameText = this.attackerNameText;
        this.attackerNameText = this.defenderNameText;
        this.defenderNameText = tmpNameText;
    }
    private void setText()
    {
        //sets the initial HP and Name Text for the attacker and defender
        this.attackerHPText.SetText(this.attacker.getHP()+"/"+this.attacker.getMaxHP());
        this.defenderHPText.SetText(this.defender.getHP()+"/"+this.defender.getMaxHP());
        this.attackerNameText.SetText(this.attacker.getName()+"'s HP:");
        this.defenderNameText.SetText(this.defender.getName()+"'s HP:");
    }
    public void Turn(short atkType)
    {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round.

        // atkType determines the type of attack attempted
        // 0 is a standard attack, 1 is a heavy attack, 2 is heal

        if (!this.attacker.isDead() && !this.defender.isDead())
        {
            //attacker.display();
            //defender.display();
            printStuff(attacker.getName() + " is on the offensive.");

            //by default, everything has perfect accuracy
            int accRoll = 20;
            
            int atkRoll = Random.Range(1,21);
            
            //heavy attack
            if (atkType == 1)
            {
                // roll for accuracy
                accRoll = Random.Range(0,15);    

                //increase atkRoll by 20%
                atkRoll += (int) (atkRoll*.2);
            }

            if (atkRoll >= defender.getAC() && accRoll >= 5)
            {
                //defender has been hit
                int tempHP = defender.getHP();
                int damage = Random.Range(1,7);

                //scoring a critical hit
                bool crit = false;
                if (Random.Range(1,101)==77)
                {
                    damage*=2;
                    crit = true;
                }

                defender.takeDamage(damage, attacker.getSTR());

                printStuff(attacker.getName() + " deals " +
                (tempHP-defender.getHP()) + " points of damage to " +
                defender.getName()+".");

                if (crit) printStuff("It was a critical hit!");

                defenderHPText.SetText(defender.getHP() + "/" + defender.getMaxHP());

                if (defender.isDead())
                {
                    //defender is dead, attacker wins
                    printStuff(defender.getName() + " was killed. " +
                    attacker.getName() + " wins.");
                    GameObject.Destroy(defenderGO);
                }
            }

            else if (atkType == 2)
            {
                //attacker chose to heal

            }

            else
            {
                //attack defended
                if (atkRoll<=2)
                {
                    //attack missed, defender counterattacks
                    int damage = Random.Range(1,3);
                    attacker.takeDamage(damage);

                    //scoring a critical hit
                    bool crit = false;
                    if (Random.Range(1,101)==77)
                    {
                        damage*=2;
                        crit = true;
                    }

                    printStuff(attacker.getName() +
                    " missed their attack! " + defender.getName() +
                    " counterattacks and deals " + damage + " points of damage.");

                    if (crit) printStuff("It was a critical hit!");

                    attackerHPText.SetText(attacker.getHP() + "/" + attacker.getMaxHP());
                   
                    if (attacker.isDead())
                    {
                        //attacker is dead, defender wins
                        printStuff(attacker.getName() + " was killed. "
                        + defender.getName() + " wins.");
                        GameObject.Destroy(attackerGO);
                    }
                }
                else
                {
                    printStuff(defender.getName() + " defended himself from the hit.");
                }
            }
            swapPlayers();
        }
    }

    private void printStuff(string s)
    {
        //Debug.Log(s);
        commentaryText.SetText(s);
    }
}