using UnityEngine;
using System.Collections;
using Client;
using PokemonNXT.Controllers;
public class GameController : MonoBehaviour {

	// Use this for initialization

    Player player;
    public Transform spawnPoint;
    public ThirdPersonCameraCtrl Camera;
	void Start () {
        player = MMOManager.Instance.player;
        GameObject go = (GameObject)MMOManager.Instance.InstantiateObject(player.TrainerAssetID, player.ID, spawnPoint.position);
        Camera.target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
