using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{
    DiceRoller theDiceRoller;
    Tile currentTile;
    public Tile StartingTile;

    bool scoreMe = false;
    
    Vector3 targetPos;
    Vector3 vel = Vector3.zero;
    float smoothTime = 0.25f;
    float smoothDist = 0.01f;
    float maxSpeed = 3f ;
    float smoothHt = 1.0f;

    Tile[] moveQueue;
    int moveQueueIndex = 9999;


    // Start is called before the first frame update
    void Start()
    {
        theDiceRoller = GameObject.FindObjectOfType<DiceRoller>();
        targetPos= transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < smoothDist)
        {
            // we have reached last desired position. do we have another move in our queue?
            if(moveQueue != null && moveQueueIndex < moveQueue.Length)
            {
                SetNewTargetPosition(moveQueue[moveQueueIndex].transform.position);
                moveQueueIndex++;
            }
        }

        // rise up before moving sideways
        if(transform.position.y < smoothHt)
        {

        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime, maxSpeed);

    }


    void SetNewTargetPosition(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y + 1.0f;
        float z = pos.z;
        targetPos = new Vector3(x, y, z);
        vel = Vector3.zero;
    }


    void OnMouseUp()
    {
        if (theDiceRoller == null)
        {
            return;
        }

        // have we rolled dice
        if (theDiceRoller.isDoneRolling == false)
        {
            return;
        }
        int spacesToMove = theDiceRoller.DiceTotal;

        // where should we end up?
        if (spacesToMove == 0)
        {
            return;
        }

        Tile finalTile = currentTile; // if rolled 0 => true

        moveQueue = new Tile[spacesToMove];

        for(int i= 0; i < spacesToMove; i++)
        {
            if (finalTile == null) // if currentTile == null / not played
            {
                Debug.Log("first tile");
                finalTile = StartingTile;
            }
            else
            {
                if (finalTile == null || finalTile.NextTiles.Length == 0)
                {
                    Debug.Log("game over");
                    Destroy(gameObject);
                    return;
                }
                else if(finalTile.NextTiles.Length > 1)
                {
                    // branch based on player id
                    Debug.Log("Choice exists");
                    finalTile = finalTile.NextTiles[0];
                }
                else
                {
                    Debug.Log("regular move");
                    finalTile = finalTile.NextTiles[0];
                }
            }
            moveQueue[i] = finalTile;
        }

        // teleport tile to final tile
        // animate

        Vector3 finalTilePos = finalTile.transform.position;

        // this.transform.position = new Vector3(x, y, z);
        // SetNewTargetPosition(finalTilePos);
        currentTile = finalTile;
        
        //
        moveQueueIndex = 0;
        theDiceRoller.NewTurn();
    }
}
