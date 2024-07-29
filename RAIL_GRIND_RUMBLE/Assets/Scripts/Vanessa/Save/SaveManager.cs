using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get;}
    public SaveState saveState;

    private void Awake ()
    {
        //ResetSave();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Load();

        
    }

    //save whole state-------------
    public void Save ()
    {
        PlayerPrefs.SetString("save", SaveSerializer.Serialize<SaveState>(saveState));
    }

    //Load last state-------------
    public void Load ()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            saveState = SaveSerializer.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            Debug.Log("found save file");
        } else
        {
            saveState = new SaveState();
            Save();
            Debug.Log("no prev save, making file");
        }
    }


    //IS IT OWNED------------------------------------------------------------------------
    //Check if item wallpaper color is owned
    public bool IsAriMatOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariMaterialOwned & (1 << index)) != 0;
    }

    //Check if item wallpaper color is owned
    public bool IsGraffitiOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariGraffitiOwned & (1 << index)) != 0;
    }
    public bool IsAriHairOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariHairOwned & (1 << index)) != 0;
    }

    public bool IsAriAccessoryOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariAccessoryOwned & (1 << index)) != 0;
    }
    public bool IsAriTopOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariTopOwned & (1 << index)) != 0;
    }

    public bool IsAriBottomOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariBottomOwned & (1 << index)) != 0;
    }

    public bool IsAriSockOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariSockOwned & (1 << index)) != 0;
    }
    public bool IsAriSkateOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariSkateOwned & (1 << index)) != 0;
    }

    public bool IsAriMaskOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (saveState.ariMaskOwned & (1 << index)) != 0;
    }





    //PURCHASING UPGRADES---------------------------------------------------------------
    public bool BuyAriMaterial( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriMaterial(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }

    //try to buy theme
    public bool BuyGraffiti( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockGraffiti(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }

    public bool BuyAriHair( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriHair(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }
     public bool BuyAriAccessory( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriAccessory(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }

     public bool BuyAriTop( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriTop(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }

     public bool BuyAriBottom( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriBottom(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }

     public bool BuyAriSock( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriSock(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }
     public bool BuyAriSkate( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriSkate(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }

    public bool BuyAriMask( int index, int cost)
    {
        if (saveState.Money >= cost)
        {
            //enough cash
            saveState.Money  -= cost;
            UnlockAriMask(index);

            //save
            Save();
            return true;
        }
        else 
        {
            //broke 
            return false;
        }
    }




    //UNLOCKING------------------------------------------------------------------------
    public void UnlockAriMaterial (int index)
    {
        saveState.ariMaterialOwned |= 1 << index;
    }

     //unlock a theme way
    public void UnlockGraffiti (int index)
    {
        saveState.ariGraffitiOwned |= 1 << index;
    }

    //unlock a hair way
    public void UnlockAriHair (int index)
    {
        saveState.ariHairOwned |= 1 << index;
    }

    public void UnlockAriAccessory (int index)
    {
        saveState.ariAccessoryOwned |= 1 << index;
    }

    public void UnlockAriTop (int index)
    {
        saveState.ariTopOwned |= 1 << index;
    }

    public void UnlockAriBottom (int index)
    {
        saveState.ariBottomOwned |= 1 << index;
    }

    public void UnlockAriSock (int index)
    {
        saveState.ariSockOwned |= 1 << index;
    }

    public void UnlockAriSkate (int index)
    {
        saveState.ariSkateOwned |= 1 << index;
    }

    public void UnlockAriMask (int index)
    {
        saveState.ariMaskOwned |= 1 << index;
    }



    //TODO: Commented Out Progression Code
    /*//LEVEL COMPLETION ------------------------------------------------------------------
    public void CompleteLevel (int index)
    {
        Debug.Log("tried to run complete level");
        //if current lvl 
        if (saveState.completedLevel == index)
        {
            saveState.completedLevel++;
            Debug.Log("level completed max is: " + saveState.completedLevel);
            Save();
        }
    }*/

    //reset the save
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
        Application.Quit();
        Debug.Log("reset!");
    }
}


