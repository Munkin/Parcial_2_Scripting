using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcial2.Game
{

public class FabricaEnemigos : MonoBehaviour {

	public GameObject prefEnemigo;

	public void FabricarEnemigo(Vector3 posEnemy)
	{

		GameObject tempEnemy = Instantiate(prefEnemigo, posEnemy, Quaternion.identity);
		tempEnemy.GetComponent<Enemy>().SetRandomParameters();

	}
	
}

}
