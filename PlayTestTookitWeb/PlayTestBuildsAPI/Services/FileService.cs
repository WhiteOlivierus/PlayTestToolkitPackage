using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PlayTestBuildsAPI.Services
{
    // https://tocalai.medium.com/upload-download-file-s-in-asp-net-core-1fa89166aab0
    public class FileService
    {

        public void SaveFiles(List<IFormFile> files, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;

            string target = Path.Combine("/", subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(file =>
            {
                SaveFile(file, subDirectory);
            });
        }

        public string SaveFile(IFormFile file, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;

            string target = Path.Combine("/", subDirectory);

            Directory.CreateDirectory(target);

            if (file.Length <= 0) return string.Empty;
            string filePath = Path.Combine(target, GetUniqueFileName(file.FileName));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filePath;
        }

        public (string fileType, byte[] archiveData) FetechFile(string subDirectory)
        {
            string folder = Path.GetDirectoryName(subDirectory);
            var files = Directory.GetFiles(Path.Combine("/", folder)).ToList();

            var file = (from f in files
                        where f.Contains(subDirectory)
                        select f).FirstOrDefault();

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(File.ReadAllBytes(file));
                }
                return ("application/zip", memoryStream.ToArray());
            }
        }

        public (string fileType, byte[] archiveData, string archiveName) FetechFiles(string filePath)
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            var files = Directory.GetFiles(Path.Combine("/", filePath)).ToList();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    files.ForEach(file =>
                    {
                        var theFile = archive.CreateEntry(file);
                        using (var streamWriter = new StreamWriter(theFile.Open()))
                        {
                            streamWriter.Write(File.ReadAllText(file));
                        }

                    });
                }

                return ("application/zip", memoryStream.ToArray(), zipName);
            }

        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public static string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }
    }
}
