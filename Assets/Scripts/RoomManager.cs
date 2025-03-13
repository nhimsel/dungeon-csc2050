using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] theDoors;
    public GameObject minimapRoom;
    private Dungeon theDungeon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Core.thePlayer = new Player("Mike");
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
            string r = Core.thePlayer.getCurrentRoom().getName();
            if(Core.isNewRoom(r))
            {
                Core.discoverRoom(r);
                Debug.Log("discovered "+r);
            }
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(tryMove("north")) this.setupRoom();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tryMove("west")) this.setupRoom();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(tryMove("east")) this.setupRoom();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(tryMove("south")) this.setupRoom();
        }
    }
}
