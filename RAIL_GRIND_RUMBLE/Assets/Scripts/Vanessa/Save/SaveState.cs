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
    public int ariGraffitiSlotRight2 = 1;
    public int ariGraffitiSlotDown3 = 2;
    public int ariGraffitiSlotLeft4 = 3;

    
    public int activeSkate = 0;

    public int Health = 100;
    public int Armor = 100;
    public int TopSpeed = 40;
    
    public int LightAttackIncrease = 0;
    public int HeavyAttackIncrease = 0;

    public int GrappleChargesTimePlusMinus = -5;
    public int GrappleCharges = 3;


    public int completedLevel = 0;
    public float endlessHighScore = 0;
}
