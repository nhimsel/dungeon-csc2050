using UnityEngine;

public class Dungeon
{
    public Dungeon()
    {
        Room r1 = new Room();
	    Room r2 = new Room();
	    Room r3 = new Room();
	    Room r4 = new Room();
	    Room r5 = new Room();
	    Room r6 = new Room();

        r1.addExit("north", r2);
	    r2.addExit("north", r3);
	    r2.addExit("south", r1);
	    r3.addExit("south", r2);
	    r3.addExit("west", r4);
	    r3.addExit("north", r6);
	    r3.addExit("east", r5);
	    r4.addExit("east", r3);
	    r5.addExit("west", r3);
	    r6.addExit("south", r3);

		Core.thePlayer.setCurrentRoom(r1);
    }
}
