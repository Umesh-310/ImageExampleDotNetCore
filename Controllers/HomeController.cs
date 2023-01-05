using ImageExampleDotNetCore.Data;
using ImageExampleDotNetCore.Models;
using ImageExampleDotNetCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageExampleDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var students = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            return View(students);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var students = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            return View(students);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(StudentViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var student = new Student
            {
                Name = vm.Name,
                ProfileImage = stringFileName
            };
            _context.Students.Add(student);
            _context.SaveChanges(); 
            return RedirectToAction("Index");
        }

        private string UploadFile(StudentViewModel vm)
        {
            string fileName = null;
            if(vm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    vm.ProfileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

       
    }
}
