//Attach this to an airport object
public class LoadAirportSkin : LoadStructureSkin<AirportSkin, AirportType>
{
    protected override bool Matches(AirportSkin skin, AirportType type)
    {
        return skin.type == type;
    }
}
