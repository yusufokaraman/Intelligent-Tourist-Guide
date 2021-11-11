using AutoMapper;
using ITG.Entities.Concrete;
using ITG.Entities.Dtos;
using ITG.Mvc.Areas.Admin.Models;
using ITG.Shared.Utilities.Extensions;
using ITG.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITG.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;



        public UserController(UserManager<User> userManager,  IWebHostEnvironment env, IMapper mapper)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
           
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success

            });
        }


        /// <summary>
        /// Yenile butonu için oluşturulmuş bir metottur. 
        /// </summary>
        /// <returns>userListDto</returns>
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto=JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success

            }, new JsonSerializerOptions { 
            
                ReferenceHandler=ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        /// <summary>
        /// Kullanıcı Ekleme için Oluşturulan PartialView
        /// </summary>
        /// <returns>"_UserAddPartial"</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        /// <summary>
        /// Kullanıcı ekleme işlemleri Post metodu
        /// </summary>
        /// <param name="userAddDto"></param>
        /// <returns>userAddAjaxModel</returns>
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto);
                var user= _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);
                }
               

            }
            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);

        }


        /// <summary>
        /// Kullanıcı için görsel yükleme işlemi
        /// </summary>
        /// <param name="userAddDto"></param>
        /// <returns>fileName</returns>
        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            //Bu işlem bize string olarak wwwroot dosya yolunu dinamik bir şekilde verecektir.
            string wwwroot = _env.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.PictureFile.FileName); //Bu işlem dosyayı sonundaki uzantı olmadan almamı sağlayacak.jpg veya png olması fark yaratmayacak kısaca.
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName); //artık dosyanın uzantısını da almış olduk.
            DateTime dateTime = DateTime.Now;
            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateandTimeStringWithUnderscore()}{fileExtension}";  //YusufKaraman_587_5_38_12_3_11_2021 şeklinde dosya adı oluşturuluyor.
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }
            return fileName;  //YusufKaraman_587_5_38_12_3_11_2021 -  "~/img/user.Picture"
        }
    }
}
