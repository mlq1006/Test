using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.Animations;

public class BuildAnimation : Editor {

    //生成出的Prefab的路径
    private static string prefabPath = "Assets/Prefabs";
    //生成出的Animation的路径
    private static string animationPath = "Assets/Animation";
    //生成出的AnimationController的路径
    private static string animatorPath = "Assets/Animator";


    [MenuItem("Tools/BuildAnimation")]
    static void AnimationBuild()
    {
        var selections = Selection.GetFiltered(typeof(object), SelectionMode.Assets);
        foreach(var obj in selections)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            Debug.Log("selectpath "+path);
            if(Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                List<AnimationClip> clipList = new List<AnimationClip>();
                foreach(var directory in dir.GetDirectories())
                {
                    Debug.Log("path "+directory.Name);
                    //每个文件夹就是一组帧动画，这里把每个文件夹下的所有图片生成出一个动画文件
                    clipList.Add(BuildAnimationClip(directory, path));
                }

                //把所有的动画文件生成在一个AnimationController里
                AnimatorController animatorController = BuildAnimatorController(clipList, dir.Name);
                BuildPrefab(dir, animatorController, path);
            }

        }
    }

    public static AnimationClip BuildAnimationClip(DirectoryInfo dir,string path)
    {
        string animationName = dir.Name;
        FileInfo[] images = dir.GetFiles("*.png");
        AnimationClip clip = new AnimationClip();
        EditorCurveBinding curveBinding = new EditorCurveBinding();
        curveBinding.type = typeof(SpriteRenderer);
        curveBinding.path = "";
        curveBinding.propertyName = "m_Sprite";
        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Length];

        float frameTime = 1/10f;

        for (int i = 0; i < images.Length; i++)
        {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path + "/" + dir.Name + "/" + images[i].Name);
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = frameTime * i;
            keyFrames[i].value = sprite;
        }

        clip.frameRate = 12f;

        if(animationName.IndexOf("Idle") >= 0)
        {
            //设置idle文件为循环动画
            SerializedObject serializedClip = new SerializedObject(clip);
            AnimationClipSettings clipSettings = new AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
            clipSettings.loopTime = true;
            serializedClip.ApplyModifiedProperties();
        }

        string parentName = Directory.GetParent(dir.FullName).Name;
        Directory.CreateDirectory(animationPath+"/"+parentName);
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
        AssetDatabase.CreateAsset(clip, animationPath + "/" + parentName + "/" + animationName + ".anim");
        AssetDatabase.SaveAssets();
        return clip;
    }

    private static AnimatorController  BuildAnimatorController(List<AnimationClip> list,string name)
    {
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(animatorPath +"/"+ name+".controller");
        AnimatorControllerLayer layer = animatorController.layers[0];
        AnimatorStateMachine state = layer.stateMachine;
        foreach(var clip in list)
        {
            AnimatorState st = state.AddState(clip.name);
            st.motion = clip;
            if(clip.name == "Idle")
            {
                state.defaultState = st;
            }
        }
        AssetDatabase.SaveAssets();

        return animatorController;
    }

    private static void BuildPrefab(DirectoryInfo dir,AnimatorController controller,string path)
    {
        //生成Prefab 添加一张预览用的Sprite
        DirectoryInfo childDir = dir.GetDirectories()[0];
        FileInfo image = childDir.GetFiles("*.png")[0];
        GameObject go = new GameObject();
        go.name = dir.Name;
        SpriteRenderer sprite = go.AddComponent<SpriteRenderer>();
        Debug.LogWarning(path + "/" + dir.Name + "/" + childDir.Name + "/" + image.Name);
        sprite.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path + "/" + childDir.Name + "/" + image.Name);
        Animator animator = go.AddComponent<Animator>();
        animator.runtimeAnimatorController = controller;
        PrefabUtility.CreatePrefab(prefabPath + "/"+go.name+".prefab",go);
        DestroyImmediate(go);
    }

    class AnimationClipSettings
    {
        SerializedProperty m_Property;

        private SerializedProperty Get(string property) { return m_Property.FindPropertyRelative(property); }

        public AnimationClipSettings(SerializedProperty prop) { m_Property = prop; }

        public float startTime { get { return Get("m_StartTime").floatValue; } set { Get("m_StartTime").floatValue = value; } }
        public float stopTime { get { return Get("m_StopTime").floatValue; } set { Get("m_StopTime").floatValue = value; } }
        public float orientationOffsetY { get { return Get("m_OrientationOffsetY").floatValue; } set { Get("m_OrientationOffsetY").floatValue = value; } }
        public float level { get { return Get("m_Level").floatValue; } set { Get("m_Level").floatValue = value; } }
        public float cycleOffset { get { return Get("m_CycleOffset").floatValue; } set { Get("m_CycleOffset").floatValue = value; } }

        public bool loopTime { get { return Get("m_LoopTime").boolValue; } set { Get("m_LoopTime").boolValue = value; } }
        public bool loopBlend { get { return Get("m_LoopBlend").boolValue; } set { Get("m_LoopBlend").boolValue = value; } }
        public bool loopBlendOrientation { get { return Get("m_LoopBlendOrientation").boolValue; } set { Get("m_LoopBlendOrientation").boolValue = value; } }
        public bool loopBlendPositionY { get { return Get("m_LoopBlendPositionY").boolValue; } set { Get("m_LoopBlendPositionY").boolValue = value; } }
        public bool loopBlendPositionXZ { get { return Get("m_LoopBlendPositionXZ").boolValue; } set { Get("m_LoopBlendPositionXZ").boolValue = value; } }
        public bool keepOriginalOrientation { get { return Get("m_KeepOriginalOrientation").boolValue; } set { Get("m_KeepOriginalOrientation").boolValue = value; } }
        public bool keepOriginalPositionY { get { return Get("m_KeepOriginalPositionY").boolValue; } set { Get("m_KeepOriginalPositionY").boolValue = value; } }
        public bool keepOriginalPositionXZ { get { return Get("m_KeepOriginalPositionXZ").boolValue; } set { Get("m_KeepOriginalPositionXZ").boolValue = value; } }
        public bool heightFromFeet { get { return Get("m_HeightFromFeet").boolValue; } set { Get("m_HeightFromFeet").boolValue = value; } }
        public bool mirror { get { return Get("m_Mirror").boolValue; } set { Get("m_Mirror").boolValue = value; } }
    }

}
