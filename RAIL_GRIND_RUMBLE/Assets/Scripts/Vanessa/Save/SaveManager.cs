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
        DontDestroyOnLoad(gameObject);
        Instance = this;
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
        Debug.Log("reset!");
    }
}


