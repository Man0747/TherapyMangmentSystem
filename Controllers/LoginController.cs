using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyMangmentSystem.Models;
using TherapyMangmentSystem.Services;

namespace TherapyMangmentSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult SignIn()
        {
            ClaimsPrincipal claimUser = User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginmodel)
        {

            LoginOPS loginops = new LoginOPS();
            string pass = loginmodel.Password;
            loginmodel = loginops.CheckUser(loginmodel);
            HttpContext.Session.SetInt32("Id", loginmodel.User_Id);
            if (loginmodel.Password == pass)
            {
                if (loginmodel.Role == "patient")
                {
                    if (!string.IsNullOrEmpty(loginmodel.Login_Id))
                    {
                        List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, loginmodel.Login_Id),
                                        new Claim(ClaimTypes.Name, loginmodel.User_Id.ToString()),
                                        new Claim(ClaimTypes.Role,"patient")
                                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = false
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Index", "Patient");
                    }

                }
                else if (loginmodel.Role == "therapist")
                {
                    if (!string.IsNullOrEmpty(loginmodel.Login_Id))
                    {
                        List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, loginmodel.Login_Id),
                                        new Claim(ClaimTypes.Name, loginmodel.User_Id.ToString()),
                                        new Claim(ClaimTypes.Role,"therapist")
                                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = false
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Index", "Therapist");
                    }
                }
                else if (loginmodel.Role == "admin")
                {
                    if (!string.IsNullOrEmpty(loginmodel.Login_Id))
                    {
                        List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, loginmodel.Login_Id),
                                        new Claim(ClaimTypes.Name, loginmodel.User_Id.ToString()),
                                        new Claim(ClaimTypes.Role,"admin")
                                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = false
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            else
            {
                ViewData["ValidateMessage"] = "user not found";
                return View();
            }

            return View();
        }
        public async Task<IActionResult> SignUp(LoginModel loginmodel)
        {
            LoginOPS loginops = new LoginOPS();
            HttpContext.Session.SetString("Login", loginmodel.Login_Id);
            if (loginops.AddUser(loginmodel))
            {
                ViewBag.Message = "Therapist Details Added Successfully";
                ModelState.Clear();

                if (loginmodel.Role == "patient")
                {
                    if (!string.IsNullOrEmpty(loginmodel.Login_Id))
                    {
                        List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, loginmodel.Login_Id),
                                        new Claim(ClaimTypes.Name, loginmodel.User_Id.ToString()),
                                        new Claim(ClaimTypes.Role,"patient")
                                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = false
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Create", "Patient");
                    }

                }
                else if (loginmodel.Role == "therapist")
                {
                    if (!string.IsNullOrEmpty(loginmodel.Login_Id))
                    {
                        List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, loginmodel.Login_Id),
                                        new Claim(ClaimTypes.Name, loginmodel.User_Id.ToString()),
                                        new Claim(ClaimTypes.Role,"therapist")
                                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = false
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Create", "Therapist");
                    }
                }
                else if (loginmodel.Role == "admin")
                {
                    if (!string.IsNullOrEmpty(loginmodel.Login_Id))
                    {
                        List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, loginmodel.Login_Id),
                                        new Claim(ClaimTypes.Name, loginmodel.User_Id.ToString()),
                                        new Claim(ClaimTypes.Role,"admin")
                                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            AllowRefresh = false
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else {
                ViewData["ValidateMessage"] = "user not found";
                return RedirectToAction("SignIn", "Login");
            }
            return View();  
        }
    }
}
