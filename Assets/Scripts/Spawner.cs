using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Spawner : MonoBehaviour {



	// public GameObject[] pieces;

    public GameObject block;



	// Update is called once per frame

	void Update () {

		if (!GameManager.instance.pieceFalling)

		{

			GameManager.instance.pieceFalling = true;

			// GameObject piece = pieces[Random.Range(0, pieces.Length)];

			// Instantiate(piece, transform.position, Quaternion.identity);

            Instantiate(block, transform.position, Quaternion.identity);

		}

	}

}