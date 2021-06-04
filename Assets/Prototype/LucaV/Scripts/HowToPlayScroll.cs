using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayScroll : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    private int currIndex;

    private void OnEnable()
    {
        image.sprite = sprites[0];
        currIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSprite(false);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSprite(true);

        }

    }

    public void ChangeSprite(bool isRight)
    {
        if (isRight)
        {
            currIndex = currIndex + 1 >= sprites.Length ? currIndex : ++currIndex;
            image.sprite = sprites[currIndex];
        }
        else
        {
            currIndex = currIndex - 1 < 0 ? currIndex : --currIndex;
            image.sprite = sprites[currIndex];
        }
    }
}
