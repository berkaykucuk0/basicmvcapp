using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;
using web.Models.Data;


namespace web.Controllers
{
   
    public class HomeController : Controller
    {   
       
        public ActionResult Index() 
        {
            using (FitnessContext context = new FitnessContext())
            {
                AnaSayfaDTO anaSayfa = new AnaSayfaDTO();
                anaSayfa.slider = context.Slider.Where(x => (x.BaslangicTarih <= DateTime.Now && x.BitisTarih > DateTime.Now)).ToList();  // verilen tarihler arasında slider fotoğrafını listeler
                anaSayfa.duyuru = context.Duyuru.OrderByDescending(x => x.DuyuruTarih).Take(3).ToList();
                anaSayfa.referans = context.Referans.OrderByDescending(x => x.ReferansTarih).Take(3).ToList();
                anaSayfa.blog = context.Blog.OrderByDescending(x => x.BlogTarih).Take(3).ToList();
                return View(anaSayfa);
            }
        }

       
        public ActionResult About()
        {
            using (FitnessContext context = new FitnessContext())
            {
                List<Takim> hakkimizda = context.Takim.OrderBy(x => x.AdSoyad).ToList(); 
                return View(hakkimizda);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "İletişim";

            return View();
        }

        public ActionResult Modules()
        {
            using (FitnessContext context = new FitnessContext())
            {
                List<Modul> moduller = context.Modul.OrderBy(x => x.ModulBaslik).ToList();
                return View(moduller);
            }
        }

        public ActionResult ModuleDetails(int ModulID)
        {
            using (FitnessContext context = new FitnessContext())
            {
                Modul modulDetay = context.Modul.FirstOrDefault(x => x.ID == ModulID);
                return View(modulDetay);
            }
        }

       [Authorize(Roles ="Editor")]
        public ActionResult Blog()
        {
            using (FitnessContext context = new FitnessContext())
            {
                List<Blog> blog = context.Blog.OrderByDescending(x => x.BlogTarih).ToList();
                return View(blog);
            }
        }

        [HttpPost]
        public ActionResult Contact(Oneri iletisimform)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Oneri _iletisimform = new Oneri();
                    _iletisimform.AdSoyad = iletisimform.AdSoyad;
                    _iletisimform.Telefon = iletisimform.Telefon;
                    _iletisimform.Eposta = iletisimform.Eposta;
                    _iletisimform.Mesaj = iletisimform.Mesaj;
                    _iletisimform.Tarih = DateTime.Now;
                    context.Oneri.Add(_iletisimform);
                    context.SaveChanges();
                    TempData["Mesaj"] = "Form Başarıyla gönderilmiştir.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }

        public ActionResult ChangeCulture(string lang, string returnUrl) //Dil değiştirme için hazır controller
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
    }
    public class AnaSayfaDTO  // Slider ve diğerlerini birbirine bağlamak amaçlı 
    {
        public List<Slider> slider { get; set; }
        public List<Duyuru> duyuru { get; set; }
        public List<Referans> referans { get; set; }
        public List<Blog> blog { get; set; }
    }
}