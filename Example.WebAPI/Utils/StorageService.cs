using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Example.Application.Interfaces;

namespace Example.WebAPI.Utils
{
    public class StorageDocumentsService : IStorageDocumentsService
    {
        private const string DIRECTORY = "Storage";
        private readonly IWebHostEnvironment _appEnvironment;

        public StorageDocumentsService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment ?? throw new ArgumentNullException(nameof(appEnvironment));
        }

        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="file">Файл</param>
        /// <param name="directory">Путь к подпапке сохранения</param>
        public async Task Load(IFormFile file, string directory = "")
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, DIRECTORY, directory, file.FileName);

            Directory.CreateDirectory(Path.Combine(_appEnvironment.ContentRootPath, DIRECTORY, directory));

            using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        /// <summary>
        /// Получает поток и информацию о скачиваемом файле
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Кортеж из MemoryStream, file_type, file_name</returns>
        public async Task<(MemoryStream, string, string)> Download(string filename)
        {

            if (filename == null)
                throw new FileNotFoundException("Запрашиваемый файл не найден");

            var path = Path.Combine(_appEnvironment.ContentRootPath, DIRECTORY, filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return (memory, GetContentType(path), Path.GetFileName(path));

        }

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="path">Путь к файлу (без корневой директории)</param>
        /// <exception cref="FileNotFoundException">Если указанный файл не найден</exception>
        public void Delete(string path)
        {
            path = Path.Combine(_appEnvironment.ContentRootPath, DIRECTORY, path);
            FileInfo fileInfo = new(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("Запрашиваемый файл не существует");

            fileInfo.Delete();
        }

        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
