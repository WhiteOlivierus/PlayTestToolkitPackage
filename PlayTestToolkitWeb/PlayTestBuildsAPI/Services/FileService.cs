﻿using Microsoft.AspNetCore.Http;
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
        public void SaveFiles(IList<IFormFile> files, string subDirectory) =>
            files.ToList().ForEach(file => Create(file, subDirectory));

        public string Create(IFormFile file, string subDirectory)
        {
            subDirectory ??= string.Empty;

            string targetDirectory = Path.Combine("/", subDirectory);

            Directory.CreateDirectory(targetDirectory);

            if (file.Length <= 0)
                return string.Empty;

            string fileName = GetUniqueFileName(file.FileName);
            string filePath = Path.Combine(targetDirectory, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filePath;
        }

        public byte[] Get(string subDirectory)
        {
            string targetDirectory = Path.GetDirectoryName(subDirectory);

            string path = Path.Combine("/", targetDirectory);
            List<string> filePaths = Directory.GetFiles(path).ToList();

            string filePath = (from entry in filePaths
                               where entry.Contains(subDirectory)
                               select entry).FirstOrDefault();

            return File.ReadAllBytes(filePath);
        }

        public (string fileType, byte[] archiveData, string archiveName) FetechFiles(string filePath)
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string zipName = $"archive-{date}.zip";

            List<string> filePaths = Directory.GetFiles(Path.Combine("/", filePath)).ToList();

            using MemoryStream memoryStream = new MemoryStream();
            using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                filePaths.ForEach(filePath =>
                {
                    ZipArchiveEntry theFile = archive.CreateEntry(filePath);
                    using StreamWriter streamWriter = new StreamWriter(theFile.Open());
                    streamWriter.Write(File.ReadAllText(filePath));
                });
            }

            return ("application/zip", memoryStream.ToArray(), zipName);
        }

        public void Delete(string path)
        {
            path ??= string.Empty;

            if (File.Exists(path))
                File.Delete(path);
        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
