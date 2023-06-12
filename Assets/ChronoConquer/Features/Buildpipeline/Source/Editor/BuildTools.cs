using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using Debug = UnityEngine.Debug;

namespace DevRowInteractive.Buildpipeline
{
    /// <summary>
    /// Utility class for building and versioning a Unity project. The script adds an entry in the top-bar -"/Build".
    /// has to be put into an "Editor" folder.
    /// </summary>
    public static class BuildTools
    {
        private const string NAME = "ChronoConquer";
        private const string BUILD_LOCATION = "Z:/Albert/Work/ChronoConquer/Builds/";

        // This is the path to the Installer. Make sure to have this set up externally, else leave it empty as in ""
        private const string INSTALLER_COMPILER_PATH = "";

        // The path where the currentVersion will be grabbed at. If there is no .txt file a file will be created with semantic versioning (0.0.0)
        private const string BUILD_VERSION_PATH = "Z:/Albert/Work/ChronoConquer/Builds/version.txt";

        // As in Semantic Software Versioning MAJOR.MINOR.PATCH
        private const string CURRENT_MAJOR_VERSION = "0";
        private const string CURRENT_MINOR_VERSION = "1";


        private static string currentVersion;

        /// <summary>
        /// Builds the project and creates an executable.
        /// </summary>
        [MenuItem("Build/Build")]
        public static void Build()
        {
            HandleVersioning();
            string path = BUILD_LOCATION + NAME + "_" + currentVersion + "/" + NAME + ".exe";
            CreateBuild(path, BuildOptions.None);
        }

        /// <summary>
        /// Builds the project for debugging purposes.
        /// </summary>
        [MenuItem("Build/Debug Build")]
        public static void DebugBuild()
        {
            HandleVersioning();
            string path = BUILD_LOCATION + NAME + "_" + currentVersion + "_developmentBuild" + "/" + NAME + ".exe";
            CreateBuild(path, BuildOptions.Development);
        }

        private static void HandleVersioning()
        {
            UpdateVersion(BUILD_VERSION_PATH, 1);
            currentVersion = ReadTextFile(BUILD_VERSION_PATH);
            PlayerSettings.bundleVersion = currentVersion;
        }

        private static void CreateBuild(string path, BuildOptions settings)
        {
            //Create a report to catch possible exceptions and decrease the version again. Try-Catch is not working
            BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path,
                BuildTarget.StandaloneWindows64, settings);

            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError($"Build failed: {report.summary.result.ToString()}");
                UpdateVersion(BUILD_VERSION_PATH, -1);
                return;
            }

            if (INSTALLER_COMPILER_PATH.Length > 0)
                CreateInstaller(INSTALLER_COMPILER_PATH);
            else
                Debug.LogWarning("No Installer Compiler Path defined");
        }

        private static void CreateInstaller(string path)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = path;
            processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);

            Process.Start(processInfo);
        }

        private static string ReadTextFile(string path)
        {
            string content = string.Empty;
            try
            {
                // Read the file at the given path
                content = File.ReadAllText(path);
            }
            catch (IOException e)
            {
                Debug.LogError($"Error reading text file: {e.Message}");
            }

            return content;
        }

        private static void UpdateVersion(string versionFilePath, int increment)
        {
            // Read the current version from the file
            string currentVersion = string.Empty;
            try
            {
                currentVersion = File.ReadAllText(versionFilePath);
            }
            catch (IOException e)
            {
                Debug.LogWarning(
                    $"Error reading version file: {e.Message}, creating new .version.txt with semantic versioning (MAJOR.MINOR.PATCH -> 0.0.0) ");

                // Create the Textfile
                var textFile = File.CreateText(versionFilePath);
                var initialVersion = CURRENT_MAJOR_VERSION + "." + CURRENT_MINOR_VERSION + "." + "0";
                textFile.Write(initialVersion);
                textFile.Close();
                currentVersion = initialVersion;

                // Add the hidden attribute to the file
                FileAttributes attributes = File.GetAttributes(versionFilePath);
                File.SetAttributes(versionFilePath, attributes | FileAttributes.Normal);
            }

            // Split the version string by punctuations
            string[] versionParts = currentVersion.Split('.');
            if (versionParts.Length != 3)
            {
                Debug.LogError("Invalid version format, has to be semantic (MAJOR.MINOR.PATCH -> 0.0.0)");
                return;
            }

            // Increment the last punctuation by 1
            int patchVersion;
            if (!int.TryParse(versionParts[2], out patchVersion))
            {
                Debug.LogError("Invalid version format, has to be semantic (MAJOR.MINOR.PATCH -> 0.0.0)");
                return;
            }

            // Make sure to not decrement the buildnumber if it is 0
            //if (patchVersion != 0)
            patchVersion += increment;

            versionParts[2] = patchVersion.ToString();

            // Combine the version parts back into a string
            string updatedVersion = CURRENT_MAJOR_VERSION + "." + CURRENT_MINOR_VERSION + "." + patchVersion;

            // Write the updated version back to the file
            try
            {
                File.WriteAllText(versionFilePath, updatedVersion);
            }
            catch (IOException e)
            {
                Debug.LogError($"Error writing version file: {e.Message}");
                return;
            }

            Debug.Log($"Version updated to {updatedVersion}");
        }
    }
}