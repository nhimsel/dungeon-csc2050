using System;
using UnityEngine;

public class fightManager : MonoBehaviour
{
    void Start()
    {
        Fight f = new Fight();
        f.Begin();
    }
}