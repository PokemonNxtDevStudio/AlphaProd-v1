using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// </summary>
public class AISpawner : MonoBehaviour
{
    public float[] spawnProbabilty;
    public GameObject[] enemyPokemonToSpawn;
    public int maximumSpawnerCount = 5;
    public float radius = 5f;
    public bool showGizmo = true;
    public Color areaColor;
    private float randomAngle;
    private Vector3 randomSpawnVector;
    private DiscreteDistribution discreteDistribution;

    //public List<GameObject> spawned = new List<GameObject> ();
    // GlobalGameManager _globalGameManager;

    private void Start()
    {
        discreteDistribution = new DiscreteDistribution(spawnProbabilty);
        StartCoroutine(SpawnPokemon());
    }

    private IEnumerator SpawnPokemon()
    {
        for (int i = 0; i < maximumSpawnerCount; i++)
        {
            GameObject prefab = (GameObject) enemyPokemonToSpawn[discreteDistribution.Sample()];
			GameObject newPokemon = PokemonSpawner.SpawnPokemon(prefab, RandomPostion(), Quaternion.identity);
			
			// newPokemon.AddComponent<PokeCore>().pokemon = Temp.PopulateDB.instance.getBasicPokemon(1);
            //newPokemon.GetComponent<PokeCore>().pokeCoreType = PokeCoreType.WildPokemon;
            //RaycastHit hit;
            //if (Physics.Raycast (newPokemon.transform.position, Vector3.down, out hit)) {
            //newPokemon.transform.position = hit.point;
            //}
            Debug.Log("PokemonSpawned: " + newPokemon.name);
            //_globalGameManager.pokemonSpawned.Add (newPokemon);
            yield return 0;
        }
    }

    private Vector3 RandomPostion()
    {
        randomAngle = Random.Range(0f, 80);
        randomSpawnVector.x = Mathf.Sin(randomAngle)*radius + transform.position.x;
        randomSpawnVector.z = Mathf.Cos(randomAngle)*radius + transform.position.z;
        randomSpawnVector.y = transform.position.y;

        return randomSpawnVector;
    }

    private IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return 0;
    }

    private void Update()
    {
        //start coroutine for checking if less then max spawn and creating one
    }

    //editor utilities Should be moved to utilities later.
    private void OnDrawGizmosSelected()
    {
        if (!showGizmo)
        {
            Gizmos.color = new Color(0.0f, 0.5f, 0.5f, 0.3f);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = areaColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}