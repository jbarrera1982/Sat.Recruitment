using Sat.Recruitment.Entities.Interface;
using System;

namespace Sat.Recruitment.Api.Class
{
    public class User : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
        private decimal percentage { get; set; }

        public override string ToString()
        {            
            return string.Join(',', Name, NormalizedEmail, Phone, Address, UserType, GiftMoney);
        }

        public decimal GiftMoney
        {
            get
            {
                percentage = 0;
                switch (UserType)
                {
                    case "Normal":
                        if (Money > 100)
                            this.percentage = 0.12M;
                        if (Money > 10 && Money < 100)
                            this.percentage = 0.8M;
                        break;
                    case "SuperUser":
                        if (Money > 100)
                            this.percentage = 0.20M;
                        break;
                    case "Premium":
                        if (Money > 100)
                            this.percentage = 2M;
                        break;
                    default:
                        break;
                }
                var gif = Money * percentage;
                return Money + gif;
            }
        }

        public string NormalizedEmail {
            get
            {

                var aux = Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                return string.Join("@", new string[] { aux[0], aux[1] });
            }

        }        
    }
}
