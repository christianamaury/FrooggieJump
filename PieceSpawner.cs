using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{

    public PieceType type;
    public Piece currentPiece;

    public void Spawn()
    {
        //Get me a new piece of the pool..
        currentPiece = LevelManager.Instance.GetPiece(type, 0);


        currentPiece.gameObject.SetActive(true);

        //We don't wanna mantain the world position..
        currentPiece.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
	
}
