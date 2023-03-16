using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationiManager : MonoBehaviour
{

    public bool misControlEnabled = true; //stop overlapping actions - Raul

    public void EnableControl()
    {
        misControlEnabled = true;
        
    }

    public void DisableControl()
    {
        misControlEnabled = false;
    }
}
