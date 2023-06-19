﻿namespace MarketPlace.ProductsApi.FileServices;

public class FileService
{
    private const string Images = "Images";

    private static void CheckDirectory(string folder)
    {
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
    }
    
    public static string ProductImages(IFormFile file)
    { 
        return  SaveFile(file, "ProductImages");
    }

    private static string SaveFile(IFormFile file, string folder)
    {
        CheckDirectory(Path.Combine(Images, folder));
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var ms = new MemoryStream();
         file.CopyToAsync(ms);
         File.WriteAllBytesAsync(Path.Combine(Images, folder, fileName), ms.ToArray());
        return $"/{folder}/{fileName}";
    }
}