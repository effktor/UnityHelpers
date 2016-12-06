// Made from Sarper Soher version
// http://www.sarpersoher.com/materials-from-textures-through-a-context-menu-in-unity/

using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MaterialsForTextures : EditorWindow
{
    [MenuItem("Window/TextureToMaterial")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MaterialsForTextures));
    }

    private GameObject go;

    public string DiffuseName            = "basecolor";
    public string NormalName             = "normal";
    public string MetallicName           = "metallic";
    public string HeightName             = "height";
    public string AOName                 = "AmbientOclusion";

    public string FileExtensionName      = "tif";

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Texture To Material", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Enter name extension for different type of textures", EditorStyles.helpBox);

        //GUILayout.Label("Left", EditorStyles.boldLabel);

        if (GUILayout.Button("Texture To Material"))
        {
            MakeMaterials();
        }

        DiffuseName = EditorGUILayout.TextField("Diffuse name:", DiffuseName);
        NormalName = EditorGUILayout.TextField("Normal name:", NormalName);
        MetallicName = EditorGUILayout.TextField("Metallic name:", MetallicName);
        HeightName = EditorGUILayout.TextField("Height name:", HeightName);
        AOName = EditorGUILayout.TextField("AO name:", AOName);

        if (GUILayout.Button("Save to folder"))
        {
            string path = EditorUtility.OpenFolderPanel( "Load png Textures", "", "" );
            if (path.Length == 0)
                Debug.Log("No folder selected, saving to texture folder");
        }
    }

    private void MakeMaterials()
    {
        var selection = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

        if (selection.Length == 0) return;

        foreach (var texture in selection)
        {
            var selectedTexture = texture as Texture2D;

            var selectedPath = AssetDatabase.GetAssetPath(texture);

            var extensionIndex = selectedPath.IndexOf('.');
            selectedPath = selectedPath.Remove(extensionIndex, selectedPath.Length - extensionIndex);

            if (selectedPath.Contains(DiffuseName))
            {
                Debug.Log(selectedPath);

                var tNormal = GetTextureFromPath(selectedPath, NormalName);
                var tMetallic = GetTextureFromPath(selectedPath, MetallicName);
                var tHeight = GetTextureFromPath(selectedPath, HeightName);

                selectedPath += ".mat";

                var material = new Material(Shader.Find("Standard")) { mainTexture = selectedTexture};

                material.SetTexture("_BumpMap", tNormal);
                material.SetTexture("_MetallicGlossMap", tMetallic);
                material.SetTexture("_ParallaxMap", tHeight);

                AssetDatabase.CreateAsset(material, selectedPath);
            }
        }
    }

    private Texture2D GetTextureFromPath(string path, string nameExtension)
    {
        var pathNew = path.Replace(DiffuseName, nameExtension);
        pathNew += "." + FileExtensionName;

        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(pathNew, typeof(Texture2D));
        return t;
    }
}