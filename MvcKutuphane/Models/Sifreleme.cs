using System;
using System.Security.Cryptography;
using System.Text;

namespace MvcKutuphane.Models
{
    // Şifreler PBKDF2 (SHA-256, 100.000 tur, rastgele tuz) ile "p1$tuz$özet" biçiminde saklanıyor.
    // Eski kayıtlar düz metin; girişte doğrulanıp bu biçime çevriliyor (bkz. Giris).
    public static class Sifreleme
    {
        const string Onek = "p1$";
        const int Tur = 100000;

        public static string Ozetle(string sifre)
        {
            var tuz = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(tuz);
            return Onek + Convert.ToBase64String(tuz) + "$" + Convert.ToBase64String(Turet(sifre, tuz));
        }

        public static bool YeniBicimde(string kayitli) => kayitli != null && kayitli.StartsWith(Onek, StringComparison.Ordinal);

        public static bool Dogrula(string sifre, string kayitli)
        {
            if (string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(kayitli)) return false;
            if (!YeniBicimde(kayitli)) return SabitSureliEsit(Encoding.UTF8.GetBytes(sifre), Encoding.UTF8.GetBytes(kayitli));

            var parcalar = kayitli.Substring(Onek.Length).Split('$');
            if (parcalar.Length != 2) return false;
            try
            {
                return SabitSureliEsit(Turet(sifre, Convert.FromBase64String(parcalar[0])), Convert.FromBase64String(parcalar[1]));
            }
            catch (FormatException)
            {
                return false;
            }
        }

        static byte[] Turet(string sifre, byte[] tuz)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(sifre, tuz, Tur, HashAlgorithmName.SHA256))
                return pbkdf2.GetBytes(32);
        }

        static bool SabitSureliEsit(byte[] a, byte[] b)
        {
            var fark = a.Length ^ b.Length;
            for (var i = 0; i < Math.Min(a.Length, b.Length); i++) fark |= a[i] ^ b[i];
            return fark == 0;
        }
    }
}
