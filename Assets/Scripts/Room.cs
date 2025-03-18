using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Room
{
    private GameObject[] theDoors;
    private List<Exit> availableExits = new List<Exit>();
    private bool discovered = false;

    public bool isDiscovered()
    {
        return discovered;
    }

    public bool tryToTakeExit(string direction)
    {
        for(int i=0; i<this.availableExits.Count; i++)
        {
            if(direction.Equals(this.availableExits.ElementAt(i).getDirection()))
            {
                Core.thePlayer.setCurrentRoom(this.availableExits[i].getDestination());
                this.discovered = true;
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
