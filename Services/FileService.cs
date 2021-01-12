using FlopBox.Context;
using FlopBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FlopBox.Services
{
    public class FileService
    {
        // create file
        internal static File CreateFile(FlopBoxdbContext _context, Folder folder, string fileName)
        {
            var newFile = new File();
            newFile.Name = fileName;
            newFile.Folder = folder.Id;

            _context.Files.Add(newFile);
            _context.SaveChanges();

            return newFile;
        }

        // get top 10 files that start with string argument provided
        internal static List<File> FileSearh(FlopBoxdbContext _context, string fileName)
        {
            var files = _context.Files.ToList();

            foreach (var x in files)
            {
                var y = x.Folder;
            }

            return (_context.Files.Where(x => x.Name.StartsWith(fileName)).Take(10)).ToList();
        }

        internal static bool Delete(FlopBoxdbContext _context, string fileName)
        {
            var file = _context.Files.SingleOrDefault(x => x.Name == fileName);

            if (file != null)
            {
                _context.Files.Remove(file);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}