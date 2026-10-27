using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Newtouch.Domain.ViewModels
{
    public class FileUploadModel
    {
        [Required(ErrorMessage = "Vui lòng chọncầnTải lêncủavănmục")]
        public IEnumerable<IFormFile> Files { get; set; }
        public string sqdh { get; set; }
        public string Description { get; set; }
    }
}
