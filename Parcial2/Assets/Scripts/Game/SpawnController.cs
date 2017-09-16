using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcial2.Game
{

public class SpawnController : MonoBehaviour {

	public FabricaEnemigos[] fabricas;

	public float tiempoParaSpawn;

	public float posX, posZ;

	public int numeroEnemigosRonda;

	private float tiempoPasado;

	private int enemyCounter;	


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyCounter<=0)
		return;

		if(tiempoPasado<tiempoParaSpawn)
		{
			tiempoPasado += Time.deltaTime;
		}
		else
		{
			enemyCounter--;
			tiempoPasado = 0;
			CrearEnemigo();
		}
	}

	void CrearEnemigo ()
	{
		int tempInt = Random.Range(0, fabricas.Length);
		float x = Random.Range(-posX, posX);
		float z = Random.Range(-posZ, posZ);
		Vector3 posEnemy = new Vector3(x,0f, z);
		fabricas[tempInt].FabricarEnemigo(posEnemy);
	}

	public void SetRound()
	{
		if(enemyCounter <= 0)
		enemyCounter = numeroEnemigosRonda;
	}

	public bool IsRoundEnd()
	{
		return enemyCounter<=0;
	}

}
}
