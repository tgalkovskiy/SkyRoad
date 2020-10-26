using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEditor;

public class AssetAudio : AssetPostprocessor
{
    private float MaxSize = 200;
    private float MinSize = 5000;
    private void OnPreprocessAudio()
    {
        var File = new FileInfo(assetPath);
        var SizeFile = (File.Length)/1024;
        AudioImporter AudioImporter = (AudioImporter)assetImporter;
        AudioImporter.loadInBackground = true;
        AudioImporter.preloadAudioData = true;
        AudioImporterSampleSettings StandartSetting = AudioImporter.defaultSampleSettings;
        AudioImporter.defaultSampleSettings = StandartSetting;
        if (SizeFile < MaxSize)
        {
            Debug.Log("Dec");
            StandartSetting.loadType = AudioClipLoadType.DecompressOnLoad;
        }
        else if (SizeFile > MinSize && SizeFile < MaxSize)
        {
            Debug.Log("Comp");
            StandartSetting.loadType = AudioClipLoadType.CompressedInMemory;
        }
        else
        {
            Debug.Log("Str");
            StandartSetting.loadType = AudioClipLoadType.Streaming;
        }
    }
}
