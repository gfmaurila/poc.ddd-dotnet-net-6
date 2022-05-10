using System.Text.RegularExpressions;

namespace Demo.Domain.Validators
{
    public static class PasswordValidator
    {

        public static bool RequireDigit(string password)
        {
            if (password.Any(x => char.IsDigit(x)))
            {
                return true;
            }
            return false;
        }

        public static bool RequiredLowerCase(string password)
        {
            if (password.Any(x => char.IsLower(x)))
            {
                return true;
            }
            return false;
        }

        public static bool RequireUppercase(string password)
        {
            if (password.Any(x => char.IsUpper(x)))
            {
                return true;
            }
            return false;
        }

        public static bool RequireNonAlphanumeric(string password)
        {
            if (Regex.IsMatch(password, "(?=.*[@#$%^&+=])"))
            {
                return true;
            }
            return false;
        }

        public static bool RequireNotRepeated(string password)
        {
            bool flag = true;
            password = password.ToUpper();
            if (password.Length < 3)
            {
                flag = false;
            }
            else
            {
                int contLetras = 0;
                int contNumeros = 0;

                int tmp = (int)password.ToCharArray()[0];
                foreach (char c in password.ToCharArray())
                {
                    if (((int)c < 127 && (int)c >= 65))
                    {
                        if (tmp == (int)c)
                        {
                            contLetras++;
                        }
                        if (contLetras >= 3) { break; }
                        tmp = (int)c;
                    }
                }

                tmp = (int)password.ToCharArray()[0];
                foreach (char c in password.ToCharArray())
                {
                    if (((int)c < 57 && (int)c >= 48))
                    {
                        if (tmp == (int)c)
                        {
                            contNumeros++;
                        }
                        if (contNumeros >= 4) { break; }
                        tmp = (int)c;
                    }
                }
                if (contLetras >= 3 || contNumeros >= 4)
                {
                    flag = false;
                }
            }

            return flag;
        }
    }
}
