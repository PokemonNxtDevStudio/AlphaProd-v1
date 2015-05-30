using UnityEngine;
using System.Collections;

public class PokemonSpawner {
	//overload method
	public static GameObject SpawnPokemon(GameObject __pokemon, Transform __parent = null) {
		return SpawnPokemon (__pokemon, Vector3.zero, Quaternion.identity, __parent);
	}
	//spawns pokemon at desired location
	public static GameObject SpawnPokemon(GameObject __pokemon, Vector3 __pos, Quaternion __rot, Transform __parent  = null) {
		GameObject newPokemon = (GameObject) GameObject.Instantiate(__pokemon, __pos, __rot);

		//parenting
		if (__parent != null)
			newPokemon.transform.parent = __parent;

		return newPokemon;
	}
}
