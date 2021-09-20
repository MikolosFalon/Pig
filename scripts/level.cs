using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    //stones
    [SerializeField]
    private List<GameObject> stonePoint;
    [SerializeField]
    private GameObject stonePrefab;

    //spawn bombs
    [SerializeField]
    private List<GameObject> bombsPoint;
    //pool bombs
    private List<GameObject> bombs;
    [SerializeField]
    private GameObject bombPrefab;
    //set time spawn bombs
    [SerializeField]
    private float bombTime=10.0f;

    //Enemy
    [SerializeField]
    private List<GameObject> EnemyPrifabs;
    
    //Player
    [SerializeField]
    private GameObject PlayerPrefab;
    private GameObject setPlayer;
    [SerializeField]
    private GameObject StartZone;
    [SerializeField]
    private GameObject FinishZone;

    //start spawn player
    [SerializeField]
    private GameObject WindowPlay;
    void Start()
    {
        //set list null
        bombs = new List<GameObject>();
        
        //make load stones 
        for (int i = 0; i < stonePoint.Count; i++)
        {         
            if (stonePoint[i]!=null)
            {
                Spawn(stonePrefab, stonePoint[i]);
            }
        }

        //make load bombs(pooling)
        for (int i = 0; i < bombsPoint.Count; i++)
        {
            GameObject setBomb =Spawn(bombPrefab, bombsPoint[i]);
            //off bomb 
            setBomb.SetActive(false);
            bombs.Add(setBomb);
        }
        //spawn bombs(active mine)
        StartCoroutine(SpawnBomb());

        //spawn enemu
        for (int i = 0; i < EnemyPrifabs.Count; i++)
        {
            GameObject positionEnemy = bombsPoint[Random.Range(0, bombsPoint.Count)];
            GameObject setBomb = Spawn(EnemyPrifabs[i], positionEnemy);
        }

        //spawn player
            setPlayer = Spawn(PlayerPrefab, StartZone); 
            setPlayer.SetActive(false);        
    }
    //load funtion
    GameObject Spawn(GameObject prefabObject, GameObject positionObject)
    {
        GameObject setObject=Instantiate(prefabObject, positionObject.transform);
        return setObject;
    }
    IEnumerator SpawnBomb()
    {
        int bombposition=Random.Range(0, bombs.Count);
        bombs[bombposition].SetActive(!bombs[bombposition].activeSelf);
        yield return new WaitForSeconds(bombTime);
        StartCoroutine(SpawnBomb());
    }
    public void SpawnPlayer()
    {
        WindowPlay.SetActive(false);
        setPlayer.transform.position = StartZone.transform.position;
        setPlayer.SetActive(true);
    }
    
    //game ower
void Update()
    {
        if (!setPlayer.activeSelf) {
            WindowPlay.SetActive(true);
        }
    }
}
