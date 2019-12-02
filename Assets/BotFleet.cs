using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFleet : MonoBehaviour
{
    public int size;
    public Vector3 position;
    public int botShipsOnScene;
    public string botShipName;
    public bool allShipsHaveDestination;
    public int totalPlanetesOnScene;

    public GameObject botShip;

    private botShip newBotShip;
    private int botShipNumber;
    private BehaviourScript botPlanet;
    private bool readyToAttack;
    public BehaviourScript planetToAttack;
    private PlayScene playScene;

    // Start is called before the first frame update
    void Start()
    {
        //totalBotPlanetPopulation = 0;
        botShipNumber = 0;

    }

    // Update is called once per frame
    void Update()
    {
        playScene = GameObject.Find("PlayScene").GetComponent<PlayScene>();
        int totalBotPlanetPopulation = 0;
        for (int i = 1; i <= totalPlanetesOnScene; i++)
        {
            botPlanet = GameObject.Find("Planet" + i.ToString()).GetComponent<BehaviourScript>();
            if (botPlanet.owned_by_bot == true)
                totalBotPlanetPopulation += botPlanet.population;
        }

        for (int i = 1; i <= totalPlanetesOnScene; i++)
        {
            planetToAttack = GameObject.Find("Planet" + i.ToString()).GetComponent<BehaviourScript>();
            if (!planetToAttack.owned_by_bot && totalBotPlanetPopulation / 2 > planetToAttack.population + 1)
            {
                readyToAttack = true;
                break;
            }
        }

        if (readyToAttack)
        {
            for (int i = 1; i <= totalPlanetesOnScene; i++)
            {
                botPlanet = GameObject.Find("Planet" + i.ToString()).GetComponent<BehaviourScript>();
                if (botPlanet.owned_by_bot == true)
                {
                    botShipNumber += 1;
                    Instantiate(botShip, botPlanet.transform.position, transform.rotation);
                    botShipsOnScene += 1;  //надо проверить, нужно ли?
                    playScene.botShips += 1;
                    botShipName = "botShip" + botShipNumber.ToString();
                    GameObject.Find("botShip(Clone)").name = botShipName;
                    newBotShip = GameObject.Find(botShipName).GetComponent<botShip>();
                    newBotShip.size = botPlanet.population / 2;
                    botPlanet.population -= newBotShip.size;
                    newBotShip.destination = planetToAttack.transform.position;
                    newBotShip.destinationName = planetToAttack.name;
                }
            }
            readyToAttack = false;
        }
    }
}
