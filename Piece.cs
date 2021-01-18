using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    none = -1,
    smallPlant = 0,
    rock = 2,
    cortez = 1,
    smallCortez = 3,
    sandRock = 4,
}

public class Piece : MonoBehaviour 
{
    public PieceType type;
    public int visualIndex;

}
