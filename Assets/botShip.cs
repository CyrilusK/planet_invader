using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botShip : MonoBehaviour
{
    public int size; //количество населения на корабле бота
    public Vector3 destination; //координаты пункта назначения
    public string destinationName; //название планеты, на которую бот отправляет корабли
    public bool readyForTravel; //готовность кораблей бота отправиться

    private float axisSpeed; //коэффициент скорости по оси
    private BehaviourScript destinationPlanet; //создаём объект для взаимодействия с планетой назначения
    private PlayScene playScene; //создаём объект типа PlayScene для работы с основными параметрами игры

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playScene = GameObject.Find("PlayScene").GetComponent<PlayScene>();
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

        // если расстояние между положением корабля и планетой меньше или равно 0.1
        if ((Mathf.Abs(destination.x - transform.position.x) <= 0.1)
            && (Mathf.Abs(destination.y - transform.position.y) <= 0.1))
        {
            destinationPlanet = GameObject.Find(destinationName).GetComponent<BehaviourScript>(); //получаем доступ к игровому объекту планеты по имени

            if (destinationPlanet.owned_by_bot == true) // если планета принадлежит боту
            {
                destinationPlanet.population += size; //отправим население с корабля на планету
            }
            if (destinationPlanet.owned_by_user == true || destinationPlanet.neutral == true) //если планета принадлежит игроку или нейтральная
            {
               
                    if (destinationPlanet.population >= size) // если население на планете больше населения на корабле
                    {
                        destinationPlanet.population -= size; // уменьшаем население на планете
                    }
                    else
                    {
                        destinationPlanet.population = size - destinationPlanet.population; // заселяем планету населением с корабля за вычетом населения планеты
                        if (destinationPlanet.owned_by_user == true) // если планета принадлежит игроку
                        {
                            destinationPlanet.owned_by_user = false; //забираем планету у игрока
                            playScene.playerPlanets -= 1;
                            destinationPlanet.owned_by_bot = true; //отдаём планету боту
                            playScene.botPlanets += 1;
                            destinationPlanet.GetComponent<SpriteRenderer>().color = Color.red; //меняем цвет планеты на красный 
                        }
                        if (destinationPlanet.neutral == true) // есчли планета нейтральная
                        {
                            destinationPlanet.neutral = false; //перестаёт быть нейтральной
                            destinationPlanet.owned_by_bot = true; // отдаем планету боту
                            playScene.botPlanets += 1;
                            destinationPlanet.GetComponent<SpriteRenderer>().color = Color.red; //меняем цвет планеты на красный 
                        }
                    }
                }

                Destroy(gameObject); //уничтожаем корабль
                playScene.botShips -= 1;
            }
        }
    }