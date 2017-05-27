using System;
using System.Collections;
using System.Collections.Generic;

public class Village {
	private List<Building> buildings = new List<Building>();

	public Village () {
	}

	public void AddBuilding (Building building) {
		buildings.Add(building);
		Logger.Info("add a building #{0}#", building);
	}

	public bool HasBuilding (BuildingType type) {
		return buildings.Exists(building => building.TypeOf(type));
	}

	public void LevelupBuilding (BuildingType type) {
		buildings.FindAll(b => b.TypeOf(type)).ForEach(building => building.Levelup());
		Logger.Info("levelup building #{0}#", type);
	}

	public void Tick () {
		buildings.ForEach(building => building.Tick());
	}
}
