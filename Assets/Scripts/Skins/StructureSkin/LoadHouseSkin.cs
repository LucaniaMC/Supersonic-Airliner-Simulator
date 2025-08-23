//Attach this to a house object
public class LoadHouseSkin : LoadStructureSkin<HouseSkin, HouseType>
{
    protected override bool Matches(HouseSkin skin, HouseType type)
    {
        return skin.type == type;
    }
}
