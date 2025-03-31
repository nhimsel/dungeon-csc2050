using UnityEngine;

public abstract class Inhabitant
{
    protected int currHp;
    protected int maxHp;
    protected int ac;
    protected int str;
    protected string name;

    public Inhabitant(string name)
    {
        this.name = name;
        this.maxHp = Random.Range(15, 30);
        this.currHp = this.maxHp;
        this.ac = Random.Range(8, 12);
        this.str = Random.Range(5,17);
    }

    public int getHP()
    {
        return this.currHp;
    }
    
    public int getMaxHP()
    {
        return this.maxHp;
    }

    public int getAC()
    {
        return this.ac;
    }

    public int getSTR()
    {
        return this.str;
    }

    public string getName()
    {
        return this.name;
    }

    public bool isDead()
    {
        return this.currHp<=0;
    }

    public void heal(int h)
    {
        this.currHp+=h;
        checkHP();
    }

    public void takeDamage(int d)
    {
        this.currHp-=d;
        checkHP();
    }

    public void takeDamage(int d, int str)
    {
        this.currHp-=(int)((str / 3) * d)/((int)(this.ac / 4)+1);
        checkHP();
    }

    private void checkHP()
    {
        if(this.currHp<0)
        {
            this.currHp=0;
        }
        else if (this.currHp>this.maxHp)
        {
            this.currHp = this.maxHp;
        }
    }

    public void display()
    {
        Debug.Log("name: "+this.name+"\nhp: "+this.currHp+"\nac: "+this.ac+"\nstr: "+this.str);
    }
}