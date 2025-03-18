using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Room
{
    private GameObject[] theDoors;
    /* private Exit[] availableExits = new Exit[4];
    private int currNumberOfExits = 0; */    
    private List<Exit> availableExits = new List<Exit>();
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
        for(int i=0; i<this.availableExits.Count; i++)
        {
            if(direction.Equals(this.availableExits.ElementAt(i).getDirection()))
            {
                Core.thePlayer.setCurrentRoom(this.availableExits[i].getDestination());
                return true;
            }
        }
       return false;
    }

    public bool hasExit(string direction)
    {
        for(int i=0; i<this.availableExits.Count; i++)
        {
            if(direction.Equals(this.availableExits.ElementAt(i).getDirection()))
            {
                return true;
            }
        }
       return false;
    }

    public void addExit(string direction, Room destination)
    {
        if(this.availableExits.Count<4)
        {
            Exit e = new Exit(direction, destination);
            this.availableExits.Add(e);
        }
   }
}
