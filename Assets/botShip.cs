using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botShip : MonoBehaviour
{
    public int size;
    public Vector3 destination;
    public string destinationName;
    public bool readyForTravel;

    private float axisSpeed;
    private BehaviourScript destinationPlanet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destination.x > gameObject.GetComponent<Transform>().position.x)
        {
            transform.position = transform.position + new Vector3((float)0.01, 0, 0);
        }
        else if (destination.x < gameObject.GetComponent<Transform>().position.x)
        {
            transform.position = transform.position - new Vector3((float)0.01, 0, 0);
        }
        if (destination.y > gameObject.GetComponent<Transform>().position.y)
        {
            transform.position = transform.position + new Vector3(0, (float)0.01, 0);
        }
        else if (destination.y < gameObject.GetComponent<Transform>().position.y)
        {
            transform.position = transform.position - new Vector3(0, (float)0.01, 0);
        }

        if ((Mathf.Abs(destination.x - transform.position.x) <= 0.1)
            && (Mathf.Abs(destination.y - transform.position.y) <= 0.1))
        {
            destinationPlanet = GameObject.Find(destinationName).GetComponent<BehaviourScript>();

            if (destinationPlanet.owned_by_bot == true)
            {
                destinationPlanet.population += size;
            }
            if (destinationPlanet.owned_by_user == true || destinationPlanet.neutral == true)
            {
                if (destinationPlanet.population >= size)
                {
                    destinationPlanet.population -= size;
                }
                else
                {
                    destinationPlanet.population = size - destinationPlanet.population;
                    if (destinationPlanet.owned_by_user == true)
                    {
                        destinationPlanet.owned_by_user = false;
                        destinationPlanet.owned_by_bot = true;
                        destinationPlanet.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (destinationPlanet.neutral == true)
                    {
                        destinationPlanet.neutral = false;
                        destinationPlanet.owned_by_bot = true;
                        destinationPlanet.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
