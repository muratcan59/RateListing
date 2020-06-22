using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateListing.Model
{
    public class Enums
    {
        public enum UyelikTipi
        {
            Normal,
            Gold
        }

        public enum OdemeTuru
        {
            Nakit,
            KrediKartı
        }

        public enum Gunler
        {
            Pazartesi = 1,
            Salı = 2,
            Çarşamba = 3,
            Perşembe = 4,
            Cuma = 5,
            Cumartesi = 6,
            Pazar = 7
        }

        public enum CompanyType
        {
            [Display(Name = "Şahıs Şirketi")]
            Sahis = 1,
            [Display(Name = "Limited Şirketi")]
            Limited = 2,
            [Display(Name = "Anonim Şirketi")]
            Anonim = 3
        }
    }
}