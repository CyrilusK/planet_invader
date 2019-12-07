using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFleet : MonoBehaviour
{
    public int size; //количество населения бота для отправки на корабль
    public Vector3 position; //позиция планеты, с которой мы взяли население бота
    public int botShipsOnScene; //количество кораблей бота на сцене
    public string botShipName; // имя корабля бота
    public bool allShipsHaveDestination; //все корабли бота имеют пункт назначения
    public int totalPlanetesOnScene; //все планеты на сцене

    public GameObject botShip; //создаём пустой игровой объект для добавления кораблей бота на сцену

    private botShip newBotShip; //создаём объект типа newBotShip для работы с новым вставленным на сцену кораблём
    private int botShipNumber; //порядковый номер корабля бота
    private BehaviourScript botPlanet; //создаем объект для доступа к игровому объекту 
    private bool readyToAttack; //готовность атаковать
    public BehaviourScript planetToAttack; //создаем объект для доступа к игровому объекту для атаки
    private PlayScene playScene;//создаём объект типа PlayScene для работы с основными параметрами игры

    // Start is called before the first frame update
    void Start()
    {
        botShipNumber = 0;//начинаем нумерацию кораблей с 0

    }

    // Update is called once per frame
    void Update()
    {
        playScene = GameObject.Find("PlayScene").GetComponent<PlayScene>();
        int totalBotPlanetPopulation = 0; //обнуляем счётчик популяции на всех планетах бота
        for (int i = 1; i <= totalPlanetesOnScene; i++)
        {
            botPlanet = GameObject.Find("Planet" + i.ToString()).GetComponent<BehaviourScript>();
            if (botPlanet.owned_by_bot == true)
                totalBotPlanetPopulation += botPlanet.population; //прибавляем население планеты к счётчику, если это планета бота
        }

        for (int i = 1; i <= totalPlanetesOnScene; i++)
        {
            planetToAttack = GameObject.Find("Planet" + i.ToString()).GetComponent<BehaviourScript>();
            //если планета не принадлежит боту и половина всеобщего количества населения на планетах бота больше чем
            //население на выбранной планете
            if (!planetToAttack.owned_by_bot && totalBotPlanetPopulation / 2 > planetToAttack.population + 1)
            {
                readyToAttack = true; // выставляем готовность атаковать
                break;
            }
        }

        if (readyToAttack) //если бот готов атаковать
        {
            for (int i = 1; i <= totalPlanetesOnScene; i++)
            {
                botPlanet = GameObject.Find("Planet" + i.ToString()).GetComponent<BehaviourScript>();
                if (botPlanet.owned_by_bot == true) //если планета принадлежит боту 
                {
                    botShipNumber += 1; //увеличиваем номер корабля на 1
                    Instantiate(botShip, botPlanet.transform.position, transform.rotation); //вставляем корабль бота на сцену
                    botShipsOnScene += 1;  //надо проверить, нужно ли?
                    playScene.botShips += 1;
                    botShipName = "botShip" + botShipNumber.ToString(); //создаем имя корабля бота
                    GameObject.Find("botShip(Clone)").name = botShipName; //меняем имя
                    newBotShip = GameObject.Find(botShipName).GetComponent<botShip>(); //получаем доступ к параметрам нового корабля
                    newBotShip.size = botPlanet.population / 2; // размер населения на корабле бота равно половине населения на планете бота
                    botPlanet.population -= newBotShip.size; //вычитаем размер населения на корабле из населения на планете
                    newBotShip.destination = planetToAttack.transform.position; //передаем пункт назначения 
                    newBotShip.destinationName = planetToAttack.name; //передаем имя пункта назначения 
                }
            }
            readyToAttack = false; //отсутствие готовности атаковать
        }
    }
}
