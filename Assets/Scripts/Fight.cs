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

    public Fight(GameObject player, GameObject enemy, TMP_Text playerHPText, TMP_Text enemyHPText, TMP_Text commentaryText, TMP_Text playerNameText, TMP_Text enemyNameText)
    {
        this.attacker = new Player();
        this.defender = new Monster();            
        this.attackerGO = player;
        this.defenderGO = enemy;
        this.attackerHPText = playerHPText;
        this.defenderHPText = enemyHPText;
        this.commentaryText = commentaryText;
        this.attackerNameText = playerNameText;
        this.defenderNameText = enemyNameText;
        this.firstAttacker();
    }

    public Fight(Inhabitant attacker, Inhabitant defender, GameObject player, GameObject enemy, TMP_Text playerHPText, TMP_Text enemyHPText, TMP_Text commentaryText, TMP_Text playerNameText, TMP_Text enemyNameText) : this(player, enemy, playerHPText, enemyHPText, commentaryText, playerNameText, enemyNameText)
    {
        this.attacker = attacker;
        this.defender = defender;
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
    public void Turn()
    {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round.

        if (!this.attacker.isDead() && !this.defender.isDead())
        {
            //attacker.display();
            //defender.display();
            printStuff(attacker.getName() + " is on the offensive.");

            int atkRoll = Random.Range(1,21);
            if (atkRoll >= defender.getAC())
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