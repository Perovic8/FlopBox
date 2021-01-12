using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FlopBox.Context;
using FlopBox.Models;
using FlopBox.Services;

namespace FlopBox.Controllers.API
{
    public class FoldersController : ApiController
    {
        private FlopBoxdbContext _context;

        public FoldersController()
        {
            _context = new FlopBoxdbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // POST: api/folders/createfolder
        [HttpPost]
        public IHttpActionResult CreateFolder(string folderName, string subfolderName = null)
        {
            var folderFromDB = _context.Folders.SingleOrDefault(x => x.Name == folderName);

            Folder subfolderFromDB = null;

            if (subfolderName != null)
            {
                subfolderFromDB = _context.Folders.SingleOrDefault(x => x.Name == subfolderName);
            }

            // subfolder doesn't exists, create one
            if (folderFromDB != null && subfolderFromDB == null && subfolderName != null)
            {
                var newSubFolder = FolderService.CreateSubFolder(_context, subfolderName, folderFromDB);
                return Created("", newSubFolder);
            }

            // folder doesn't exist
            if (folderFromDB == null && subfolderFromDB != null)
            {
                return BadRequest("Parent folder doesn't exist.");
            }

            // folder and subfolder already exist
            if (folderFromDB != null && subfolderFromDB != null)
            {
                return BadRequest("Parent folder and subfolder already exists.");
            }

            // folder and subfolder doesn't exist 
            else
            {
                var newFolder = FolderService.CreateNewFolder(_context, folderName);
                var newSubFolder = FolderService.CreateSubFolder(_context, subfolderName, newFolder);

                var listOfFolders = new List<Folder>();
                listOfFolders.Add(newFolder);
                listOfFolders.Add(newSubFolder);

                return Created("", listOfFolders);
            }
        }

        // api/folder/deletefolder
        [HttpGet]
        public IHttpActionResult DeleteFolder(string folderName)
        {
            var folder = _context.Folders.SingleOrDefault(x => x.Name == folderName);

            if (folder != null)
            {
                FolderService.DeleteFolder(_context, folder);
                
                return Ok("Folder, subfolders and files in associated folders are deleted.");
            }

            return BadRequest("Folder with provided name doesn't exist.");
        }
    }
}
