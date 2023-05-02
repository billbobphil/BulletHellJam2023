using UnityEngine;

namespace General
{
    public class WallGenerator : MonoBehaviour
    {
        public GameObject wallPrefab;
        [SerializeField] private Sprite topWallSprite;
        [SerializeField] private Sprite bottomWallSprite;
        [SerializeField] private Sprite leftWallSprite;
        [SerializeField] private Sprite rightWallSprite;
        [SerializeField] private Sprite topRightCornerWallSprite;
        [SerializeField] private Sprite topLeftCornerWallSprite;
        [SerializeField] private Sprite bottomRightCornerWallSprite;
        [SerializeField] private Sprite bottomLeftCornerWallSprite;

        public void GenerateWalls(float xMin, float xMax, float yMin, float yMax)
        {
            GameObject wallFolder = new GameObject();
            wallFolder.name = "Walls";
            
            for(float i = xMin; i < xMax; i++)
            {
                GameObject bottomWall = Instantiate(wallPrefab, new Vector3(i, yMin, 0), Quaternion.identity); 
                bottomWall.transform.SetParent(wallFolder.transform);
                
                if (i == xMin)
                {
                    bottomWall.GetComponent<SpriteRenderer>().sprite = bottomLeftCornerWallSprite;
                    bottomWall.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                else
                {
                    bottomWall.GetComponent<SpriteRenderer>().sprite = bottomWallSprite;    
                }
                
                GameObject topWall = Instantiate(wallPrefab, new Vector3(i, yMax, 0), Quaternion.identity);
                topWall.transform.SetParent(wallFolder.transform);
                if (i == xMin)
                {
                    topWall.GetComponent<SpriteRenderer>().sprite = topLeftCornerWallSprite;
                    topWall.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                else
                {
                    topWall.GetComponent<SpriteRenderer>().sprite = topWallSprite;
                }
            }
            
            for(float i = yMin; i < yMax; i++)
            {
                GameObject leftWall = Instantiate(wallPrefab, new Vector3(xMin, i, 0), Quaternion.identity);
                leftWall.transform.SetParent(wallFolder.transform);
                leftWall.GetComponent<SpriteRenderer>().sprite = leftWallSprite;
                GameObject rightWall = Instantiate(wallPrefab, new Vector3(xMax, i, 0), Quaternion.identity);
                rightWall.transform.SetParent(wallFolder.transform);
                rightWall.GetComponent<SpriteRenderer>().sprite = rightWallSprite;
            }

            GameObject topRightCornerWall = Instantiate(wallPrefab, new Vector3(xMax, yMax, 0), Quaternion.identity);
            topRightCornerWall.transform.SetParent(wallFolder.transform);
            topRightCornerWall.GetComponent<SpriteRenderer>().sprite = topRightCornerWallSprite;
            topRightCornerWall.GetComponent<SpriteRenderer>().sortingOrder = 1;

            GameObject bottomRightCornerWall = Instantiate(wallPrefab, new Vector3(xMax, yMin, 0), Quaternion.identity);
            bottomRightCornerWall.transform.SetParent(wallFolder.transform);
            bottomRightCornerWall.GetComponent<SpriteRenderer>().sprite = bottomRightCornerWallSprite;
            bottomRightCornerWall.GetComponent<SpriteRenderer>().sortingOrder = 1;

        }
    }
}