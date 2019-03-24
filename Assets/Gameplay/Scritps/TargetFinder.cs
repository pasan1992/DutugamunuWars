using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour 
{
	private List<EnemyAttackSystem> enemyList = new List<EnemyAttackSystem>();
	private PlayerAttackSystem m_playerAttackSystem;

	//temp
	int enemyCount =0;

	public Transform getCurrentTarget()
    {
		Transform currentTarge =null;
		float distance = 999;
		foreach (EnemyAttackSystem enemy in enemyList) 
		{
			if (distance > Vector3.Distance (this.transform.position, enemy.transform.position) && enemy.isFunctional()) 
			{
				currentTarge = enemy.transform;
				distance = Vector3.Distance (this.transform.position, enemy.transform.position);
			}
		}
		return currentTarge;
    }

	public void addToEnemyList(EnemyAttackSystem enemy)
	{
		enemyList.Add (enemy);
	}

	public void removeFromEnemyList(EnemyAttackSystem enemy)
	{
		enemyList.Remove (enemy);
		enemyCount++;
		if (enemyList.Count == 0) 
		{
			PlayerAttackSystem player =	this.GetComponentInParent<PlayerAttackSystem> ();

			//Temporaray Code
			GameObject tempEnemy = null;
			GameObject tempEmemy2 = null;
			PrefabManager manager = PrefabManager.getInstance ();
			if (enemyCount % 2 == 0) 
			{
				tempEnemy = GameObject.Instantiate(manager.EnemyPrefab,null);
				//tempEmemy2 =  GameObject.Instantiate(manager.EnemyPrefab2,null);
			} 
			else 
			{
				tempEnemy = GameObject.Instantiate(manager.EnemyPrefab2,null);
				//tempEmemy2 =  GameObject.Instantiate(manager.EnemyPrefab,null);
			}
			//player.putAwayWeapon ();

			tempEnemy.GetComponent<EnemyAIController> ().enemyTarget = player;
			tempEnemy.GetComponent<EnemyAIController> ().enabled = true;
			tempEnemy.transform.position = manager.EnemySpawnPoint.transform.position;

			// tempEmemy2.GetComponent<EnemyAIController> ().m_Player = player;
			// tempEmemy2.GetComponent<EnemyAIController> ().enabled = true;
			// tempEmemy2.transform.position = manager.EnemeySpawnPoint2.transform.position;
		}
	}

	public List<EnemyAttackSystem> getEnemyList()
	{
		return enemyList;
	}
}
