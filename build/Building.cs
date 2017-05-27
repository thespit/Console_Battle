public class Building {
	public int level = 1;
	public BuildingType type;

	public Building () {}

	public void Levelup () {
		level++;
	}

	public bool TypeOf (BuildingType value) {
		return type.Equals(value);
	}

	public virtual void Tick () {}

	public override string ToString () {
		return type + " level:" + level;
	}
}
