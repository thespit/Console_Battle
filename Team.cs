using System;
using System.Collections;
using System.Collections.Generic;

public class Team {
	private List<Hero> heroes;

	public Team () {
		heroes = new List<Hero>();
	}

	public void AddHero (Hero hero) {
		heroes.Add(hero);
	}

	public Hero GetHeroByIndex (int index) {
		return heroes[index];
	}

	public int GetHeroSize () {
		return heroes.Count;
	}

	public void Remove (Hero hero) {
		heroes.Remove(hero);
	}

	public bool IsAllDead () {
		foreach (Hero hero in heroes) {
			if (!hero.IsDead()) {
				return false;
			}
		}
		return true;
	}

	public String ToStr () {
		string str = "";
		foreach (Hero hero in heroes) {
			str += hero.ToStr() + "\n";
		}
		return str;
	}
}