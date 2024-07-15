using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web.Models;

namespace web.DAL
{
    public class Repository
    {
        static List<LoginViewModel> users = new List<LoginViewModel>() // admin tanımlama
        {

            new LoginViewModel() {KullaniciAdi="Berkay",Email="berkaykucuk@gmail.com",Roles="Admin,Editor",Password="123456789" },
            new LoginViewModel() {KullaniciAdi="Enes",Email="enescavus@gmail.com",Roles="Admin,Editor",Password="123456789" },
            new LoginViewModel() {KullaniciAdi="Samet",Email="sametkusbey@gmail.com",Roles="Admin,Editor",Password="123456789" },
            new LoginViewModel() {KullaniciAdi="Mehmet",Email="mehmetsavas@gmail.com",Roles="Editor",Password="123456789" }
        };

        public static LoginViewModel GetUserDetails(LoginViewModel user) // kullanıcı detaylarını alıyor
        {
            return users.Where(u => u.Email.ToLower() == user.Email.ToLower() &&
            u.Password == user.Password).FirstOrDefault();
        }
    }
}