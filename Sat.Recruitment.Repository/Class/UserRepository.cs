using Sat.Recruitment.Api.Class;
using Sat.Recruitment.Entities.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Repository.Class
{
    public class UserRepository : IUserRepository
    {
        private readonly string filepath;

        public UserRepository()
        {
            this.filepath = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        }

        public bool CreateUser(IUser user)
        {
            ValidateFilePath();            
            using (StreamWriter sw = File.AppendText(filepath))
                {            
                sw.WriteLine(user.ToString());
                }
            return true;
        }

        public List<IUser> GetUsers()
        {
            var users = new List<IUser>();

            try
            {
                //var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
                using (var sr = new StreamReader(filepath))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        var row = line.Split(',');
                        var user = new User
                        {
                            Name = ValidateField(row[0]),
                            Email = ValidateField(row[1]),
                            Phone = ValidateField(row[2]),
                            Address = ValidateField(row[3]),
                            UserType = ValidateField(row[4]),
                            Money = decimal.Parse(ValidateField(row[5])),
                        };
                        users.Add(user);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("File could not be read");
                Console.WriteLine(e.Message);
            }
            return users;
        }

        private void ValidateFilePath()
        {
            FileInfo file = new FileInfo(filepath);
            if (!System.IO.Directory.Exists(file.DirectoryName))
                System.IO.Directory.CreateDirectory(file.DirectoryName);

        }

        private string ValidateField(string value)
        {
            return value != null ? value : string.Empty;
        }
    }
}
