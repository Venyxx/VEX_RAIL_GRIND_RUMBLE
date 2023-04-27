using UnityEngine;
public class SaveState 
{
    public float Money = 9999; /*temp for presentation*/
    public int MaxLevel = 0;
    public int ariMaterialOwned = 0;
    public int ariGraffitiOwned = 0;
    public int ariHairOwned = 0;
    public int ariAccessoryOwned = 0;
    public int ariTopOwned = 0;
    public int ariBottomOwned = 0;
    public int ariSockOwned = 0;
    public int ariSkateOwned = 0;
    public int ariMaskOwned = 0;

    public Vector3 playerLocation = new Vector3 (0,0,0);

    public int activeAriMaterial = 0;
    public int activeAriHair = 0;
    public int activeAriAccessory = 0;
    public int activeAriTop = 0;
    public int activeAriBottom = 0;
    public int activeAriSock = 0;
    public int activeAriSkate = 0;
    public int activeAriMask = 0;

    public string ariGraffitiSlotUp1 = "Decal_1";
    public string ariGraffitiSlotRight2 = "Decal_2";
    public string ariGraffitiSlotDown3 = "Decal_3";
    public string ariGraffitiSlotLeft4 = "Decal_4";

    
    //public int activeSkate = 0;

   // public int Health = 100;
    //public int Armor = 100;
    //public int TopSpeed = 40;
    
    //public int LightAttackIncrease = 0;
    //public int HeavyAttackIncrease = 0;

    //public int GrappleChargesTimePlusMinus = -5;
    //public int GrappleCharges = 3;


    public int completedLevel = 0;
    public float endlessHighScore = 0;
}
