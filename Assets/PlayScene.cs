using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public GameObject planet1;
    public GameObject planet2;
    public GameObject planet3;
    public GameObject planet4;

    public GameObject win;
    public GameObject lose;

    public int playerPlanets;
    public int botPlanets;
    public int playerShips;
    public int botShips;


    private BehaviourScript newPlanet;
    private Transform planetPosition;
    private string planetName;
    private string botPlanetName;
    private string playerPlanetName;

    private Vector3[] randPosition;
    private Vector3 samplePosition;
    private bool positionAccepted;

    // Start is called before the first frame update
    void Start()
    {
        System.Array.Resize(ref randPosition, 12);
        positionAccepted = true;

        playerPlanets = 0;
        botPlanets = 0;
        playerShips = 0;
        botShips = 0;

    //float max_X = -7.5f, max_Y = -3.8f, min_X = 7.5f, min_Y = 3.8f;
    float max = 0f, min = 0f;

        for (int i = 0; i < 12; i++)
        {
            positionAccepted = true;
            samplePosition.Set(Random.Range(-7.5f, 7.5f), Random.Range(-3.8f, 3.8f), 0);
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if ((Mathf.Abs(samplePosition.x - randPosition[j].x) < 2.5) &&
                        (Mathf.Abs(samplePosition.y - randPosition[j].y) < 2.5))
                    {
                        positionAccepted = false;
                    }
                }
                if (positionAccepted)
                {
                    randPosition[i] = samplePosition;
                } else
                {
                    i--;
                }
            }
        }

        for (int i = 1; i < 12; i++)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    Instantiate(planet1, randPosition[i], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet1(Clone)").name = planetName;
                    break;
                case 2:
                    Instantiate(planet2, randPosition[i], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet2(Clone)").name = planetName;
                    break;
                case 3:
                    Instantiate(planet3, randPosition[i], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet3(Clone)").name = planetName;
                    break;
                case 4:
                    Instantiate(planet4, randPosition[i], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet4(Clone)").name = planetName;
                    break;
                default:
                    Instantiate(planet1, randPosition[i], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet1(Clone)").name = planetName;
                    break;
            }
            newPlanet = GameObject.Find(planetName).GetComponent<BehaviourScript>();
            newPlanet.neutral = true;
        }

        for (int i = 1; i < 12; i++)
        {
            planetName = "Planet" + i.ToString();
            planetPosition = GameObject.Find(planetName).GetComponent<Transform>();

            //if ((planetPosition.position.x >= max_X) && (planetPosition.position.y >= max_Y))
            //{
            //    max_X = planetPosition.position.x;
            //    max_Y = planetPosition.position.y;
            //    botPlanetName = planetName;
            //}
            //if ((planetPosition.position.x <= min_X) && (planetPosition.position.y <= min_Y))
            //{
            //    min_X = planetPosition.position.x;
            //    min_Y = planetPosition.position.y;
            //    playerPlanetName = planetName;
            //}

            if (planetPosition.position.x + Mathf.Abs(planetPosition.position.y) > max)
            {
                max = planetPosition.position.x + planetPosition.position.y;
                botPlanetName = planetName;
            }
            if (planetPosition.position.x - Mathf.Abs(planetPosition.position.y) < min)
            {
                min = planetPosition.position.x + planetPosition.position.y;
                playerPlanetName = planetName;
            }

        }

        newPlanet = GameObject.Find(botPlanetName).GetComponent<BehaviourScript>();
        newPlanet.neutral = false;
        newPlanet.owned_by_bot = true;
        botPlanets += 1;
        newPlanet.GetComponent<SpriteRenderer>().color = Color.red;

        newPlanet = GameObject.Find(playerPlanetName).GetComponent<BehaviourScript>();
        newPlanet.neutral = false;
        newPlanet.owned_by_user = true;
        playerPlanets += 1;
        newPlanet.GetComponent<SpriteRenderer>().color = Color.green;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerShips == 0 && playerPlanets ==0)
        {
            Instantiate(lose, transform.position, transform.rotation);
            Time.timeScale = 0;
            Application.Quit();
        }
        if (botShips == 0 && botPlanets == 0)
        {
            Instantiate(win, transform.position, transform.rotation);
            Time.timeScale = 0;
            Application.Quit();
        }
    }
}
