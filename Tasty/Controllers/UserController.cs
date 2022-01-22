using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Models;
using Tasty.Models.Interfaceses;
using Tasty.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Tasty.Controllers
{
    public class UserController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private IShopRepository shopRepository;
        public int PageSize = 10;

        public UserController(RoleManager<IdentityRole> rlMgr, IShopRepository repo, UserManager<AppUser> usrMgr, IUserValidator<AppUser> userValid, IPasswordValidator<AppUser> passValid, IPasswordHasher<AppUser> passwordHash)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            shopRepository = repo;
            roleManager = rlMgr;
        }

        [Authorize(Roles = "Administratorzy")]
        public IActionResult ChangeUserShops(string id)
        {
            var user = userManager.Users
                .Include(u => u.Shops)
                .FirstOrDefault(u => u.Id.Equals(id));
            if (user != null)
            {
                return View(new ChangeUserShopsViewModel
                {
                    Id = id,
                    Shops = new SelectList(shopRepository.Shops, "ShopId", "Name"),
                    SelectedShops = user.Shops.Select(s => s.ShopId)
                });
            }
            else
            {
                return RedirectToAction(nameof(Users));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administratorzy")]
        public async Task<IActionResult> ChangeUserShops(ChangeUserShopsViewModel model)
        {
            AppUser user = await userManager.Users
                .Include(u => u.Shops)
                .FirstOrDefaultAsync(u => u.Id.Equals(model.Id));
            if (user != null)
            {
                user.Shops.Clear();
                user.Shops = shopRepository.Shops.Where(c => model.SelectedShops.Contains(c.ShopId)).ToList();
                IdentityResult result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    AddErrorsFromResult(result);
                }
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Users));
                }
            }
            model.Shops = new SelectList(shopRepository.Shops, "ShopId", "Name");
            return View(model);
        }

        [Authorize(Roles = "Administratorzy")]
        public async Task<IActionResult> ChangeUserRoles(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            if (user != null)
            {
                return View(new ChangeUserRolesViewModel
                {
                    Id = id,
                    Roles = new SelectList(roleManager.Roles, "Name", "Name"),
                    SelectedRoles = roles
                });
            }
            else
            {
                return RedirectToAction(nameof(Users));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administratorzy")]
        public async Task<IActionResult> ChangeUserRoles(ChangeUserRolesViewModel model)
        {
            IdentityResult result;
            AppUser user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var oldRoles = await userManager.GetRolesAsync(user);
                result = await userManager.RemoveFromRolesAsync(user, oldRoles);
                if (!result.Succeeded)
                {
                    AddErrorsFromResult(result);
                }
                result = await userManager.AddToRolesAsync(user, model.SelectedRoles);
                if (!result.Succeeded)
                {
                    AddErrorsFromResult(result);
                }
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Users));
                }
            }
            model.Roles = new SelectList(roleManager.Roles, "Name", "Name");
            return View(model);
        }

        [Authorize(Roles = "Administratorzy")]
        public async Task<IActionResult> Users(int page = 1)
        {
            var users = userManager.Users
                .Include(u => u.Shops)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(u => new UsersViewModel
            {
                Id = u.Id,
                Login = u.UserName,
                Email = u.Email,
                Roles = string.Join(", ", userManager.GetRolesAsync(u).Result.ToArray()),
                Shops = u.Shops
            });
            return View(new UsersWithPagesViewModel
            {
                Users = users,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = await userManager.Users.CountAsync()
                }
            });
        }

        [Authorize]
        public async Task<IActionResult> UserDetails(string name)
        {
            AppUser user = await userManager.FindByNameAsync(name);
            if(User.Identity.Name.Equals(user.UserName))
            {
                return View(user);
            }
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        [Authorize]
        public async Task<IActionResult> EditUserDetails(string id, string returnUrl)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (User.Identity.Name.Equals(user.UserName) || User.IsInRole("Administratorzy"))
                {
                    ViewBag.ReturnUrl = returnUrl ?? "";
                    return View(user);
                }
                else
                {
                    return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
                }       
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUserDetails(string id, string email, string password, string zip, string city, string street, string name, string phoneNumber, string returnUrl)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (User.Identity.Name.Equals(user.UserName) || User.IsInRole("Administratorzy"))
                {
                    user.Zip = zip;
                    user.City = city;
                    user.Name = name;
                    user.Street = street;
                    user.PhoneNumber = phoneNumber;
                    user.Email = email;
                    IdentityResult validData = await userValidator.ValidateAsync(userManager, user);
                    if (!validData.Succeeded)
                    {
                        AddErrorsFromResult(validData);
                    }

                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(password))
                    {
                        validPass = await passwordValidator.ValidateAsync(userManager,
                            user, password);
                        if (validPass.Succeeded)
                        {
                            user.PasswordHash = passwordHasher.HashPassword(user,
                                password);
                        }
                        else
                        {
                            AddErrorsFromResult(validPass);
                        }
                    }

                    if ((validData.Succeeded && validPass == null) || (validData.Succeeded && password != string.Empty && validPass.Succeeded))
                    {
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                else
                {
                    return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
                }
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika.");
            }
            ViewBag.ReturnUrl = returnUrl ?? "";
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Users));
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika.");
            }
            return View("Users");
        }

        public ViewResult CreateUser(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl ?? "";
            return View();
        }
        

        [HttpPost]  
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Login,
                    Name = model.Name,
                    Email = model.Email,
                    Zip = model.Zip,
                    City = model.City,
                    Street = model.Street,
                    PhoneNumber = model.Phone
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, "Użytkownicy");
                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            ViewBag.ReturnUrl = model.ReturnUrl ?? "";
            return View(model);
        }
    }
}
