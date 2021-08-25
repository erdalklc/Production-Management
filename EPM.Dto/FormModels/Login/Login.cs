using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Dto.FormModels.Login
{
    public class Login
    {
        [Required(ErrorMessage = "E-Mail Bilgisi Gereklidir")]
        [RegularExpression(@"^[\d\w\._\-]+@([\d\w\._\-]+\.)+[\w]+$", ErrorMessage = "E-Mail Geçerli Değil. Lütfen Kontrol Ediniz")]
        [Display(Name ="E-Mail")]
        public string EMail { get; set; } 
        [Display(Name ="Şifre")]
        [Required(ErrorMessage = "Şifre Bilgisi Gereklidir")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool BeniHatirla { get; set; }

        public string Message { get; set; }
    }
}
