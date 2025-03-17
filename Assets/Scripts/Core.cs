using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core
{
    public static Player thePlayer;
    public static int lastUsedID=0;
    public static List<int> discoveredRooms = new List<int>();

    public static void discoverRoom(int ID)
    {
        discoveredRooms.Add(ID);
    }

    public static bool isNewRoom(int ID)
    {
        return !discoveredRooms.Contains(ID);
    }
}
