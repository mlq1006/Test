using UnityEngine;
using System.Collections;

public enum AlignmentType
{
    Left = -1,Middle,Right = 1
}

public class MySpriteNum : MonoBehaviour {

    public UIAtlas atlas;
    public string prefix;
    public Color color;
    public AlignmentType align = AlignmentType.Left;
    public float offectWidth;
    public int depth;

    private int _num;
    public int Num
    {
        get{ return _num; }
        set
        {
            _num = value;
            ChangeNum(_num);
        }
    }

    private void ChangeNum(int num)
    {
        string numStr = num.ToString();
        int numlength = numStr.Length;
        int count = transform.childCount;
        for (int i = 0; i < numlength; i++)
        {
            if(i >= count)
            {
                char numName = numStr[i];
                NGUITools.AddSprite(gameObject, atlas, prefix + numName);
            }
        }
        count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = transform.GetChild(i);
            if (i < numlength)
            {
                child.gameObject.SetActive(true);
                UISprite sprite = child.GetComponent<UISprite>();
                char numName = numStr[i];
                sprite.spriteName = prefix + numName;
                sprite.color = color;
                sprite.depth = depth;
                sprite.MakePixelPerfect();
                if(align == AlignmentType.Middle)
                {
                    sprite.transform.localPosition = new Vector3(-1 * (numlength - 1) * offectWidth/2 + i * offectWidth, 0, 0);
                }
                else if (align == AlignmentType.Right)
                {
                    sprite.transform.localPosition = new Vector3(i * offectWidth, 0, 0);
                }else if(align == AlignmentType.Left)
                {
                    sprite.transform.localPosition = new Vector3( -1*(numlength - 1 - i) * offectWidth, 0, 0);
                }

            }else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
