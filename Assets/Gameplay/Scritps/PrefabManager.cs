using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager :MonoBehaviour
{

	// Use this for initialization
	public GameObject CharacterUI;
	public GameObject EnemyPrefab;
	public GameObject EnemyPrefab2;
	public GameObject EnemySpawnPoint;
	public GameObject EnemeySpawnPoint2;


	public static PrefabManager getInstance()
	{
		return Object.FindObjectOfType<PrefabManager> ();
	}
}
