using AutoMapper;
using ITG.Entities.Concrete;
using ITG.Entities.Dtos;
using ITG.Mvc.Areas.Admin.Models;
using ITG.Shared.Utilities.Extensions;
using ITG.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;



        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success

            });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-mail veya şifreniz yanlış!");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-mail veya şifreniz yanlış!");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }

        }

        /// <summary>
        /// This is Logout Action
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        /// <summary>
        /// Yenile butonu için oluşturulmuş bir metottur. 
        /// </summary>
        /// <returns>userListDto</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success

            }, new JsonSerializerOptions
            {

                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        /// <summary>
        /// Kullanıcı Ekleme için Oluşturulan PartialView
        /// </summary>
        /// <returns>"_UserAddPartial"</returns>
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName, userAddDto.PictureFile);
                var user = _mapper.Map<User>(userAddDto);
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
                        ModelState.AddModelError("", error.Description);
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
        /// This is the 403 Error method. 
        /// </summary>
        /// <returns>AccessDenied.cshtml</returns>
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// This is Delete method for the users.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla silinmiştir.",
                    User = user
                });
                return Json(deletedUser);
            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessages = $"*{error.Description}\n";
                }
                var deletedUserModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı silinirken bazı hatalar oluştu.\n{errorMessages}",
                    User = user
                });
                return Json(deletedUserModel);
            }
        }
        /// <summary>
        /// This is HttpGet Update Action for the User info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>_UserUpdatePartial</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }

        /// <summary>
        /// This is the HtpPost Update Action for the User info
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPictureUploaded = true;
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        ImageDelete(oldUserPicture);
                    }
                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateErrorViewModel);
                }
            }
            else
            {
                var userUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateModelStateErrorViewModel);
            }

        }

        /// <summary>
        /// This View Result allows users to edit their own information.
        /// </summary>
        /// <returns>View(updateDto)</returns>
        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        /// <summary>
        /// This View Result allows users to edit their own information.
        /// This is Post Action.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    if (oldUserPicture != "defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }
                    
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        ImageDelete(oldUserPicture);
                    }
                    TempData.Add("SuccessMessage", $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDto);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userUpdateDto);
                }
            }
            else
            {
                return View(userUpdateDto);
            }

        }
        /// <summary>
        /// This is User Password Change Action.
        /// This action use three property; current password, new password and repeat password.
        /// </summary>
        /// <returns>PasswordChangeView</returns>
        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }
        /// <summary>
        /// This is User Password Change Action.
        /// This action use three property; current password, new password and repeat password.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        TempData.Add("SuccessMessage", $"Şifreniz başarıyla değiştirilmiştir!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen girmiş olduğunuz şu anki şifreyi kontrol ediniz.");
                    return View(userPasswordChangeDto);
                }
            }
            else
            {
                return View(userPasswordChangeDto);
            }
            return View();
            
        }


        /// <summary>
        /// Image Upload Process for the Users
        /// </summary>
        /// <param name="userAddDto"></param>
        /// <returns>fileName</returns>
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<string> ImageUpload(string userName, IFormFile pictureFile)
        {
            //Bu işlem bize string olarak wwwroot dosya yolunu dinamik bir şekilde verecektir.
            string wwwroot = _env.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.PictureFile.FileName); //Bu işlem dosyayı sonundaki uzantı olmadan almamı sağlayacak.jpg veya png olması fark yaratmayacak kısaca.
            string fileExtension = Path.GetExtension(pictureFile.FileName); //artık dosyanın uzantısını da almış olduk.
            DateTime dateTime = DateTime.Now;
            string fileName = $"{userName}_{dateTime.FullDateandTimeStringWithUnderscore()}{fileExtension}";  //YusufKaraman_587_5_38_12_3_11_2021 şeklinde dosya adı oluşturuluyor.
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName;  //YusufKaraman_587_5_38_12_3_11_2021 -  "~/img/user.Picture"
        }

        /// <summary>
        /// This Image Delete Method for User image process
        /// </summary>
        /// <param name="pictureName"></param>
        /// <returns> true or false</returns>
        [Authorize(Roles = "Admin,PowerUser")]
        public bool ImageDelete(string pictureName)
        {

            string wwwroot = _env.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
