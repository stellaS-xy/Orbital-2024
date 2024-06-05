using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPictureManager : MonoBehaviour
{
    //public Picture PicturePrefab;
    //public Transform PicSpawnPosition;
    //public list<Picture> PictureList; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void SpawnPictureMesh(int rows, int cols, Vector2 Pos, Vector2 offset, bool scaleDown)
    {
        for (int col = 0; col < 4; col++)
        {
            for (int row = 0; row < 4; row++)
            {
                //var tempPicture = (Picture)Instantiate(PicturePrefab, PicSpawnPosition.position, PicSpawnPosition.transform.rotation);
            }
        }
    }
    
}
