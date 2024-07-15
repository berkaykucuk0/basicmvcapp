using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models.Data;

namespace web.Controllers
{


   [Authorize]
    public class AdminController : Controller
    {

        [Authorize(Roles = "Admin")]  // SADECE ADMİN GİRİŞ YAPABİLİR
        public ActionResult Index()
        {
            return View();
        }
       

     
        public ActionResult Slider()  // SLİDER İÇERİKLERİNİ DÜZENLEME 
        {
            using (FitnessContext context = new FitnessContext())
            {
                var slider = context.Slider.ToList();
                return View(slider);
            }
        }
        
        public ActionResult SlideEkle() 
        {
            return View();
        }
        
        public ActionResult SlideDuzenle(int SlideID) // ID sini bulduğu slide ilk sıraya yerleştirir
        {
            using (FitnessContext context = new FitnessContext())
            {
                var _slideDuzenle = context.Slider.Where(x => x.ID == SlideID).FirstOrDefault();
                return View(_slideDuzenle);
            }
        }
        
        public ActionResult SlideSil(int SlideID) // İD sini bulduğu slideı siler admin slidere yönlendir
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    context.Slider.Remove(context.Slider.First(d => d.ID == SlideID));
                    context.SaveChanges();
                    return RedirectToAction("Slider", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
       
        [HttpPost]
        public ActionResult SlideEkle(Slider s, HttpPostedFileBase file)   
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Slider _slide = new Slider();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _slide.SliderFoto = memoryStream.ToArray();  // view den byte olarak alınan fotoğrafı array haline getir verilen tarih aralığında içeriği yayınlar
                    }
                    _slide.SliderText = s.SliderText;
                    _slide.BaslangicTarih = s.BaslangicTarih;
                    _slide.BitisTarih = s.BitisTarih;
                    context.Slider.Add(_slide);
                    context.SaveChanges();
                    return RedirectToAction("Slider", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
      
        [HttpPost]
        public ActionResult SlideDuzenle(Slider slide, HttpPostedFileBase file) // Byte olarak alınan sliderın üzerinde tarihler yazılar olarak değişiklik yapar
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    var _slideDuzenle = context.Slider.Where(x => x.ID == slide.ID).FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _slideDuzenle.SliderFoto = memoryStream.ToArray();
                    }
                    _slideDuzenle.SliderText = slide.SliderText;
                    _slideDuzenle.BaslangicTarih = slide.BaslangicTarih;
                    _slideDuzenle.BitisTarih = slide.BitisTarih;
                    context.SaveChanges();
                    return RedirectToAction("Slider", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

      

      
        public ActionResult Blog() 
        {
            using (FitnessContext context = new FitnessContext())
            {
                var blog = context.Blog.ToList();
                return View(blog);
            }
        }
       
        public ActionResult BlogEkle()
        {
            return View();
        }
       
        public ActionResult BlogDuzenle(int BlogID)
        {
            using (FitnessContext context = new FitnessContext())
            {
                var _blogDuzenle = context.Blog.Where(x => x.ID == BlogID).FirstOrDefault();
                return View(_blogDuzenle);
            }
        }
      
        public ActionResult BlogSil(int BlogID)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    context.Blog.Remove(context.Blog.First(d => d.ID == BlogID));
                    context.SaveChanges();
                    return RedirectToAction("Blog", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
       
        [HttpPost]
        public ActionResult BlogEkle(Blog b, HttpPostedFileBase file) 
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Blog _blog = new Blog();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _blog.BlogFoto = memoryStream.ToArray();
                    }
                    _blog.BlogBaslik = b.BlogBaslik;
                    _blog.BlogIcerik = b.BlogIcerik;
                    _blog.Tarih = DateTime.Now;
                    context.Blog.Add(_blog);
                    context.SaveChanges();
                    return RedirectToAction("Blog", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
       
        [HttpPost]
        public ActionResult BlogDuzenle(Blog b, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    var _blogDuzenle = context.Blog.Where(x => x.ID == b.ID).FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _blogDuzenle.BlogFoto = memoryStream.ToArray();
                    }
                    _blogDuzenle.BlogBaslik = b.BlogBaslik;
                    _blogDuzenle.BlogIcerik = b.BlogIcerik;
                    _blogDuzenle.Tarih = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Blog", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

       

        #region // Takım yani Çalışanların Kontrolleri

        public ActionResult Personel()
        {
            using (FitnessContext context = new FitnessContext())
            {
                var personel = context.Takim.ToList();
                return View(personel);
            }
        }
        public ActionResult PersonelEkle() 
        {
            return View();
        }
        public ActionResult PersonelDuzenle(int PersonelID)
        {
            using (FitnessContext context = new FitnessContext())
            {
                var _personelDuzenle = context.Takim.Where(x => x.ID == PersonelID).FirstOrDefault();
                return View(_personelDuzenle);
            }
        }
        public ActionResult PersonelSil(int PersonelID)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    context.Takim.Remove(context.Takim.First(d => d.ID == PersonelID));
                    context.SaveChanges();
                    return RedirectToAction("Personel", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [HttpPost]
        public ActionResult PersonelEkle(Takim t, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Takim _personel = new Takim();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _personel.Foto = memoryStream.ToArray();
                    }
                    _personel.AdSoyad = t.AdSoyad;
                    _personel.Icerik = t.Icerik;
                    _personel.Tip = t.Tip;
                    context.Takim.Add(_personel);
                    context.SaveChanges();
                    return RedirectToAction("Personel", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [HttpPost]
        public ActionResult PersonelDuzenle(Takim t, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    var _personelDuzenle = context.Takim.Where(x => x.ID == t.ID).FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _personelDuzenle.Foto = memoryStream.ToArray();
                    }
                    _personelDuzenle.AdSoyad = t.AdSoyad;
                    _personelDuzenle.Icerik = t.Icerik;
                    _personelDuzenle.Tip = t.Tip;
                    context.SaveChanges();
                    return RedirectToAction("Personel", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Modül kontrolleri
        public ActionResult Moduller()
        {
            using (FitnessContext context = new FitnessContext())
            {
                var moduller = context.Modul.ToList();
                return View(moduller);
            }
        }
        public ActionResult ModulEkle()
        {
            return View();
        }
        public ActionResult ModulDuzenle(int ModulID)
        {
            using (FitnessContext context = new FitnessContext())
            {
                var _modulDuzenle = context.Modul.Where(x => x.ID == ModulID).FirstOrDefault();
                return View(_modulDuzenle);
            }
        }
        public ActionResult ModulSil(int ModulID)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    context.Modul.Remove(context.Modul.First(d => d.ID == ModulID));
                    context.SaveChanges();
                    return RedirectToAction("Moduller", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [HttpPost]
        public ActionResult ModulEkle(Modul m, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Modul _modul = new Modul();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _modul.ModulFoto = memoryStream.ToArray();
                    }
                    _modul.ModulBaslik = m.ModulBaslik;
                    _modul.ModulIcerik = m.ModulIcerik;
                    _modul.Tarih = DateTime.Now;
                    context.Modul.Add(_modul);
                    context.SaveChanges();
                    return RedirectToAction("Moduller", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [HttpPost]
        public ActionResult ModulDuzenle(Modul m, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    var _modulDuzenle = context.Modul.Where(x => x.ID == m.ID).FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _modulDuzenle.ModulFoto = memoryStream.ToArray();
                    }
                    _modulDuzenle.ModulBaslik = m.ModulBaslik;
                    _modulDuzenle.ModulIcerik = m.ModulIcerik;
                    _modulDuzenle.Tarih = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Moduller", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Duyuruların Kontrolleri

        public ActionResult Duyurular()
        {
            using (FitnessContext context = new FitnessContext())
            {
                var duyurular = context.Duyuru.ToList();
                return View(duyurular);
            }
        }
        public ActionResult DuyuruEkle()
        {
            return View();
        }
        public ActionResult DuyuruDuzenle(int DuyuruID)
        {
            using (FitnessContext context = new FitnessContext())
            {
                var _duyuruDuzenle = context.Duyuru.Where(x => x.ID == DuyuruID).FirstOrDefault();
                return View(_duyuruDuzenle);
            }
        }
        public ActionResult DuyuruSil(int DuyuruID)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    context.Duyuru.Remove(context.Duyuru.First(d => d.ID == DuyuruID));
                    context.SaveChanges();
                    return RedirectToAction("Duyurular", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [HttpPost]
        public ActionResult DuyuruEkle(Duyuru d, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Duyuru _duyuru = new Duyuru();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _duyuru.DuyuruFoto = memoryStream.ToArray();
                    }
                    _duyuru.DuyuruBaslik = d.DuyuruBaslik;
                    _duyuru.DuyuruIcerik = d.DuyuruIcerik;
                    _duyuru.Tarih = DateTime.Now;
                    context.Duyuru.Add(_duyuru);
                    context.SaveChanges();
                    return RedirectToAction("Duyurular", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [HttpPost]
        public ActionResult DuyuruDuzenle(Duyuru d, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext
())
                {
                    var _duyuruDuzenle = context.Duyuru.Where(x => x.ID == d.ID).FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _duyuruDuzenle.DuyuruFoto = memoryStream.ToArray();
                    }
                    _duyuruDuzenle.DuyuruBaslik = d.DuyuruBaslik;
                    _duyuruDuzenle.DuyuruIcerik = d.DuyuruIcerik;
                    _duyuruDuzenle.Tarih = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Duyurular", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Referansların Kontrolleri

        public ActionResult Referanslar()
        {
            using (FitnessContext context = new FitnessContext())
            {
                var referanslar = context.Referans.ToList();
                return View(referanslar);
            }
        }
        public ActionResult ReferansEkle()
        {
            return View();
        }
        public ActionResult ReferansDuzenle(int ReferansID)
        {
            using (FitnessContext context = new FitnessContext())
            {
                var _referansDuzenle = context.Referans.Where(x => x.ID == ReferansID).FirstOrDefault();
                return View(_referansDuzenle);
            }
        }
        public ActionResult ReferansSil(int ReferansID)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    context.Referans.Remove(context.Referans.First(d => d.ID == ReferansID));
                    context.SaveChanges();
                    return RedirectToAction("Referanslar", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [HttpPost]
        public ActionResult ReferansEkle(Referans r, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    Referans _referans = new Referans();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _referans.ReferansFoto = memoryStream.ToArray();
                    }
                    _referans.ReferansBaslik = r.ReferansBaslik;
                    _referans.ReferansIcerik = r.ReferansIcerik;
                    _referans.Tarih = DateTime.Now;
                    context.Referans.Add(_referans);
                    context.SaveChanges();
                    return RedirectToAction("Referanslar", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [HttpPost]
        public ActionResult ReferansDuzenle(Referans r, HttpPostedFileBase file)
        {
            try
            {
                using (FitnessContext context = new FitnessContext())
                {
                    var _referansDuzenle = context.Referans.Where(x => x.ID == r.ID).FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _referansDuzenle.ReferansFoto = memoryStream.ToArray();
                    }
                    _referansDuzenle.ReferansBaslik = r.ReferansBaslik;
                    _referansDuzenle.ReferansIcerik = r.ReferansIcerik;
                    _referansDuzenle.Tarih = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Referanslar", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion
    }
}