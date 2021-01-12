using FlopBox.Context;
using FlopBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FlopBox.Services
{
    public class FolderService
    {
        // create folder
        internal static Folder CreateNewFolder(FlopBoxdbContext _context, string folderName)
        {
            var newFolder = new Folder();
            newFolder.Name = folderName;
            newFolder.ParentFolder = null;

            _context.Folders.Add(newFolder);
            _context.SaveChanges();

            return newFolder;
        }

        // create subfolder
        internal static Folder CreateSubFolder(FlopBoxdbContext _context, string subfolderName, Folder newCreatedFolder)
        {
            var newSubfolder = new Folder();
            newSubfolder.Name = subfolderName;
            newSubfolder.ParentFolder = newCreatedFolder.Id;

            _context.Folders.Add(newSubfolder);
            _context.SaveChanges();

            return newSubfolder;
        }

        internal static void DeleteFolder(FlopBoxdbContext _context, Folder folder)
        {
            // find all child folders of passed argument folder
            var childFolders = _context.Folders.Where(x => x.ParentFolder == folder.Id).ToList();
                        
            if (childFolders.Any() && childFolders.Count() > 0)
            {

                foreach (var child in childFolders)
                {
                    var files = _context.Files.Where(x => x.Folder == child.Id).ToList();
                            
                    // if exist, delete all files in child folders
                    if (files.Any() && files.Count() > 0)
                    {
                        foreach (var file in files)
                        {
                            _context.Files.Remove(file);
                            //_context.SaveChanges();
                        }
                    }

                    // delete child folder
                    _context.Folders.Remove(child);
                    //_context.SaveChanges();
                }
            }

            // finally delete files in "main" folder
            var folderFiles = _context.Files.Where(x => x.Folder == folder.Id).ToList();

            if (folderFiles.Any() && folderFiles.Count() > 0)
            {
                foreach (var file in folderFiles)
                {
                    _context.Files.Remove(file);
                }
            }

            _context.Folders.Remove(folder);
            _context.SaveChanges();
        }
    }
}