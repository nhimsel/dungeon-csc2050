using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doormanager : MonoBehaviour
{
    public GameObject NDoor;
    public GameObject SDoor;
    public GameObject WDoor;
    public GameObject EDoor;
    public bool NDoorEnabled = false;
    public bool SDoorEnabled = false;
    public bool EDoorEnabled = false;
    public bool WDoorEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        NDoor.SetActive(NDoorEnabled);
        SDoor.SetActive(SDoorEnabled);
        WDoor.SetActive(WDoorEnabled);
        EDoor.SetActive(EDoorEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        NDoor.SetActive(NDoorEnabled);
        SDoor.SetActive(SDoorEnabled);
        WDoor.SetActive(WDoorEnabled);
        EDoor.SetActive(EDoorEnabled);
    }

    public void Change(GameObject door)
    {
        if (door.Equals(NDoor))
        {
            NDoorEnabled = !NDoorEnabled;
        }
        else if (door.Equals(SDoor))
        {
            SDoorEnabled = !SDoorEnabled;
        }
        else if (door.Equals(EDoor))
        {
            EDoorEnabled = !EDoorEnabled;
        }
        else if (door.Equals(WDoor))
        {
            WDoorEnabled = !WDoorEnabled;
        }
    }
}
