using UnityEngine;

namespace General
{
    public class WallGenerator : MonoBehaviour
    {
        public GameObject wallPrefab;

        public void GenerateWalls(float xMin, float xMax, float yMin, float yMax)
        {
            GameObject wallFolder = new GameObject();
            wallFolder.name = "Walls";
            
            for(float i = xMin; i < xMax; i++)
            {
                Instantiate(wallPrefab, new Vector3(i, yMin, 0), Quaternion.identity).transform.SetParent(wallFolder.transform);
                Instantiate(wallPrefab, new Vector3(i, yMax, 0), Quaternion.identity).transform.SetParent(wallFolder.transform);
            }
            
            for(float i = yMin; i < yMax; i++)
            {
                Instantiate(wallPrefab, new Vector3(xMin, i, 0), Quaternion.identity).transform.SetParent(wallFolder.transform);
                Instantiate(wallPrefab, new Vector3(xMax, i, 0), Quaternion.identity).transform.SetParent(wallFolder.transform);
            }
            
            Instantiate(wallPrefab, new Vector3(xMax, yMax, 0), Quaternion.identity).transform.SetParent(wallFolder.transform);
        }
    }
}