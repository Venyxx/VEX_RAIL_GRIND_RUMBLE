using UnityEngine;
public class SaveState 
{
    public float Money = 100;
    public int MaxLevel = 0;
    public int ariMaterialOwned = 0;
    public int ariGraffitiOwned = 0;
    public int ariHairOwned = 0;

    public Vector3 playerLocation = new Vector3 (0,0,0);

    public int activeAriMaterial = 0;
    public int activeAriHair = 0;
    public int ariGraffitiSlotUp1 = 0;
    public int ariGraffitiSlotRight2 = 0;
    public int ariGraffitiSlotDown3 = 0;
    public int ariGraffitiSlotLeft4 = 0;



    public int Health = 0;
    public int Armor = 0;
    public int TopSpeed = 0;
    
    public int LightAttackIncrease = 0;
    public int HeavyAttackIncrease = 0;

    public int GrappleChargesTimeDecrease = 0;
    public int GrappleCharges = 0;


    public int completedLevel = 0;
    public float endlessHighScore = 0;
}
