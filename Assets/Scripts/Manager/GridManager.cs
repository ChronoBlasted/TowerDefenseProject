using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoSingleton<GridManager>, ISpawner
{

    [SerializeField] Dictionary<GameObject, Vector2> AllCasesOccupied = new Dictionary<GameObject, Vector2>();
    [SerializeField] Vector2  DebuggerMouse;
    [SerializeField] GameObject currentTower, enviro;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask mask;

    public GameObject ObjectToSpawn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector2 SpawnPosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Update()
    {
        MousePosGetter();

        if (Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
    }
    
    void MousePosGetter()
    {
        DebuggerMouse = WordSpaceToGrid(GetMouseGridPosition());
        if(currentTower)
            currentTower.transform.position = new Vector3(DebuggerMouse.x ,0, DebuggerMouse.y);

    }
   
    Vector3 GetMouseGridPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            return hit.point;
        }
       
        return new Vector3 (0,0,0);
    }

    public Vector2 WordSpaceToGrid(Vector3 WorldPosition)
    {
        int posX = Mathf.RoundToInt(WorldPosition.x);
        int posZ = Mathf.RoundToInt(WorldPosition.z);

        return new Vector2(posX, posZ);
    }

    public bool GetCellInfo(Vector2 pos)
    {
        if (AllCasesOccupied.ContainsValue(pos))
            return false;    
        else
            return true;
    }
    
    public void ChooseTower(GameObject gO)
    {
        currentTower = gO;
    }

    public void Spawn()
    {

        Dictionary<string, int> AAA = new Dictionary<string, int>();
        AAA.Add("Gold", 2);

        if (ResourceManager.Instance.EnoughRessource(AAA))
        {
            if (GetCellInfo(DebuggerMouse))
            {
                GameObject newTower = Instantiate(currentTower, currentTower.transform.position, currentTower.transform.rotation, enviro.transform);
                AllCasesOccupied.Add(newTower, DebuggerMouse);
                ResourceManager.Instance.SpendResources(AAA);
            }
            else
            print("can not pose because Occupied");
        }
        else
        {
            print("can not pose because You PAUVRE");

        }

    }
}

