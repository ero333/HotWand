using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class NewEditorTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		string[] projectContent = AssetDatabase.GetAllAssetPaths();  
		AssetDatabase.ExportPackage(projectContent, "UltimateTemplate.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeLibraryAssets );  
        Debug.Log("Project Exported"); 
    }
}
