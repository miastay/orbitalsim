using UnityEngine;
using System.Collections;
public class Generator : MonoBehaviour
{

    public float Width = 200;
    public float Height = 200;
    private GameObject[,] grid = new GameObject[20, 20];
    public Transform grid_transform;

    void Start()
    {
        CreateGrid();
    }


    private void CreateGrid()
    {

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject grid_object = Instantiate(grid_transform).gameObject;
                grid_object.transform.position = new Vector2(
                    grid_object.transform.position.x + x,
                    grid_object.transform.position.y + y);
                grid[x, y] = grid_object;
            }
        }
    }
}