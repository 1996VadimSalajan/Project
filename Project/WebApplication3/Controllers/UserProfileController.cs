using CodeFirst;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using PagedList;
using WebApplication3.DTO;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;

namespace WebApplication3.Controllers
{   [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IRepository<User> repuser;
        private readonly IRepository<Address> repaddress;

        public UserProfileController(IRepository<User> repuser, IRepository<Address> repaddress)
        {
            this.repuser = repuser;
            this.repaddress = repaddress;
        }
       
        public ActionResult Index(string searchString, string sortOption, int page = 1)
        {
            int pageSize = 10;
            var users = repuser.Read().AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = repuser.Read().Where(u => u.UserName.ToLower().Contains(searchString));
            }
            switch (sortOption)
            {
                case "name_acs":
                    users = users.OrderBy(p => p.UserName);
                    break;
                case "name_desc":
                    users = users.OrderByDescending(p => p.Email);
                    break;
                default:
                    users = users.OrderBy(p => p.Id);
                    break;

            }
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("UserNameList", users.ToPagedList(page, pageSize))
                : View(users.ToPagedList(page, pageSize));
        }

        public JsonResult List()
        {
            var list = from l in repaddress.Read() select new { l.Id, l.Village, l.City, l.Country, l.UserId };
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Address address)
        {
           
                var userid = User.Identity.GetUserId();
                address.UserId = userid;
                if (address.Village == null)
                    address.Village = " ";
                repaddress.Insert(address);
                repaddress.Save();
                return Json(address, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var address = repaddress.Read().FirstOrDefault(x => x.Id.Equals(ID));
            var coursDto = Mapper.Map<Address, AddressDTO>(address);
            return Json(coursDto, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Address address)
        {
            if (address.Village == null)
                address.Village = " ";
            repaddress.Update(address);
            repaddress.Save();
            return Json(address, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            Address address = new Address()
            {
                Id = ID
            };
            repaddress.Delete(address);
            repaddress.Save();
            return Json(address, JsonRequestBehavior.AllowGet);
        }
        public new ActionResult Profile()
        {
            return View();
        }
     
        public ActionResult DetailsUser()
        {           
            return PartialView();
        }
        [HttpGet]
        public ActionResult Address()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DisplayBirthdate()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult DisplayBirthdate(User user)
        {
            repuser.Insert(user);
            return View(user);
        }


        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  ActionResult AddPhoto(string id)
        {
            HttpPostedFileBase poImgFile = Request.Files["Photo"];
            if (poImgFile == null || poImgFile.ContentLength==0)
            {
                ModelState.AddModelError("Photo", "Please upload a Image!");
            }
            if (poImgFile.ContentLength > 10 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "This Image is too big!");
            }
            string ext = Path.GetExtension(poImgFile.FileName);
            if (String.IsNullOrEmpty(ext) ||
               !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Photo", "This file is not a Image format Jpg!");
            }

            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var user = repuser.Read().Where(u => u.Id == userid).FirstOrDefault();

                user.Photo = new byte[poImgFile.ContentLength];
                poImgFile.InputStream.Read(user.Photo, 0, poImgFile.ContentLength);
                repuser.Update(user);
                repuser.Save();
                return RedirectToAction("Index", "Manage");
            }
            return View();
        }
        public FileContentResult DisplayPhoto()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    return File(imageData, "image/png");

                }
                var userImage =repuser.Read().Where(x => x.Id == userId).FirstOrDefault();
                if (userImage.Photo != null)
                    return new FileContentResult(userImage.Photo, "image/jpg");
                return null;
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }

    }
}