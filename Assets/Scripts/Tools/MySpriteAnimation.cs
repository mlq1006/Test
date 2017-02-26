using UnityEngine;
using System.Collections;

public enum AnimType
{
    Once,Loop
}

[RequireComponent(typeof(UISprite))]
public class MySpriteAnimation : MonoBehaviour {

    public float delayTime;//延迟时间
    public int framerate;//帧率
    public string namePrefix;//图片名称前缀
    public AnimType animType;

    private UIAtlas atlas; //图集
    private UISprite sprite; //精灵
    private BetterList<string> spritesList = new BetterList<string>();

    private float timeFramer;//每帧所用时间
    private int spriteCount;

    public bool needSnap;//改变尺寸
    public bool needTranslate;//改变位置
    public bool needRotate;//改变角度

    public Vector3[] spriteDicection;
    public Vector3[] spriteRotation;

    void Start()
    {
        if(0 != framerate)
        {
            timeFramer = 1f / framerate;
        }else
        {
            Debug.LogError("framerate 不可为0");
        }

        sprite = this.GetComponent<UISprite>();
        atlas = sprite.atlas;
        BetterList<string> allList = atlas.GetListOfSprites();
        int count = allList.size;
        for (int i = 0; i < count; i++)
        {
            string name = allList[i];
            if(!string.IsNullOrEmpty(namePrefix) && name.StartsWith(namePrefix))
            {
                spritesList.Add(name);
            }
        }

        spriteCount = spritesList.size;

        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(delayTime);
        int count = 0;
        if(animType.Equals(AnimType.Once))
        {
            while(count < spriteCount)
            {
                yield return new WaitForSeconds(timeFramer);
                SpriteAnim(count);
                count++;
            }
        }else if(animType.Equals(AnimType.Loop))
        {
            while(true)
            {
                if(count == spriteCount)
                {
                    count = 0;
                }
                yield return new WaitForSeconds(timeFramer);
                SpriteAnim(count);
                count++;
            }

        }
        
    }

    void SpriteAnim(int index)
    {
        string spriteName = spritesList[index];
        sprite.spriteName = spriteName;

        if(needSnap)
        {
            var spriteData = atlas.GetSprite(spriteName);
            sprite.width = spriteData.width;
            sprite.height = spriteData.height;
        }

        if(needRotate)
        {
            sprite.transform.rotation = Quaternion.Euler(spriteRotation[index]);
        }
        
        if(needTranslate)
        {
            sprite.transform.localPosition = spriteDicection[index];
        }

    }

}
