using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TruRandomTile : RuleTile<TruRandomTile.Neighbor> {
    public Sprite[] randomSprites; // список можливих варіантів

    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Null = 3;
        public const int NotNull = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        switch (neighbor)
        {
            case Neighbor.Null: return tile == null;
            case Neighbor.NotNull: return tile != null;
        }
        return base.RuleMatch(neighbor, tile);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // Викликаємо базову логіку RuleTile (щоб правила працювали)
        base.GetTileData(position, tilemap, ref tileData);

        // Потім накладаємо свій випадковий вибір спрайта
        if (randomSprites != null && randomSprites.Length > 0)
        {
            int seed = position.x * 73856093 ^ position.y * 19349663;
            Random.InitState(seed);
            tileData.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
        }
    }
}