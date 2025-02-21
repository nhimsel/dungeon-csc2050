using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doormanager : MonoBehaviour
{
    public bool NDoorStart;
    public bool SDoorStart;
    public bool WDoorStart;
    public bool EDoorStart;
    
    public GameObject NDoor;
    public GameObject SDoor;
    public GameObject WDoor;
    public GameObject EDoor;
    // Start is called before the first frame update
    void Start()
    {
        Core.NDoorEnabled = this.NDoorStart;
        Core.SDoorEnabled = this.SDoorStart;
        Core.WDoorEnabled = this.WDoorStart;
        Core.EDoorEnabled = this.EDoorStart;

        NDoor.SetActive(Core.NDoorEnabled);
        SDoor.SetActive(Core.SDoorEnabled);
        WDoor.SetActive(Core.WDoorEnabled);
        EDoor.SetActive(Core.EDoorEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        NDoor.SetActive(Core.NDoorEnabled);
        SDoor.SetActive(Core.SDoorEnabled);
        WDoor.SetActive(Core.WDoorEnabled);
        EDoor.SetActive(Core.EDoorEnabled);
    }

    public void Change(GameObject door)
    {
        if (door.Equals(NDoor))
        {
            Core.NDoorEnabled = !Core.NDoorEnabled;
        }
        else if (door.Equals(SDoor))
        {
            Core.SDoorEnabled = !Core.SDoorEnabled;
        }
        else if (door.Equals(EDoor))
        {
            Core.EDoorEnabled = !Core.EDoorEnabled;
        }
        else if (door.Equals(WDoor))
        {
            Core.WDoorEnabled = !Core.WDoorEnabled;
        }
    }
}
