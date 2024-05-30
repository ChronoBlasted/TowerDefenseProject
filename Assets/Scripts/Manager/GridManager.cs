using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoSingleton<GridManager>, ISpawner
{

    [SerializeField] Dictionary<Vector2, GameObject> AllCasesOccupied = new Dictionary<Vector2 ,GameObject>();
    [SerializeField] Vector2  DebuggerMouse, AreaSize;
    [SerializeField] GameObject enviro;
    [SerializeField] public Tower currentTower;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask gridMask, objectMask;
    
    [Header("GeneratePath")]
    [SerializeField] List<Vector2> PathsOQP;
    [SerializeField] GameObject path;

    [Header("TowerSlection")]
    [SerializeField] Tower towerA;
    [SerializeField] Tower towerB;
    [SerializeField] bool getTowerA, getTowerB;


    public GameObject ObjectToSpawn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector3 SpawnPosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        VerifyStartCells();
    }

    private void Update()
    {
        MousePosGetter();

        if (Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
        if (Input.GetMouseButtonDown(1))
        {
            UnchooseTower();
        }

        if (getTowerA && currentTower != towerA) {
            ChooseTower(towerA);
            getTowerA = false;
        }
        if (getTowerB && currentTower != towerB) { 
            ChooseTower(towerB);
            getTowerB= false;
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

        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridMask))
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
        if (AllCasesOccupied.ContainsKey(pos))
            return false;    
        else
            return true;
    }

    public void ChooseTower(Tower gO)
    {
        if (currentTower != null)
            UnchooseTower();

        currentTower = Instantiate(gO,enviro.transform);
    }

    public void Spawn()
    {
        if (currentTower == null ||EventSystem.current.IsPointerOverGameObject()) 
            return;

        Dictionary<RESOURCETYPE, int> tower = new Dictionary<RESOURCETYPE, int>
        {
            { currentTower.costResourceType, currentTower.price }
        };
        if (ResourceManager.Instance.EnoughRessource(tower))
        {

            if (GetCellInfo(DebuggerMouse))
            {
                Tower newTower = Instantiate(currentTower, currentTower.transform.position, currentTower.transform.rotation, enviro.transform);
                newTower.isTowerBuilt = true;
                newTower.OnPose();

                AllCasesOccupied.Add(DebuggerMouse,newTower.gameObject);
                ResourceManager.Instance.SpendResources(tower);
                
            }
            else
            print("can not pose because Occupied");
        }
    }
    

    void VerifyStartCells()
    {
        for (int i = (int)(AreaSize.x/-2); i <= AreaSize.x; i++)
        {
            for (int y = (int)(AreaSize.y / -2); y <= AreaSize.y; y++)
            {
                Vector2 Coordinate = new Vector2(i, y);

                Collider[] AAA = Physics.OverlapBox(new Vector3(Coordinate.x, 0, Coordinate.y), Vector3.one * 0.5f,Quaternion.Euler(Vector3.zero),objectMask);
                if (AAA.Length >= 1)
                {
                    foreach(Collider col in  AAA)
                    { 
                        AllCasesOccupied.Add(Coordinate, col.gameObject);
                    }
                }
            }   
        }
    }

    void UnchooseTower()
    {
        GameObject toDestroy = currentTower.gameObject;
        currentTower = null;
        Destroy(toDestroy);
    }

    [ContextMenu("CreatePath")]
    void SetPath()
    {
        foreach(KeyValuePair<Vector2, GameObject> gO in AllCasesOccupied)
        {
            if (gO.Value == path)
            {
                Destroy(gO.Value);
                AllCasesOccupied.Remove(gO.Key);
            }
        }

        for (int x = (int)AreaSize.x / -2; x < AreaSize.x; x++)
        {
            for (int y = (int)AreaSize.y / -2; y < AreaSize.y; y++)
            {
                Vector2 coord = new Vector2(x, y);
                
                if (PathsOQP.Contains(coord))
                {                     
                    Instantiate(path, new Vector3(coord.x, 0, coord.y), Quaternion.Euler(Vector3.zero), enviro.transform);

                }

            }
        }
    }
}

