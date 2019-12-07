using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public GameObject planet1; //создаём пустой игровой объект для добавления планеты 1 на сцену
    public GameObject planet2; //"-"
    public GameObject planet3;
    public GameObject planet4;

    public GameObject win; //создаём пустой игровой объект для отображения "победа"
    public GameObject lose; //создаём пустой игровой объект для отображения "поражение"

    public int playerPlanets; //счётчик планет игрока
    public int botPlanets; //счётчик планет бота
    public int playerShips; //счётчик кораблей игрока
    public int botShips; //счётчик кораблей бота


    private BehaviourScript newPlanet; //создаем объект для доступа к свойствам планеты
    private Transform planetPosition; // создаем объект для назначения координат планеты
    private string planetName; //название планеты
    private string botPlanetName; //название планеты бота
    private string playerPlanetName; //название планеты игрока

    private Vector3[] randPosition; //массив случайных координат планет
    private Vector3 samplePosition; //временное значение координат планеты
    private bool positionAccepted; //принятие положения планеты

    // Start is called before the first frame update
    void Start()
    { 
        System.Array.Resize(ref randPosition, 12); //изменение размера массива randPosition на 12 планет
        positionAccepted = true; 

        playerPlanets = 0; //в начале игры у нас нет планет игрока на сцене
        botPlanets = 0; //в начале игры у нас нет планет бота на сцене
        playerShips = 0; //в начале игры у нас нет кораблей игрока на сцене
        botShips = 0; //в начале игры у нас нет кораблей бота на сцене

        float max = 0f, min = 0f; //переменные для вычисления Манхэттенского растояния от центра сцены,
                                  //чтобы выбрать какая планета будет принадлежать игроку, а какая - боту

        for (int i = 0; i < 12; i++)
        {
            positionAccepted = true;
            samplePosition.Set(Random.Range(-7.5f, 7.5f), Random.Range(-3.8f, 3.8f), 0); //случайный выбор временной координаты
            if (i > 0) //если у нас уже есть первые координаты для планеты
            {
                for (int j = 0; j < i; j++)
                {
                    //если разница между временными и уже имеющимися коодинатами
                    //по обоим осям меньше 2.5, то мы не принимаем такие координаты
                    if ((Mathf.Abs(samplePosition.x - randPosition[j].x) < 2.5) && 
                        (Mathf.Abs(samplePosition.y - randPosition[j].y) < 2.5)) 
                    {
                        positionAccepted = false;
                    }
                }
                if (positionAccepted)
                {
                    randPosition[i] = samplePosition;//положим временные координаты в массив
                } else
                {
                    i--; //возвращаемся к предыдущему шагу выбора координыт
                }
            } else randPosition[i] = samplePosition; //запоминаем координаты для самой первой планеты
        }

        for (int i = 1; i < 13; i++)
        {
            switch (Random.Range(1, 5)) //переключатель для случайного выбора размера планеты
            {
                case 1:
                    Instantiate(planet1, randPosition[i-1], transform.rotation); //вставляем самую маленькую планету
                    planetName = "Planet" + i.ToString(); // создаём имя планеты с номером
                    GameObject.Find("Planet1(Clone)").name = planetName; // меняем имя вставленной планеты
                    break;
                case 2:
                    Instantiate(planet2, randPosition[i-1], transform.rotation); //"-"
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet2(Clone)").name = planetName;
                    break;
                case 3:
                    Instantiate(planet3, randPosition[i-1], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet3(Clone)").name = planetName;
                    break;
                case 4:
                    Instantiate(planet4, randPosition[i-1], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet4(Clone)").name = planetName;
                    break;
                default:
                    Instantiate(planet1, randPosition[i-1], transform.rotation);
                    planetName = "Planet" + i.ToString();
                    GameObject.Find("Planet1(Clone)").name = planetName;
                    break;
            }
            newPlanet = GameObject.Find(planetName).GetComponent<BehaviourScript>(); //получаем доступ к св-вам вставленной планеты
            newPlanet.neutral = true; //по умолчанию все планеты нейтральные
        }

        for (int i = 1; i < 13; i++)
        {
            planetName = "Planet" + i.ToString(); // создаём имя планеты с номером
            planetPosition = GameObject.Find(planetName).GetComponent<Transform>(); //запоминаем позицию планеты с именем planetName
            //если манхэтенское расстояние больше текущего расстояния справа от центра
            if (planetPosition.position.x + Mathf.Abs(planetPosition.position.y) > max) 
            {
                max = planetPosition.position.x + Mathf.Abs(planetPosition.position.y); //перезаписываем максимум
                botPlanetName = planetName; //запоминаем имя планеты бота
            }
            //если отрицательное манхетенское расстояние меньше текущего расстояния слева от центра
            if (planetPosition.position.x - Mathf.Abs(planetPosition.position.y) < min)
            {
                min = planetPosition.position.x - Mathf.Abs(planetPosition.position.y); //перезаписываем минимум
                playerPlanetName = planetName; //запоминаем имя планеты игрока
            }

        }

        //передаём планету боту
        newPlanet = GameObject.Find(botPlanetName).GetComponent<BehaviourScript>();
        newPlanet.neutral = false;
        newPlanet.owned_by_bot = true;
        botPlanets += 1;
        newPlanet.GetComponent<SpriteRenderer>().color = Color.red;

        //передаём планету игроку
        newPlanet = GameObject.Find(playerPlanetName).GetComponent<BehaviourScript>();
        newPlanet.neutral = false;
        newPlanet.owned_by_user = true;
        playerPlanets += 1;
        newPlanet.GetComponent<SpriteRenderer>().color = Color.green;

    }

    // Update is called once per frame
    void Update()
    {
        //условие поражения игрока
        if (playerShips == 0 && playerPlanets ==0)
        {
            Instantiate(lose, transform.position, transform.rotation); //отображаем "поражение" на сцене
            Time.timeScale = 0; //останавливаем течение времени в игре
            Application.Quit(); //выход из игры
        }
        //условие поражения бота
        if (botShips == 0 && botPlanets == 0)
        {
            Instantiate(win, transform.position, transform.rotation); //отображаем "победа" на сцене
            Time.timeScale = 0; //останавливаем течение времени в игре
            Application.Quit(); //выход из игры
        }
    }
}
