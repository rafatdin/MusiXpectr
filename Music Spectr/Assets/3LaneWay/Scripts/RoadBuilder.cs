using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RoadBuilder : MonoBehaviour {

	public GameObject Road;
	public GameObject Coin;
	public List<Material> Materials = new List<Material>();

	public List<Transform> CoinPoints = new List<Transform>();
    public List<GameObject> CurrentRoads = new List<GameObject>();
    public List<GameObject> CurrentCoins = new List<GameObject>();
    public int CurrentPoint = 1;
    public int positionNum = 0;
    public int CurrentRoadId = 0;
	public int LevelCount = 0;
    public float time = 0;
    public float distance = 0, nextDistance = 0;
	public float speed = 1.5f;
    public Transform CenterPoints;
    private Transform Tran;


    public RoadBuilder(GameObject road, GameObject coin, List<Material> materials, Transform t)
    {
        Tran = t;
        Road = road;
        Coin = coin;
        Materials = materials;
        CreateNewRoad(Tran);
    }

    public void MoveObject(GameObject transport)
    {
        Vector3 startPoint = new Vector3(CoinPoints[CurrentPoint].position.x + distance,
                                  CoinPoints[CurrentPoint].position.y - 0.5f,
                                  CoinPoints[CurrentPoint].position.z);
        Vector3 endPoint = new Vector3(CoinPoints[CurrentPoint + 1].position.x + distance,
                                        CoinPoints[CurrentPoint + 1].position.y - 0.5f,
                                        CoinPoints[CurrentPoint + 1].position.z);
        transport.transform.position = Vector3.Lerp(startPoint, endPoint, time);
    }

    public void AddRoad(Transform t) 
    {
        time += Time.deltaTime * speed;

        if (time > 1)
        {
            time = 0;
            CurrentPoint++;

            if (CoinPoints[CurrentPoint].name == "P10")
            {
                CurrentRoadId++;
                LevelCount++;
                CreateNewRoad(t);
            }

        }
    }

    public void ChangePosition()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (positionNum != -1)
            {
                positionNum--;
                nextDistance = 1.5f * positionNum;
            }

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (positionNum != 1)
            {
                positionNum++;
                nextDistance = 1.5f * positionNum;
            }
        }


        distance = Mathf.Lerp(distance, nextDistance, Time.deltaTime * 3 * speed);
    }

	    
    public void CreateNewRoad(Transform t)
	{
		for (int i = CurrentCoins.Count - 1; i >= 0; i--) {
			if(CurrentCoins[i].transform.position.z < t.position.z)
			{
				DestroyImmediate(CurrentCoins[i]);
				CurrentCoins.RemoveAt(i);
			}
		}
		
        if(CurrentRoadId > 0)
		{
			if(CurrentRoadId > 1)
			{
				DestroyImmediate(CurrentRoads[CurrentRoadId - 2]);
				CurrentRoads.RemoveAt(0);
				CurrentRoadId--;
			}
            Vector3 v3 = new Vector3();
            v3.Set(0, 0, CoinPoints[LevelCount * 19 + LevelCount - 1].position.z);
			CurrentRoads.Add(Instantiate (Road, v3, Quaternion.identity) as GameObject);
		}
		else
			CurrentRoads.Add(Instantiate (Road, Vector3.zero, Quaternion.identity) as GameObject);

        roadSide();
        CurrentRoads[CurrentRoadId].transform.FindChild(transformsName).gameObject.SetActive(true);
        CenterPoints = CurrentRoads[CurrentRoadId].transform.FindChild(transformsName).FindChild("Points");
		for (int i = 1; i < 21; i++) 
		{
			Transform childPoint = CenterPoints.transform.FindChild("P" + i);
			CoinPoints.Add(childPoint);

			int typeNum = Random.Range(0,  Materials.Count);
			Vector3 cPOS = Vector3.zero;
			
			if(LevelCount > 0)
				cPOS = new Vector3(childPoint.transform.position.x + Random.Range(-1, 2) * 1.5f,
				                   childPoint.transform.position.y - 0.5f, 
				                   childPoint.transform.position.z);
			else
				cPOS = new Vector3(CoinPoints[i - 1].position.x + Random.Range(-1,2) * 1.5f,
                                   CoinPoints[i - 1].position.y - 0.5f, 
                                   CoinPoints[i - 1].position.z);



            GameObject newCoin = (Instantiate(Coin, cPOS, Quaternion.identity) as GameObject);
            CurrentCoins.Add(newCoin);
            //newCoin.transform.Rotate(90, 0, 0);
            newCoin.transform.GetComponent<Renderer>().material = Materials[typeNum];

            if (typeNum == 0)
                newCoin.name = "Yellow";

            else if (typeNum == 1)
                newCoin.name = "Red";

            else if (typeNum == 2)
                newCoin.name = "Blue";

            else if (typeNum == 3)
                newCoin.name = "Black";
		}


	}


    private string transformsName = "Straight";
    private void roadSide()
    {
        string[] sides = new string[] { "Left", "Straight", "Right" };
        if (transformsName.Equals("Straight"))
            transformsName = sides[Random.Range(0, 2)];
        else if (transformsName.Equals("Right"))
            transformsName = sides[Random.Range(1, 2)];
        else
            transformsName = "Right";

    }
}
