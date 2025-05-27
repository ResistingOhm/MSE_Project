// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class InfiniteTilemap : MonoBehaviour
// {
//     public GameObject grid; 
//     public Transform player;
//     private Vector3Int playerGridPosition;
//     private Vector3Int lastPlayerGridPosition;

//     private Vector2 tilemapSize;

//     void Start()
//     {
//         Transform baseTilemap = grid.transform.GetChild(0);
//         tilemapSize = CalculateTileSize(baseTilemap.GetComponent<Tilemap>());

//         for (int i = 0; i < 8; i++)
//         {
//             Instantiate(baseTilemap, grid.transform); 
//         }

//         int index = 0;
//         for (int x = -1; x <= 1; x++)
//         {
//             for (int y = -1; y <= 1; y++)
//             {
//                 Vector3 pos = new Vector3(x * tilemapSize.x, y * tilemapSize.y, 0);
//                 grid.transform.GetChild(index++).position = pos;
//             }
//         }

//         player.position = new Vector3(960f, 540f, 0f);
//         playerGridPosition = GetGridPosition(player.position);
//         lastPlayerGridPosition = playerGridPosition;
//     }

//     void Update()
//     {
//         playerGridPosition = GetGridPosition(player.position);

//         if (playerGridPosition != lastPlayerGridPosition)
//         {
//             UpdateTilemaps();
//             lastPlayerGridPosition = playerGridPosition;
//         }
//     }

//     Vector3Int GetGridPosition(Vector3 position)
//     {
//         return new Vector3Int(
//             Mathf.FloorToInt(position.x / tilemapSize.x),
//             Mathf.FloorToInt(position.y / tilemapSize.y),
//             0
//         );
//     }

//     void UpdateTilemaps()
//     {
//         Vector3Int offset = playerGridPosition - lastPlayerGridPosition;

//         foreach (Transform tilemap in grid.transform)
//         {
//             tilemap.position += new Vector3(
//                 offset.x * tilemapSize.x,
//                 offset.y * tilemapSize.y,
//                 0
//             );
//         }
//     }

//     Vector2 CalculateTileSize(Tilemap tilemap)
//     {
//         BoundsInt bounds = tilemap.cellBounds;

//         Vector3Int? min = null;
//         Vector3Int? max = null;

//         foreach (var pos in bounds.allPositionsWithin)
//         {
//             if (tilemap.HasTile(pos))
//             {
//                 if (!min.HasValue)
//                 {
//                     min = max = pos;
//                 }
//                 else
//                 {
//                     min = Vector3Int.Min(min.Value, pos);
//                     max = Vector3Int.Max(max.Value, pos);
//                 }
//             }
//         }

//         if (min.HasValue && max.HasValue)
//         {
//             int width = max.Value.x - min.Value.x + 1;
//             int height = max.Value.y - min.Value.y + 1;
//             return new Vector2(width, height);
//         }

//         return new Vector2(0, 0);
//     }
// //     void LateUpdate()
// // {
// //     Camera.main.transform.position = new Vector3(
// //         player.position.x,
// //         player.position.y,
// //         -10f
// //     );
// // }
// }
