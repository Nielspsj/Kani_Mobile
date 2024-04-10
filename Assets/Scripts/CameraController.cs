using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();
    private Vector3 gyroRot = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(playerGO);
        }
        NewCameraPosition();

        Input.gyro.enabled = true;
        //InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        //InputSystem.EnableDevice(Accelerometer.current);
    }

    private void Update()
    {
        ViewWithGyro();

    }

    //Listen to an event that changes the current player.
    public void NewCameraPosition()
    {
        //currentPlayer = curr
        Debug.Log("Currentplayer: " + GameManager.currentPlayer);
        Vector3 newPosition = players[GameManager.currentPlayer - 1].transform.position;
        newPosition.y = newPosition.y + 1f;
        transform.position = newPosition;
        transform.LookAt(GameObject.FindGameObjectWithTag("Table").transform);
    }

    private void ViewWithGyro()
    {
        //Unity is left handed, Gyro is right handed. So below line will kinda not work.
        //transform.rotation = Input.gyro.attitude;

        //Fix for switching to Unity's left handed orientation.
        gyroRot.y = -Input.gyro.rotationRateUnbiased.y;
        transform.Rotate(gyroRot);
    }
}
