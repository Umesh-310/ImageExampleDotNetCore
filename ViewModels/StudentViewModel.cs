
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageExampleDotNetCore.ViewModels
{
    public class StudentViewModel
    {
        public string Name { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile ProfileImage { get; set; }
    }
}
