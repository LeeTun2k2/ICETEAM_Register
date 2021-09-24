using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Register
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] UserInfo;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            readFileUserData();
            /*
                Tên không được để trống;
                Tên tài khoản: 8 <= userName <= 24 và không có kí tự đặc biệt;
                Mật khẩu: 8 <= passWord <= 24, phải có Upper, Lower, Number và không kí tự đặc biệt;
                Nhập lại pass: phải trùng pass.
                Email: Phải có @
             */
            if (txbUserDisplay.Text == "") MessageBox.Show("Tên của ban không được để trống.");
            else if (CheckUserName() == false) MessageBox.Show("Tên tài khoản tối thiểu 8 kí tự và tối đa 24 kí tự và không dùng kí tự đặc biệt.");
            else if (CheckHaveAlreadyAccount() == false) MessageBox.Show("Tên tài khoản này đã tồn tại. Vui lòng chọn tên tài khoản khác.");
            else if (CheckPassWord() == false) MessageBox.Show("Mật khẩu có độ dài từ 8 đến 24 kí tự, không có kí tự đặc biệt, phải chứa chữ thường, số và chữ in hoa.");
            else if (pwbPassWord.Password != pwbReWritePassWord.Password) MessageBox.Show("Nhập lại mật khẩu không chính xác.");
            else if (checkEmail() == false) MessageBox.Show("Email không hợp lệ.");
            else
            {
                outputFileTxt();
                MessageBox.Show("Đăng ký thành công.");
            }    
        }
        private bool CheckHaveAlreadyAccount()
        {
            int NumberOfAccount = UserInfo.Length;
            for (int i = 0; i < NumberOfAccount; i++)
                if (txbUserName.Text == new UserInformation(UserInfo[i]).UserName) return false;
            return true;
        }
        private bool CheckUserName()
        {
            string userName = txbUserName.Text;
            /*
             *  8 <= userName <= 24.
            */            
            if (userName.Length < 8 || userName.Length > 24) return false;

            /*
                Check no special case 
            */
            foreach (char i in userName)
                if (!(i >= '0' && i <= '9') && !(i >= 'a' && i <= 'z') && !(i >= 'A' && i <= 'Z')) return false;
            return true;
        }
        private bool CheckPassWord()
        {
            /*
                8 <= password.Length <= 24;    
            */
            string passWord = pwbPassWord.Password;
            if (passWord.Length < 8 || passWord.Length > 24) return false;

            /*
                Check no special case;
            */
            foreach (char i in passWord)
                if (!(i>= '0' && i <= '9') && !(i>='a' && i<='z') && !(i >= 'A' && i <= 'Z')) return false;

            /*
                1. Check lower;
                2. Check upper;
                3. Check number;
                if (1 or 2 or 3 == false) return false; 
            */
            bool haveLower = false;
            foreach (char i in passWord)
                if (i >= 'a' && i <= 'z') { haveLower = true; break;}
            bool haveUpper = false;
            foreach (char i in passWord)
                if (i >= 'A' && i <= 'Z') { haveUpper = true; break; }
            bool haveNumber = false;
            foreach (char i in passWord)
                if (i >= '0' && i <= '9') { haveNumber = true; break; }
            if (!(haveLower && haveUpper && haveNumber)) return false;

            return true;
        }

        private bool checkEmail()
        {
            // Phải có @ để email hợp lệ
            string email = txbEmail.Text;
            foreach (char i in email)
                if (i == '@') return true;
            return false;
        }

        private void outputFileTxt()
        {
            string filePath = @"userData.txt";

            // str: [Tên tài khoản] [Mật khẩu] [Email] [Tên người dùng]  
            string str = txbUserName.Text + " " + pwbPassWord.Password + " " + txbEmail.Text + " " + txbUserDisplay.Text;
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(str);
            }
        }

        private void readFileUserData()
        {
            string filePath = @"userData.txt";
            UserInfo = System.IO.File.ReadAllLines(filePath);
        }
    }
}
