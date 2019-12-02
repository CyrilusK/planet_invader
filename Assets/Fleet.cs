using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public int size; //количество населения для отправки на корабль
    public Vector3 position; //позиция планеты, с которой мы взяли население
    public int shipsOnScene; //количество кораблей на сцене
    public string shipName; // имя корабля
    public bool allShipsHaveDestination; //все корабли имеют пункт назначения

    public GameObject ship; //создаём пустой игровой объект для добавления кораблей на сцену

    private Ship newShip; //создаём объект типа Ship для работы с новым вставленным на сцену кораблём
    private int shipNumber; //порядковый номер корабля
    private PlayScene playScene;


    // Start is called before the first frame update
    void Start()
    {
        shipNumber = 0; //начинаем нумерацию кораблей с 0
        shipsOnScene = 0; //в начале игры у нас нет кораблей на сцене
        allShipsHaveDestination = true; //по умолчанию все корабли имеют пункт назначения
    }

    // Update is called once per frame
    void Update()
    {
        playScene = GameObject.Find("PlayScene").GetComponent<PlayScene>();
        if (size > 0 ) //если количество населения для отправки на корабль > 0
        {
            shipNumber += 1; //увеличиваем порядковый номер корабля
            Instantiate(ship, position, transform.rotation); //вставляем префаб корабля на сцену
            shipsOnScene += 1;  //увеличиваем счётчик количества кораблей на сцене на 1
            playScene.playerShips += 1;
            allShipsHaveDestination = false;    //не все корабли имеют пункт назначения
            shipName = "Ship" + shipNumber.ToString();  //создаём новое имя корабля = ship + порядковый номер
            GameObject.Find("Ship(Clone)").name = shipName; //задаем уникальное имя новому кораблю
            newShip = GameObject.Find(shipName).GetComponent<Ship>(); //получаем доступ к игровому объекту по имени
            newShip.size = size; //отправили население на корабль
            size = 0; //обнуляем количество населения для отправки на корабль
        }
    }
}
