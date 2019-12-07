using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int size; //количество населения на корабле
    public Vector3 destination; //координаты пункта назначения
    public string destinationName; //название планеты, на которую отправим корабли
    public bool readyForTravel; //готовность кораблей отправиться

    private float axisSpeed; //коэффициент скорости по оси
    private BehaviourScript destinationPlanet; //создаём объект для взаимодействия с планетой назначения
    private PlayScene playScene; //создаём объект типа PlayScene для работы с основными параметрами игры

    // Start is called before the first frame update
    void Start()
    {
        readyForTravel = false; //при создании корабля он ещё не готов к отправке
        axisSpeed = Mathf.Abs( (destination.x - transform.position.x) / (destination.y - gameObject.transform.position.y));
    }

    // Update is called once per frame
    void Update()
    {
        playScene = GameObject.Find("PlayScene").GetComponent<PlayScene>();

        if (readyForTravel) //если готовы к отправлению
        {
            if (destination.x > gameObject.GetComponent<Transform>().position.x) //если х пункта назначения больше, чем х положения корабля
            {
                transform.position = transform.position + new Vector3((float)0.01, 0, 0); //увеличиваем значение координаты х корабля
            }
            else if (destination.x < gameObject.GetComponent<Transform>().position.x) //если х пункта назначения меньше, чем х положения корабля
            {
                transform.position = transform.position - new Vector3((float)0.01, 0, 0); //уменьшаем значение координаты х корабля
            }
            if (destination.y > gameObject.GetComponent<Transform>().position.y)
            {
                transform.position = transform.position + new Vector3(0, (float)0.01, 0);
            }
            else if (destination.y < gameObject.GetComponent<Transform>().position.y)
            {
                transform.position = transform.position - new Vector3(0, (float)0.01, 0);
            }
        }

        if ((Mathf.Abs(destination.x - transform.position.x) <= 0.1 )
            && (Mathf.Abs(destination.y - transform.position.y) <= 0.1)) // если расстояние между положением корабля и планетой меньше или равно 0.1
        {
            destinationPlanet = GameObject.Find(destinationName).GetComponent<BehaviourScript>();//получаем доступ к игровому объекту планеты по имени

            if (destinationPlanet.owned_by_user == true) // если планета принадлежит игроку
            {
                destinationPlanet.population += size; //отправим население с корабля на планету
            }
            if (destinationPlanet.owned_by_bot == true || destinationPlanet.neutral == true) //если планета принадлежит боту или нейтральная
            {
                if (destinationPlanet.population >= size) // если население на планете больше населения на корабле
                {
                    destinationPlanet.population -= size; // уменьшаем население на планете
                }
                else // иначе
                {
                    destinationPlanet.population = size - destinationPlanet.population; // заселяем планету населением с корабля за вычетом населения планеты
                    if (destinationPlanet.owned_by_bot == true) // если планета принадлежит боту
                    {
                        destinationPlanet.owned_by_bot = false; //забираем планету у бота
                        playScene.botPlanets -= 1;
                        destinationPlanet.owned_by_user = true; //отдаём планету игроку
                        playScene.playerPlanets += 1;
                        destinationPlanet.GetComponent<SpriteRenderer>().color = Color.green; //меняем цвет планеты на зёленый 
                    }
                    if (destinationPlanet.neutral == true) // если планета нейтральная
                    {
                        destinationPlanet.neutral = false; //перестаёт быть нейтральной
                        destinationPlanet.owned_by_user = true; //отдаём планету игроку
                        playScene.playerPlanets += 1;
                        destinationPlanet.GetComponent<SpriteRenderer>().color = Color.green; //меняем цвет планеты на зёленый 
                    }
                }
            }
            
            Destroy(gameObject); //уничтожаем корабль
            GameObject.Find("Fleet").GetComponent<Fleet>().shipsOnScene -= 1; //уменьшаем счетчик кол-ва кораблей на 1
            playScene.playerShips -= 1;
        }
    }
}
