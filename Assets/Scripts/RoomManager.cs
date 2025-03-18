using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] theDoors;
    public GameObject minimapRoom;
    private Dungeon theDungeon;
    private Vector2Int coords = new Vector2Int(0,0);

    void Start()
    {
        Core.thePlayer = new Player("Player");
        this.theDungeon = new Dungeon();
        this.setupRoom();
    }

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
        if(Core.thePlayer.getCurrentRoom().tryToTakeExit(direction))
        {
            dirModCoord(direction);
            if(!Core.thePlayer.getCurrentRoom().isDiscovered())
            {
                newRoomMinimap(coords.x, coords.y);
            }
            return true;
        }
        return false;
    }
    
    private void dirModCoord(string dir)
    {
        if(dir.Equals("north")) coords.y++;
        else if(dir.Equals("south")) coords.y--;
        else if(dir.Equals("west")) coords.x--;
        else if(dir.Equals("east")) coords.x++;
    }

    private void newRoomMinimap(int xMult, int yMult)
    {
        GameObject newRoom = Instantiate(this.minimapRoom);
        newRoom.transform.SetParent(this.minimapRoom.transform.parent);
        Quaternion rotation;
        Vector3 currPos;
        this.minimapRoom.transform.GetLocalPositionAndRotation(out currPos, out rotation);
        Vector3 newPos = new Vector3(currPos.x + (11f*xMult), currPos.y + (11f*yMult), currPos.z);
        newRoom.transform.SetLocalPositionAndRotation(newPos,rotation);
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
