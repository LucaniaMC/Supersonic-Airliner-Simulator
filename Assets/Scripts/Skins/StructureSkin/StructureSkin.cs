using UnityEngine;


//abstract class for all structure skins, include a main and shadow sprite
[System.Serializable]
public abstract class StructureSkin
{
    public abstract Sprite Main { get; }
    public abstract Sprite Shadow { get; }
}


//Different types of houses and airports
public enum HouseType { Default, Water, Desert, Snow }
public enum AirportType { Black, White, Orange}


//house skin class
[System.Serializable]
public class HouseSkin : StructureSkin
{
    public HouseType type;
    public Sprite mainSprite;
    public Sprite shadowSprite;

    public override Sprite Main => mainSprite;
    public override Sprite Shadow => shadowSprite;
}


//airport skin class
[System.Serializable]
public class AirportSkin : StructureSkin
{
    public AirportType type;
    public Sprite mainSprite;
    public Sprite shadowSprite;

    public override Sprite Main => mainSprite;
    public override Sprite Shadow => shadowSprite;
}
