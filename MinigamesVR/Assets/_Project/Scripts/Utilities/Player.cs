using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private string playerName;

    public string GetPlayerName() => playerName;
    
    public void MovePlayerTo(Vector3 position)
    {
        transform.position = position;
    }
}
