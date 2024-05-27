using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] Dictionary<GameObject, Vector2> AllCasesOccupied;
    [SerializeField] Vector2  DebuggerMouse;
    [SerializeField] GameObject currentTower;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask mask;


    private void Update()
    {
        MousePosGetter();
    }
    
    void MousePosGetter()
    {
        DebuggerMouse = WordSpaceToGrid(GetMouseGridPosition());
        //currentTower.transform.position = new Vector3(DebuggerMouse.x ,0, DebuggerMouse.y);
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
            return true;    
        else
            return false;
    }

    public void ChooseTower(GameObject gO)
    {
        currentTower = gO;
    }



}

