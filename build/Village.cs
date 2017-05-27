using System;
using System.Collections;
using System.Collections.Generic;

public class Village {
	private List<Building> buildings = new List<Building>();

	public Village () {
	}

	public void AddBuilding (Building building) {
		Console.WriteLine("add a building " + building.type);
		buildings.Add(building);
	}

	public bool HasBuilding (BuildingType type) {
		foreach (Building building in buildings) {
			if (building.type == type) {
				return true;
			}
		}
		return false;
	}

	public void levelupBuilding (BuildingType type) {
		Console.WriteLine("levelup a building " + type);
		foreach (Building building in buildings) {
			if (building.type.Equals(type)) {
				building.level += 1;
			}
		}
	}

	public void Tick () {
		foreach (Building building in buildings) {
			building.Tick();
		}
	}
}
