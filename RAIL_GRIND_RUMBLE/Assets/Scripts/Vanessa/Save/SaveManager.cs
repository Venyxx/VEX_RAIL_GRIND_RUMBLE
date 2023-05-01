using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get;}
    public SaveState state;

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
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //Load last state-------------
    public void Load ()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            Debug.Log("found save file");
        } else
        {
            state = new SaveState();
            Save();
            Debug.Log("no prev save, making file");
        }
    }


    //IS IT OWNED------------------------------------------------------------------------
    //Check if item wallpaper color is owned
    public bool IsAriMatOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariMaterialOwned & (1 << index)) != 0;
    }

    //Check if item wallpaper color is owned
    public bool IsGraffitiOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariGraffitiOwned & (1 << index)) != 0;
    }
    public bool IsAriHairOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariHairOwned & (1 << index)) != 0;
    }

    public bool IsAriAccessoryOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariAccessoryOwned & (1 << index)) != 0;
    }
    public bool IsAriTopOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariTopOwned & (1 << index)) != 0;
    }

    public bool IsAriBottomOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariBottomOwned & (1 << index)) != 0;
    }

    public bool IsAriSockOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariSockOwned & (1 << index)) != 0;
    }
    public bool IsAriSkateOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariSkateOwned & (1 << index)) != 0;
    }

    public bool IsAriMaskOwned(int index)
    {
        //check if bit is set, if yes its owned
        return (state.ariMaskOwned & (1 << index)) != 0;
    }





    //PURCHASING UPGRADES---------------------------------------------------------------
    public bool BuyAriMaterial( int index, int cost)
    {
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        if (state.Money >= cost)
        {
            //enough cash
            state.Money  -= cost;
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
        state.ariMaterialOwned |= 1 << index;
    }

     //unlock a theme way
    public void UnlockGraffiti (int index)
    {
        state.ariGraffitiOwned |= 1 << index;
    }

    //unlock a hair way
    public void UnlockAriHair (int index)
    {
        state.ariHairOwned |= 1 << index;
    }

    public void UnlockAriAccessory (int index)
    {
        state.ariAccessoryOwned |= 1 << index;
    }

    public void UnlockAriTop (int index)
    {
        state.ariTopOwned |= 1 << index;
    }

    public void UnlockAriBottom (int index)
    {
        state.ariBottomOwned |= 1 << index;
    }

    public void UnlockAriSock (int index)
    {
        state.ariSockOwned |= 1 << index;
    }

    public void UnlockAriSkate (int index)
    {
        state.ariSkateOwned |= 1 << index;
    }

    public void UnlockAriMask (int index)
    {
        state.ariMaskOwned |= 1 << index;
    }



    //LEVEL COMPLETION ------------------------------------------------------------------
    public void CompleteLevel (int index)
    {
        Debug.Log("tried to run complete level");
        //if current lvl 
        if (state.completedLevel == index)
        {
            state.completedLevel++;
            Debug.Log("level completed max is: " + state.completedLevel);
            Save();
        }
    }

    //reset the save
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
        Application.Quit();
        Debug.Log("reset!");
    }
}


