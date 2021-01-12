using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlopBox.Context;
using FlopBox.Models;
using FlopBox.Services;

namespace FlopBox.Controllers.API
{
    public class FilesController : ApiController
    {
        private FlopBoxdbContext _context;

        public FilesController()
        {
            _context = new FlopBoxdbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // POST: api/files/createfile
        [HttpPost]
        public IHttpActionResult CreateFile(string fileName, string folderName)
        {
            var folder = _context.Folders.SingleOrDefault(x => x.Name == folderName);

            if (folder == null)
                return BadRequest("Folder with provided name doesn't exists.");

            var newCreatedFile = FileService.CreateFile(_context, folder, fileName);

            return Created("", newCreatedFile);
        }

        // GET: api/files/searh
        [HttpGet]
        public IHttpActionResult Search(string fileName)
        {
            List<File> listOfFoles = FileService.FileSearh(_context, fileName);

            return Ok(listOfFoles);        
        }

        // GET: api/files/delete
        [HttpGet]
        [Route("api/files/delete")]
        public IHttpActionResult Delete(string fileName)
        {
            if (FileService.Delete(_context, fileName) == true)
                return Ok("File deleted.");

            return BadRequest("Failed to delete file: file with provided name doesn't exist.");
        }
    }
}