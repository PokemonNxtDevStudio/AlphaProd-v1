using UnityEngine;
using System.Collections;

public struct SharedConstants  {
	public enum Category {
		player,
		playerProjectile,
		enemy,
		enemyProjectile
	}
	public enum AnimType {
		run,
		walk,
		idle,
		swim,
		attack1,
		attack2,
		attack3
	}
	public enum CharacterType {
		player,
		ai
	}
	public enum Movement {
		forward,
		back,
		up,
		down,
		left,
		right
	}
	public enum CollisionType {
		shop
	}
	public enum Commands {
		ReleasePokemon,
		CapturePokemon
	}
}
