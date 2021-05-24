using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Direction direction;
    private bool isEmpty;
    private float spawnTimer = 0.25f;

    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, 1 << LayerMask.NameToLayer("Environment"));
        if (colliders.Length <= 0)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
        spawnTimer = Random.Range(0.20f, 0.35f);

    }

    private void Update()
    {
        if (isEmpty)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                isEmpty = false;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, 1 << LayerMask.NameToLayer("Environment"));
                if (colliders.Length <= 0)
                    CreateRoom();
            }
        }
    }

    private void CreateRoom()
    {
        List<Room> possibleRooms = new List<Room>();
        Direction needDirection = Direction.north;

        switch (direction)
        {
            case Direction.north:
                needDirection = Direction.south;
                break;
            case Direction.west:
                needDirection = Direction.east;
                break;
            case Direction.south:
                needDirection = Direction.north;
                break;
            case Direction.east:
                needDirection = Direction.west;
                break;
            default:
                break;
        }

        Room[] nonEndRooms;
        Vector3 spawnPos = transform.position;
        if (Random.Range(0, 4) == 1)
        {
            Vector3 hallWayPos = transform.parent.position + (transform.position - transform.parent.position) + (transform.position - transform.parent.position).normalized * 10;
            if (Physics.OverlapSphere(hallWayPos, 4f).Length <= 0)
            {
                nonEndRooms = GameManager.singleton.hallwayRooms;
                spawnPos = hallWayPos;
            }
            else
            {
                nonEndRooms = GameManager.singleton.rooms;
            }
        }
        else
        {
            nonEndRooms = GameManager.singleton.rooms;
        }

        Room[] usedRooms;
        if (++GameManager.singleton.RoomNumber <= GameManager.singleton.minRooms)
        {
            usedRooms = nonEndRooms;
        }
        else
        {
            usedRooms = GameManager.singleton.endRooms;
            spawnPos = transform.position;
        }
        foreach (var room in usedRooms)
        {
            foreach (var dir in room.directions)
            {
                if (dir.direction == needDirection)
                {
                    possibleRooms.Add(room);
                }
            }
        }

        int rand = Random.Range(0, possibleRooms.Count);

        Instantiate(possibleRooms[rand].gameObject, spawnPos, Quaternion.identity);
    }
}
