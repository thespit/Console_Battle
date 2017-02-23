using System;
using System.Collections.Generic;

public class Weapon {
	public static readonly Weapon EMPTY = new Weapon();
	static Weapon() {
		EMPTY.name = "Hand";
		EMPTY.SetAbility(WAbilityType.ATK_MIN, 1);
		EMPTY.SetAbility(WAbilityType.ATK_MAX, 2);
		EMPTY.SetAbility(WAbilityType.ATK_ADD, 0);
	}

	public string name;
	private Dictionary<WAbilityType, int> abilities;

	public Weapon () {
		abilities = new Dictionary<WAbilityType, int>();
	}

	public void SetAbility (WAbilityType type, int value) {
		abilities[type] = value;
	}

	public int GetAbility (WAbilityType type) {
		if (!abilities.ContainsKey(type)) {
			return 0;
		}
		return abilities[type];
	}

	public string ToAtkStr () {
		string str = "";
		str += GetAbility(WAbilityType.ATK_MIN) + "-" + GetAbility(WAbilityType.ATK_MAX);
		if (GetAbility(WAbilityType.ATK_ADD) != 0) {
			str += " +" + GetAbility(WAbilityType.ATK_ADD);
		}
		return str;
	}
}