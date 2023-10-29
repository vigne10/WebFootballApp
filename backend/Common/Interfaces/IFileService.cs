namespace WebFootballApp.Common.Interfaces;

public interface IFileService
{
    Task<string> SaveImageAsync(IFormFile? file, string category);
    Task DeleteImageAsync(string imagePath, string category);
}