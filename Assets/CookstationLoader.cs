using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookstationLoader : JsonLoader<AbstractCookingStation>
{
    public List<AbstractCookingStation> CookingStations { get; private set; }


    public CookstationLoader()
    {
        CookingStations = new List<AbstractCookingStation>();
        Init("Assets/CookingStations.json");
    }

    public override void Init(string filePath)
    {
        base.Init(filePath);
        CookingStations = GetObjectListFromFilePathByString("Stations");
    }

    public AbstractCookingStation GetCookingStationById(int nameOfCookingStationToFind)
    {

        AbstractCookingStation StationToReturn = null;
        for (int i = 0; i < CookingStations.Count; i++)
        {
            if (nameOfCookingStationToFind == CookingStations[i].ID)
            {
                StationToReturn = new AbstractCookingStation();
                StationToReturn.TurnOrder = 1;
                StationToReturn.cariedObjects = new List<iCaryable>();
                StationToReturn.characterArt = SpriteHolder.instance.GetBuildingArtFromIDNumber(CookingStations[i].ID);
                StationToReturn.Name = CookingStations[i].Name;
                StationToReturn.EntityType = EnumHolder.EntityType.CookingStation;
                StationToReturn.numberOfCarriedObjects = CookingStations[i].numberOfCarriedObjects;
            }
        }
        return StationToReturn;
    }
}
