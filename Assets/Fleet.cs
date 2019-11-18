using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public int size;
    public Vector3 position;
    public int shipsOnScene;
    public string shipName;
    public bool allShipsHaveDestination;

    public GameObject ship;

    private Ship newShip;
    private int shipNumber;


    // Start is called before the first frame update
    void Start()
    {
        shipNumber = 0;
        shipsOnScene = 0;
        allShipsHaveDestination = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (size > 0 )
        {
            shipNumber += 1;
            Instantiate(ship, position, transform.rotation);
            shipsOnScene += 1;
            allShipsHaveDestination = false;
            shipName = "Ship" + shipNumber.ToString();
            GameObject.Find("Ship(Clone)").name = shipName;
            newShip = GameObject.Find(shipName).GetComponent<Ship>();
            newShip.size = size;
            size = 0;
        }
    }
}
