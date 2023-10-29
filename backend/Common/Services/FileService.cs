using WebFootballApp.Common.Interfaces;

namespace WebFootballApp.Common.Services;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SaveImageAsync(IFormFile file, string category)
    {
        if (file == null || file.Length <= 0) return null;

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(GetFolderPathForCategory(category), fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task DeleteImageAsync(string imagePath, string category)
    {
        if (string.IsNullOrEmpty(imagePath)) return;

        var fullPath = Path.Combine(GetFolderPathForCategory(category), imagePath);

        if (File.Exists(fullPath)) File.Delete(fullPath);
    }

    private string GetFolderPathForCategory(string category)
    {
        var folderPath = _configuration.GetValue<string>("FilesPaths:Images");
        var categoryPath = _configuration.GetValue<string>($"FilesCategories:{category}");
        var path = Path.Combine(folderPath, categoryPath);

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        return path;
    }
}