using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Utility.Jwt;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace Application.Utility.File
{
    public class FileHelperService
    {

        private readonly FileSettings _settings;
        private readonly IFileService _file;
        private string _storagePath = string.Empty;
        private long _maxFileSize = 0;
        public FileHelperService(IOptions<FileSettings> settings, IFileService file)
        {
            _settings = settings.Value;
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), _settings.uploadPath);
            _maxFileSize = Convert.ToInt32(_settings.maxSize) * 1024 * 1024;
            _file = file;

        }

        public async Task<Files> UploadFile(IFormFile file)
        {

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
            if (CheckMaxSize(file))
            {
                var sanitizedFileName = SanitizeFileName(file.FileName);
                var filePath = Path.Combine(_storagePath, sanitizedFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    var uFile = await _file.AddAsync(new Domain.Entities.Files
                    {
                        fid = Guid.NewGuid(),
                        fileName = file.FileName,
                        filePath = _settings.uploadPath + "/" + sanitizedFileName,
                        fileType = Path.GetExtension(filePath)

                    });

                    return uFile;
                }
            }
            return null;

        }
        public async Task<(string FileName, byte[] FileBytes)> download(string fid)
        {

            var file = (await _file.GetByConditionAsync(x => x.fid == Guid.Parse(fid))).FirstOrDefault();
            if (file!= null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.filePath);
                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return (file.fileName, fileBytes);

                }

                return ("File Not Found",null);
               
            }
            return ("File Not Found", null);

        }

        public bool CheckMaxSize(IFormFile file)
        {
            if (file.Length > _maxFileSize)
            {
                return false; // File is too large
            }

            return true; // File size is valid

        }

        public static string SanitizeFileName(string fileName)
        {
            // Get the file extension
            string extension = Path.GetExtension(fileName);

            // Get the file name without extension
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            // Remove special characters using a regular expression
            string sanitizedName = Regex.Replace(nameWithoutExtension, @"[^a-zA-Z0-9_\-]", "_");

            // Append the current timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string sanitizedFileName = $"{sanitizedName}_{timestamp}{extension}";

            return sanitizedFileName;
        }
    }
}
