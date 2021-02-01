using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class WinSceneScript : MonoBehaviour
{

    public Text TimeTaken;
    public Text ItemsCollected;

    private void Awake()
    {
        TimeTaken.text = TimeTaken.text.Replace("TIMETAKEN", Global.TimeTaken.ToString());
        ItemsCollected.text = ItemsCollected.text.Replace("X", Global.ItemsCollected.ToString()).Replace("Y", Global.TotalItems.ToString());
    }

    private void Update()
    {
        if (InputManager.PlayerInput.JumpPressed)
        {
            Application.Quit();
        }
    }
}
