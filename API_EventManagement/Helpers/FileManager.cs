namespace API_EventManagement.Helpers
{
    public static class FileManager
    {
        public static string SaveImage(this IFormFile file, string path)
        {

            string NewFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path, NewFileName);
            using var fileStream = new FileStream(root, FileMode.Create);
            file.CopyTo(fileStream);
            return NewFileName;
        }
    }
}
