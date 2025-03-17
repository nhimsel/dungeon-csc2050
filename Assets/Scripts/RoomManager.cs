using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] theDoors;
    public GameObject minimapRoom;
    private Dungeon theDungeon;
    private int curX=0;
    private int curY=0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Core.thePlayer = new Player("Player");
        this.theDungeon = new Dungeon();
        this.setupRoom();
    }

    //disable all doors
    private void resetRoom()
    {
        this.theDoors[0].SetActive(false);
        this.theDoors[1].SetActive(false);
        this.theDoors[2].SetActive(false);
        this.theDoors[3].SetActive(false);
    }

    //show the doors appropriate to the current room
    private void setupRoom()
    {
        Room currentRoom = Core.thePlayer.getCurrentRoom();
        this.theDoors[0].SetActive(currentRoom.hasExit("north"));
        this.theDoors[1].SetActive(currentRoom.hasExit("south"));
        this.theDoors[2].SetActive(currentRoom.hasExit("east"));
        this.theDoors[3].SetActive(currentRoom.hasExit("west"));
    }

    private bool tryMove(string direction)
    {
        //return Core.thePlayer.getCurrentRoom().tryToTakeExit(direction);
        if(Core.thePlayer.getCurrentRoom().tryToTakeExit(direction))
        {
            int ID = Core.thePlayer.getCurrentRoom().getID();
            dirModCoord(direction);
            if(Core.isNewRoom(ID))
            {
                Core.discoverRoom(ID);
                Debug.Log("discovered R"+ID);
                newRoomMinimap(curX, curY);
            }
            return true;
        }
        return false;
    }
    
    private void dirModCoord(string dir)
    {
        if(dir.Equals("north")) curY++;
        else if(dir.Equals("south")) curY--;
        else if(dir.Equals("west")) curX--;
        else if(dir.Equals("east")) curX++;
    }

    private void newRoomMinimap(int xMult, int yMult)
    {
        //Debug.Log("curx = "+curX+"\ncury = "+curY);
        GameObject newRoom = Instantiate(this.minimapRoom);
        newRoom.transform.SetParent(this.minimapRoom.transform.parent);
        Vector3 currPos = newRoom.transform.position;
        Vector3 newPos = new Vector3(currPos.x + (11f*xMult), currPos.y + (11f*yMult), currPos.z);
        newRoom.transform.SetLocalPositionAndRotation(newPos,newRoom.transform.rotation);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(tryMove("north")) 
            {
                this.setupRoom();
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tryMove("west"))
            {
                this.setupRoom();
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(tryMove("east"))
            {
                this.setupRoom();
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(tryMove("south")) 
            {
                this.setupRoom();
            }
        }
    }
}
