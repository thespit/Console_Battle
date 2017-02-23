using System.Collections.Generic;

public class WeaponList {
	public List<Weapon> weapons = new List<Weapon>();

	public WeaponList () {
		Weapon knife = new Weapon();
		knife.name = "Knife";
		knife.SetAbility(WAbilityType.ATK_MIN, 3);
		knife.SetAbility(WAbilityType.ATK_MAX, 6);
		weapons.Add(knife);

		Weapon axe = new Weapon();
		axe.name = "Axe";
		axe.SetAbility(WAbilityType.ATK_MIN, 1);
		axe.SetAbility(WAbilityType.ATK_MAX, 10);
		weapons.Add(axe);

		Weapon katana = new Weapon();
		katana.name = "Katana";
		katana.SetAbility(WAbilityType.ATK_MIN, 1);
		katana.SetAbility(WAbilityType.ATK_MAX, 6);
		katana.SetAbility(WAbilityType.ATK_ADD, 1);
		weapons.Add(katana);

		Weapon sword = new Weapon();
		sword.name = "Sword";
		sword.SetAbility(WAbilityType.ATK_MIN, 2);
		sword.SetAbility(WAbilityType.ATK_MAX, 8);
		weapons.Add(sword);
	}

	public Weapon RandomOne () {
		int index = Utils.RandomInt(0, weapons.Count - 1);
		return weapons[index];
	}
}