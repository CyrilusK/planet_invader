using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourScript : MonoBehaviour
{
    public int population;      //популяция планеты
    public bool owned_by_user;  //принадлежность игроку
    public bool owned_by_bot;   //принадлежность боту
    public bool neutral;        //нейтральная планета
    public float counter;       //счётчик времени

    private Ship ship;

    private Fleet planetFleet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((owned_by_user == true) || (owned_by_bot == true)) { //для планет игрока и бота
            counter += Time.deltaTime; 
            if (counter >= 1)       //если прошла одна или более секунд
            {
                population += 1;    //увеличиваем население
                counter = 0;        //сбрасываем счётчик
            }
        }
    }

    private void OnMouseDown() //при нажатии кнопки мыши на планету
    {
        planetFleet = GameObject.Find("Fleet").GetComponent<Fleet>(); //получаем доступ к игровому объекту Fleet
        if (planetFleet.shipsOnScene > 0) //если на сцене есть корабли
        {
            ship = GameObject.Find(planetFleet.shipName).GetComponent<Ship>(); //получаем доступ к игровому объекту Ship по его имени
        }
        if (owned_by_user == true) //если планета принадлежит игроку
        {
            if (planetFleet.allShipsHaveDestination) //если все корабли имеют пункт назначения
            {
                planetFleet.size = population / 2; //берём половину населения планеты для отправки на новый корабль
                population -= planetFleet.size; //уменьшаем население в 2 раза
                planetFleet.position = gameObject.GetComponent<Transform>().position; //запоминаем позицию планеты, с которой мы взяли население
            }
            else //если есть корабли без пункта назначения
            {
                ship.destination = gameObject.GetComponent<Transform>().position; //назначаем короблю пункт назначения
                ship.destinationName = gameObject.name; //запоминаем название планеты, на которую летит корабль
                ship.readyForTravel = true; //говорим, что корабль готов к отправлению
                planetFleet.allShipsHaveDestination = true; //говорим, что корабли во флоте имеют пункт назначения
            }
        }
        if (owned_by_bot == true || neutral == true) //если планета принадлежит боту или нейтральная
        {
            ship.destination = gameObject.GetComponent<Transform>().position;   //запоминаем позицию планеты, на которую отправим корабли
            ship.destinationName = gameObject.name; //запоминаем название планеты, на которую летит корабль
            ship.readyForTravel = true; //корабли готовы к отправлению
            planetFleet.allShipsHaveDestination = true; //все корабли имеют пункт назначения
        }
    }
}
