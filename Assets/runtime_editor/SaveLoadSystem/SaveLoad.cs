using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            DataManager.Instance.SavePlayer();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DataManager.Instance.LoadPlayer();
        }
    }

}
