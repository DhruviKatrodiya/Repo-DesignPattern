using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CollegeApplication.Core.Domain.Exceptions;
using CollegeApplication.Infra.Domain;
using CollegeApplication.Infra.Domain.Entity;
using dotenv.net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Core.Service.Helper
{
    public class FileUploadHelper
    {
        private readonly IConfiguration _configuration;
        private readonly CollegeApplicationContext _context;
        public FileUploadHelper(IConfiguration configuration, CollegeApplicationContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles "));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return path + @"\" + file.FileName;
                }
                else
                {
                    throw new BadRequestException("Cv is not uploaded successfully.");
                }


                /*DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
                Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
                cloudinary.Api.Secure = true;
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName.ToString(), file.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = true
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                Console.WriteLine(uploadResult.JsonObj);
                var secureUrl = uploadResult.SecureUrl;
                return secureUrl.ToString();*/

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task DownloadCVFile(int studentId)
        {
            try
            {
                var file = await _context.Students.Where(x => x.Id == studentId).Select(x => x.CvFile).FirstOrDefaultAsync();
                var content = new System.IO.MemoryStream(file.Length);
                var filearr = file.Split("\\");
                var filename = filearr[filearr.Length - 1];

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), @"E:\Bigscal\Asp.net\AllDemoProject\CollegeApplication.API\FileDownloaded\",
                    filename);

                //string physicalPath = path;
                //byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);
                //MemoryStream ms = new MemoryStream(pdfBytes);
                //return new FileStreamResult(ms, "application/pdf");

                await CopyStream(content, path);
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var filestream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(filestream);
            }
        }



    }
}
