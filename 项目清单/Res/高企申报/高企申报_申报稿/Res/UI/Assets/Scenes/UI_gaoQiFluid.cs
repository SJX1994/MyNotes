using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// Folder creator wizard.
/// </summary>
public class UI_gaoQiFluid : ScriptableWizard
{

    // Flags for folder creation.
    // Animations
    public bool 中点法;
    // Materials
    public bool 自适应改变步长;
    // Prefabs
    public bool 隐式实现方法;


    /// <summary>
    /// Creating and displaying wizard.
    /// </summary>
    [MenuItem("艾迪亚/Unity Fluid Pro Unity2019")]
    public static void CreateWizard()
    {
        DisplayWizard<UI_gaoQiFluid>("Create Default Folders", "导入");
    }


    /// <summary>
    /// Wizard Update.
    /// Runs when window need to be refreshed.
    /// </summary>
    private void OnWizardUpdate()
    {
        // Shows message what to do.
        helpString = "Select folders to create!";

        // Building error message if any of the selected folder exists.
        errorString = "";

       

       
    }

    /// <summary>
    /// Method called on Create button click.
    /// Used here to create selected folder.
    /// </summary>
    private void OnWizardCreate()
    {
        
    }
}
