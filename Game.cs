using System;
using System.Collections;
using System.Collections.Generic;

public class Game {
	static Team team = new Team();
	static Team enemyTeam = null;
	static Hero atker = null;
	static Hero defer = null;
	static Hero hero = null;
		
	public static void Main (String[] args) {
		MenuState state = MenuState.VILLAGE;
		while (true) {
			string selection = OutputMenu(state);
			try {
				if (state == MenuState.VILLAGE) {
					if (selection == "1") {
						hero = RandomHero (RandomName());
						output(hero.ToStr());
						state = MenuState.RANDOM_HERO;
					} else if (selection == "2") {
						output(team.ToStr());
					} else if (selection == "3") {
						enemyTeam = RandomEnemyTeam();
						output(enemyTeam.ToStr());
					} else if (selection == "4") {
						output("battle");
						state = MenuState.BATTLE;
					} else if (selection == "0") {
						output("Bye bye.");
						break;
					}
				} else if (state == MenuState.RANDOM_HERO) {
					if (selection == "1") {
						hero = RandomHero (RandomName());
						output(hero.ToStr());
					} else if (selection == "2") {
						team.AddHero(hero);
						state = MenuState.VILLAGE;
					} else if (selection == "3") {
						state = MenuState.VILLAGE;
					}
				} else if (state == MenuState.BATTLE) {
					if (team.IsAllDead()) {
						output("you lose.");
						state = MenuState.VILLAGE;
					} else if (enemyTeam.IsAllDead()) {
						output("you win.");
						state = MenuState.VILLAGE;
					} else if (selection == "1") {
						state = MenuState.BATTLE_ATTACK;
					}
				} else if (state == MenuState.BATTLE_ATTACK) {
					atker = team.GetHeroByIndex(int.Parse(selection) - 1);
					state = MenuState.BATTLE_ATTACK_TARGET;
				} else if (state == MenuState.BATTLE_ATTACK_TARGET) {
					defer = enemyTeam.GetHeroByIndex(int.Parse(selection) - 1);
					List<string> result = Fight(atker, defer);
					OutputList(result);

					OutputBattleState(team, enemyTeam);
					state = MenuState.BATTLE;
				}
			} catch {
				Console.WriteLine("error.");
			}
		}
	}

	public static List<string> Fight (Hero hero1, Hero hero2) {
		List<string> result = new List<string>();

		Hero atker = null;
		Hero defer = null;
		if (hero1.GetAbility(AbilityType.SPD) >= hero2.GetAbility(AbilityType.SPD)) {
			atker = hero1;
			defer = hero2;
		} else {
			atker = hero2;
			defer = hero1;
		}

		if (atker.hit(defer)) {
			result.Add(atker.name + " hit " + defer.name);
			int dmg = atker.dmg(defer);
			defer.hp -= dmg;
			result.Add("dmg " + dmg);
			if (defer.IsDead()) {
				result.Add(defer.name + " is dead.");
			}
		} else {
			result.Add(atker.name + " miss " + defer.name);
		}

		if (defer.IsDead()) {
			enemyTeam.Remove(defer);
			return result;
		}

		if (defer.hit(atker)) {
			result.Add(defer.name + " hit " + atker.name);
			int dmg = defer.dmg(atker);
			atker.hp -= dmg;
			result.Add("dmg " + dmg);
			if (atker.IsDead()) {
				result.Add(atker.name + " is dead.");
			}
		} else {
			result.Add(defer.name + " miss " + atker.name);
		}

		if (defer.IsDead()) {
			enemyTeam.Remove(defer);
		}
		if (atker.IsDead()) {
			team.Remove(atker);
		}
		return result;
	}

	public static void output (string str) {
		Console.WriteLine("> " + str);
	}

	public static void OutputList (List<string> strs) {
		foreach (string str in strs) {
			output(str);
		}
	}

	public static string Input (string str) {
		Console.WriteLine("\nMenu:\n  " + str);
		Console.Write("> ");
		string result = Console.ReadLine();
		Console.Clear();
		// Console.WriteLine("\n");
		return result;
	}

	public static string RandomName () {
		int size = Utils.RandomInt(3, 5);
		string name = "";
		for (int i = 0; i < size; ++i) {
			int code = Utils.RandomInt(0, 51);
			char codeStr = (char) ('A' + code);
			name += codeStr;
		}
		return name;
	}

	public static string OutputMenu (MenuState state) {
		if (state == MenuState.VILLAGE) {
			return Input("1: random hero"
			+ "\n  2: print team"
			+ "\n  3: random enemy team"
			+ "\n  4: battle"
			+ "\n  0: quit");
		} else if (state == MenuState.RANDOM_HERO) {
			return Input("1: rerandom hero"
				+ "\n  2: pick hero"
				+ "\n  3: back");
		} else if (state == MenuState.BATTLE) {
			return Input("1: attack");
		} else if (state == MenuState.BATTLE_ATTACK) {
			string str = "select attacker:\n";
			for (int i = 1; i <= team.GetHeroSize(); ++i) {
				Hero hero = team.GetHeroByIndex(i - 1);
				str += i + ":" + hero.ToAtkStr() + "\n";
			}
			return Input(str);
		} else if (state == MenuState.BATTLE_ATTACK_TARGET) {
			string str = "select target:\n";
			for (int i = 1; i <= enemyTeam.GetHeroSize(); ++i) {
				Hero hero = enemyTeam.GetHeroByIndex(i - 1);
				str += i + ":" + hero.ToDefStr() + "\n";
			}
			return Input(str);
		}
		return "0";
	}

	public static Hero RandomHero (string name) {
		Hero hero = new Hero(name);
		hero.SetAttribute(AttributeType.STR, Utils.RandomInt(1, 10));
		hero.SetAttribute(AttributeType.AGI, Utils.RandomInt(1, 10));
		hero.SetAttribute(AttributeType.END, Utils.RandomInt(1, 10));
		hero.SetAttribute(AttributeType.PER, Utils.RandomInt(1, 10));

		hero.RefreshAbilityByAttribute();

		if (Utils.Hit(2500)) {
			hero.weapon = new WeaponList().RandomOne();
		}
		hero.hp = hero.GetAbility(AbilityType.HP);

		return hero;
	}

	public static Team RandomEnemyTeam () {
		Team team = new Team();
		for (int i = 0; i < 3; ++i) {
			team.AddHero(RandomHero(RandomName()));
		}
		return team;
	}

	public static void OutputBattleState (Team team1, Team team2) {
		string str = "\t";
		for (int i = 0; i < team1.GetHeroSize(); ++i) {
			str += team1.GetHeroByIndex(i).name + "\t";
		}
		str += "\n\t";
		for (int i = 0; i < team1.GetHeroSize(); ++i) {
			str += team1.GetHeroByIndex(i).hp + "\t";
		}
		str += "\n\n\t";
		for (int i = 0; i < team2.GetHeroSize(); ++i) {
			str += team2.GetHeroByIndex(i).name + "\t";
		}
		str += "\n\t";
		for (int i = 0; i < team2.GetHeroSize(); ++i) {
			str += team2.GetHeroByIndex(i).hp + "\t";
		}
		str += "\n";

		output(str);
	}
}