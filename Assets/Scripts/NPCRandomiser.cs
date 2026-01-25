using System.Collections.Generic;
using UnityEngine;

public class NPCRandomiser : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public enum Facing
    {
        front,
        left,
        right
    }

    public List<Sprite> frontSprites;
    public List<Sprite> leftSprites;
    public List<Sprite> rightSprites;

    public Facing facing;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplySprite();
    }

    Sprite Pick(List<Sprite> list)
    {
        if (list == null || list.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, list.Count);
        Sprite chosen = list[randomIndex];

        list.RemoveAt(randomIndex);

        return chosen;
    }

    void ApplySprite()
    {
        spriteRenderer.flipX = false;

        switch (facing)
        {
            case Facing.front:
                spriteRenderer.sprite = Pick(frontSprites);
                break;

            case Facing.left:
                spriteRenderer.sprite = Pick(leftSprites);
                break;

            case Facing.right:
                spriteRenderer.sprite = Pick(rightSprites);
                break;
        }
    }
}
