using System;

public class Sawmill : Building {
	
	public Sawmill () {
		type = BuildingType.SAWMILL;
	}

	public override void Tick () {
		Global.wood += level;
	}
}