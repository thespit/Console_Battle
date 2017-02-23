using System;
using System.Collections;
using System.Collections.Generic;

public class Hero {
	public string name;
	public int hp;

	private Dictionary<AttributeType, Attribute> attributes;
	private Dictionary<AbilityType, Ability> abilities;

	public Weapon weapon = Weapon.EMPTY;

	public Hero (string name) {
		this.name = name;
		attributes = new Dictionary<AttributeType, Attribute>();
		abilities = new Dictionary<AbilityType, Ability>();
	}

	public bool IsDead () {
		return hp <= 0;
	}

	public int GetAttribute (AttributeType type) {
		if (attributes.ContainsKey(type)) {
			return attributes[type].value;
		} else {
			return 0;
		}
	}

	public void RefreshAbilityByAttribute () {
		SetAbility(AbilityType.ATK, (int) (GetAttribute(AttributeType.STR) * 0.5));
		SetAbility(AbilityType.DEF, 0);
		SetAbility(AbilityType.HIT, 10 + (int) (GetAttribute(AttributeType.PER) * 0.5));
		SetAbility(AbilityType.DODGE, (int) (GetAttribute(AttributeType.AGI) * 0.5));
		SetAbility(AbilityType.SPD, 10);
		SetAbility(AbilityType.HP, 10 + (int) (GetAttribute(AttributeType.END)));
	}

	public void SetAttribute (AttributeType type, int value) {
		if (attributes.ContainsKey(type)) {
			attributes[type].value = value;
		} else {
			attributes[type] = new Attribute(type, value);
		}
	}

	public int GetAbility (AbilityType type) {
		if (!abilities.ContainsKey(type)) {
			return 0;
		}
		return abilities[type].value;
	}

	public void SetAbility (AbilityType type, int value) {
		abilities[type] = new Ability(type, value);
	}

	public bool HasWeapon () {
		return !weapon.Equals(Weapon.EMPTY);
	}

	public bool hit (Hero enemy) {
		int tmp = GetAbility(AbilityType.HIT) + weapon.GetAbility(WAbilityType.HIT) - enemy.GetAbility(AbilityType.DODGE);
		return Utils.RandomInt(1, 20) < tmp;
	}

	public int dmg (Hero enemy) {
		return Utils.RandomInt(weapon.GetAbility(WAbilityType.ATK_MIN), weapon.GetAbility(WAbilityType.ATK_MAX));
	}

	public string ToAtkStr () {
		string str = "";
		str += name + " ";
		str += "(";
		str += "hit:" + GetAbility(AbilityType.HIT);
		str += " atk:" + GetAbility(AbilityType.ATK);
		str += " dmg:" + weapon.ToAtkStr();
		str += ")";
		return str;
	}

	public string ToDefStr () {
		string str = "";
		str += name + " ";
		str += "(";
		str += "hp:" + hp;
		str += " dodge:" + GetAbility(AbilityType.DODGE);
		str += " def:" + GetAbility(AbilityType.DEF);
		str += ")";
		return str;
	}

	public string ToStr () {
		string str = "";
		str += name;

		// name part
		str += "\t\t\t\t" + weapon.name;
		str += "\n";

		foreach (KeyValuePair<AttributeType, Attribute> pair in attributes) {
			str += pair.Key + "\t" + pair.Value.value;
			if (pair.Key.Equals(AttributeType.STR)) {
				str += "\t\t\t" + weapon.ToAtkStr();
			}

			if (pair.Key.Equals(AttributeType.END)) {
				str += "\t\t\t";
				str += "No Equip";
			}

			str += "\n";
		}
		str += "\n";

		foreach (KeyValuePair<AbilityType, Ability> pair in abilities) {
			str += pair.Key + "\t" + pair.Value.value + "\n";
		}
		return str;
	}
}