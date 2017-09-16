using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcial2.Game
{
public class ControladorRonda : MonoBehaviour {

	public float tiempoSiguienteRonda;

	public SpawnController miSpawner;

	private float tiempoPasado;
	
	// Update is called once per frame
	void Update () {
		if(miSpawner.IsRoundEnd())
		{
			if(tiempoPasado<tiempoSiguienteRonda)
			tiempoPasado += Time.deltaTime;
			else
			{
				miSpawner.SetRound();
				tiempoPasado = 0;
			}
		}
	}
}

}
