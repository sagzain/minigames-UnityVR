using System.Collections.Generic;
using UnityEditor;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine;

namespace Samples
{
    internal class SamplePackage : IXRPackage
    {
        private const string k_PackageNotificationTooltip =
            @"This loader is purely a sample and will not load any XR Device.

This message is a part of sample code to show how to register a loader that might contain issues or require additonal
context. One example could be that the package that contains this loader is being deprecated and any user who intends to
use the package needs to be aware of deprecation.

Click this icon to be taken to the XR Plug-in Management documentation home page.";

        private const string k_PackageNotificationIcon = "console.warnicon.sml";

        private const string k_PackageNotificationManagementDocsURL =
            @"https://docs.unity3d.com/Packages/com.unity.xr.management@latest/index.html";

        private static readonly IXRPackageMetadata s_Metadata = new SamplePackageMetadata
        {
            packageName = "Sample Package <SAMPLE ONLY YOU MUST REIMPLEMENT>",
            packageId = "com.unity.xr.samplespackage",
            settingsType = typeof(SampleSettings).FullName,

            loaderMetadata = new List<IXRLoaderMetadata>
            {
                new SampleLoaderMetadata
                {
                    loaderName = "Sample Loader One  <SAMPLE ONLY YOU MUST REIMPLEMENT>",
                    loaderType = typeof(SampleLoader).FullName,
                    supportedBuildTargets = new List<BuildTargetGroup>
                    {
                        BuildTargetGroup.Standalone,
                        BuildTargetGroup.WSA
                    }
                },
                new SampleLoaderMetadata
                {
                    loaderName = "Sample Loader Two <SAMPLE ONLY YOU MUST REIMPLEMENT>",
                    loaderType = typeof(SampleLoader).FullName,
                    supportedBuildTargets = new List<BuildTargetGroup>
                    {
                        BuildTargetGroup.Android,
                        BuildTargetGroup.iOS,
                        BuildTargetGroup.Lumin
                    }
                }
            }
        };

        public IXRPackageMetadata metadata
        {
            get
            {
                // Register package notification information anytime the metadata is asked requested.
                var packageNotificationInfo = new PackageNotificationInfo(
                    EditorGUIUtility.IconContent(k_PackageNotificationIcon),
                    k_PackageNotificationTooltip,
                    k_PackageNotificationManagementDocsURL);
                PackageNotificationUtils.RegisterPackageNotificationInformation(s_Metadata.packageId,
                    packageNotificationInfo);
                return s_Metadata;
            }
        }

        public bool PopulateNewSettingsInstance(ScriptableObject obj)
        {
            return true;
        }

        private class SampleLoaderMetadata : IXRLoaderMetadata
        {
            public string loaderName { get; set; }
            public string loaderType { get; set; }
            public List<BuildTargetGroup> supportedBuildTargets { get; set; }
        }

        private class SamplePackageMetadata : IXRPackageMetadata
        {
            public string packageName { get; set; }
            public string packageId { get; set; }
            public string settingsType { get; set; }
            public List<IXRLoaderMetadata> loaderMetadata { get; set; }
        }
    }
}