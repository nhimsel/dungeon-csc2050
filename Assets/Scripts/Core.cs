using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core
{
    public static Player thePlayer;
    public static List<string> discoveredRooms = new List<string>();

    public static void discoverRoom(string r)
    {
        discoveredRooms.Add(r);
    }

    public static bool isNewRoom(string r)
    {
        return !discoveredRooms.Contains(r);
    }
}