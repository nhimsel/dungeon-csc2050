using UnityEngine;

public class Fight
{
    private Inhabitant attacker;
    private Inhabitant defender;

    public Fight()
    {
        this.attacker = new Player("Player");
        this.defender = new Monster("Monster");            
        this.firstAttacker();
    }

    public Fight(Inhabitant attacker, Inhabitant defender)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.firstAttacker();
    }

    public void firstAttacker()
    {
        //swap the attacker and defender if the roll is over 10 
        if (Random.Range(1, 21) >= 10)
        {
            Inhabitant tmp = this.attacker;
            this.attacker = this.defender;
            this.defender = tmp;
        }
    }
    public void Begin()
    {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round.

        while (!this.attacker.isDead() && !this.defender.isDead())        
        {
            attacker.display();
            defender.display();
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

                if (defender.isDead())
                {
                    //defender is dead, attacker wins
                    printStuff(defender.getName() + " was killed. " +
                    attacker.getName() + " wins.");
                    return;
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

                    if (attacker.isDead())
                    {
                        //attacker was killed, defender wins
                        printStuff(attacker.getName() + " was killed. "
                        + defender.getName() + " wins.");
                         return;
                    }
                }
                else
                {
                    printStuff(defender.getName() + " defended themselves from the hit.");
                }
            }

            //swap players
            Inhabitant tmp = attacker;
            attacker = defender;
            defender = tmp;
        }
    }

    private void printStuff(string s)
    {
        Debug.Log(s);
    }
}