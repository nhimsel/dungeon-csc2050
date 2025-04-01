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
    private Transform attackerHPBar;
    private TMP_Text defenderHPText;
    private TMP_Text defenderNameText;
    private Transform defenderHPBar;
    private TMP_Text commentaryText;
    private TMP_Text commentary2Text;

    /*
     * children of canvas:
     * current as of 04-01-2025
     * 0 - enemy hp bar
     * -> 0 - hp bar
     * -> 1 - hp text
     * 1 - player hp bar
     * -> 0 - hp bar
     * -> 1 - hp text
     * 2 - enemy name label
     * 3 - player name label
     * 4 - commentary
     * 5 - commentary 2
     * 6 - standard attack button
     * 7 - heavy attack button
     * 8 - heal button
     */

    public Fight(GameObject player, GameObject enemy, GameObject canvas)
    {
        this.attacker = new Player();
        this.defender = new Monster();            
        this.attackerGO = player;
        this.defenderGO = enemy;
        this.attackerHPText = canvas.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        this.defenderHPText = canvas.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        this.attackerHPBar = canvas.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform;
        this.defenderHPBar = canvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform;
        this.commentaryText = canvas.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
        this.commentary2Text = canvas.transform.GetChild(5).gameObject.GetComponent<TMP_Text>();
        this.attackerNameText = canvas.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        this.defenderNameText = canvas.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

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
        Transform tmpHPBar = this.attackerHPBar;
        this.attackerHPBar = this.defenderHPBar;
        this.defenderHPBar = tmpHPBar;
        TMP_Text tmpNameText = this.attackerNameText;
        this.attackerNameText = this.defenderNameText;
        this.defenderNameText = tmpNameText;
    }
    private void setText()
    {
        //sets the initial HP and Name Text for the attacker and defender
        this.updateHP();
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
            //we don't print this if we're gonna heal
            if (atkType!=2) printStuff(attacker.getName() + " is on the offensive.");

            //by default, everything has perfect accuracy
            int accRoll = 20;
            
            int atkRoll = Random.Range(1,21);
            
            //heavy attack
            if (atkType == 1)
            {
                // roll for accuracy
                accRoll = Random.Range(0,15);    

                //increase atkRoll by 50%
                atkRoll += (int) (atkRoll*.5);
            }

            //player can only attack if their attack roll clears the defender's armor class
            // and if their accuracy roll is high enough (and if they're not healing)
            if (atkRoll >= defender.getAC() && accRoll >= 5 && atkType!=2)
            {
                //defender has been hit
                int tempHP = defender.getHP();
                int damage = Random.Range(1,7);
                
                //50% more damage if it's a heavy attack
                if (atkType == 1) damage = (int)(damage+.5*damage);

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

                this.updateHP();

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
                int heal = (int) (attacker.getMaxHP()*.25);
                
                //heal extra if in critical health
                if (attacker.getHP()<=5) heal = (int)(heal+heal*.3);

                attacker.heal(heal);
                this.updateHP();

                printStuff(attacker.getName() + " heals " + heal + " HP.");
            }

            else
            {
                //attack defended
                if (accRoll<=3)
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

                    this.updateHP();
                   
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

    private void updateHP()
    {
        attackerHPText.SetText(attacker.getHP() + "/" + attacker.getMaxHP());
        defenderHPText.SetText(defender.getHP() + "/" + defender.getMaxHP());
    
        attackerHPBar.localScale = new Vector3(((float)attacker.getHP()/attacker.getMaxHP()), attackerHPBar.localScale.y, attackerHPBar.localScale.z);
        defenderHPBar.localScale = new Vector3(((float)defender.getHP()/defender.getMaxHP()), defenderHPBar.localScale.y, defenderHPBar.localScale.z);
    }

    private void printStuff(string s)
    {
        //Debug.Log(s);
        commentary2Text.SetText(commentaryText.text);
        commentaryText.SetText(s);
    }
}