using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Register
{
    public class UserInformation
    {
        private string _Name;
        private string _UserName;
        private string _PassWord;
        private string _Email;

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
            }
        }

        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
            }
        }

        public string PassWord
        {
            set
            {
                _PassWord = value;
            }
        }

        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
            }
        }

        public UserInformation()
        {

        }

        public UserInformation(string Information)
        {
            int index = 0;
            _UserName = getString(ref index, Information);
            _PassWord = getString(ref index, Information);
            _Email = getString(ref index, Information);

            string temp = "";
            for (int i = index; i < Information.Length; i++)
                temp += Information[i];
            _Name = temp;
        }
        private string getString(ref int index, string str)
        {
            int temp = index;
            while (str[index] != ' ') index++;
            string st = "";
            for (int i = temp ; i < index; i++)
                st += str[i];
            index++;
            return st;
        }
        public UserInformation(string Name, string UserName, string PassWord, string Email)
        {
            _Name = Name;
            _UserName = UserName;
            _PassWord = PassWord;
            _Email = Email;
        }
    }
}