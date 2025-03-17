using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Room
{
    private GameObject[] theDoors;
    private Exit[] availableExits = new Exit[4];
    private int currNumberOfExits = 0;
    private int ID=-1;

    private string name;

    public Room(string name)
    {
        this.name = name;
        this.ID = Core.lastUsedID++;
    }

    public string getName()
    {
        return this.name;
    }

    public int getID()
    {
        return this.ID;
    }

    public bool tryToTakeExit(string direction)
    {
        for(int i = 0; i<this.currNumberOfExits; i++)
        {
            if(String.Equals(this.availableExits[i].getDirection(), direction))
            {
                Core.thePlayer.setCurrentRoom(this.availableExits[i].getDestination());
                return true;
            }
        }
        return false;
    }

    public bool hasExit(string direction)
    {
        for(int i = 0; i < this.currNumberOfExits; i++)
        {
            if(String.Equals(this.availableExits[i].getDirection(), direction))
            {
                return true;
            }
        }
        return false;
    }
    public void addExit(string direction, Room destination)
    {
        if(this.currNumberOfExits <= 3)
        {
            Exit e = new Exit(direction, destination);
            this.availableExits[this.currNumberOfExits] = e;
            this.currNumberOfExits++;
        }
        else
        {
            Console.Error.WriteLine("there are too many exits!!!!");
        }
    }

}
