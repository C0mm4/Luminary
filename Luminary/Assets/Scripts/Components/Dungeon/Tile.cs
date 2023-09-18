using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    public int types;
    public int index;

    [SerializeField]
    public DunRoom targetRoom;

    public int tildID;

    public int x, y;

}
