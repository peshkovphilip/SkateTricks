using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SpriteManager
{
    public static List<Sprite> SpritesMain = new List<Sprite>();
    public static bool LoadingComplete;

    public static async Task InitAllSprites()
    {
        await InitSprites("main", SpritesMain);    
    }
    
    public static async Task InitSprites(string assetLabel, List<Sprite> outSprites)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(assetLabel, typeof(Sprite)).Task;
        List<Task<Sprite>> tasks = new List<Task<Sprite>>();

        foreach (var location in locations)
        {
            tasks.Add(Addressables.LoadAssetAsync<Sprite>(location).Task);
        }

        var loadedSprites = await Task.WhenAll(tasks);

        outSprites.Clear();
        foreach (Sprite sprite in loadedSprites)
        {
            outSprites.Add(sprite);
        }

        LoadingComplete = true;
    }
}
