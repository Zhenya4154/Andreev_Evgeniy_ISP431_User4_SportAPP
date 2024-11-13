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

namespace SportApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        
        public string FlagAddOrEdit = "default";
        public Data.Users CurrentUser = new Data.Users();
        public AddEditPage(Data.Users _user)
        {

            InitializeComponent();
            if (_user != null)
            {
                CurrentUser = _user;
                FlagAddOrEdit = "edit";
            }
            else
            {
                FlagAddOrEdit = "add";
            }
            DataContext = CurrentUser;
            Init();
        }

    
        
        private void Init()
        {
            try
            {
                RoleComboBox.ItemsSource = Data.User4Entities.GetContext().RoleUser.ToList();
                GenderComboBox.ItemsSource = Data.User4Entities.GetContext().GenderUser.ToList();

                if(FlagAddOrEdit == "add")
                {
                    UserNameTextBox.Text = string.Empty;
                    RoleComboBox.SelectedItem = null;
                    BitrhdayDatePicker.Text = string.Empty;
                    GenderComboBox.SelectedItem = null;
                    NumberPhoneTextBox.Text = string.Empty;
                    EmailTextBox.Text = string.Empty;
                    LoginTextBox.Text = string.Empty;
                    PasswordTextBox.Password = string.Empty;
                }
                else if (FlagAddOrEdit == "edit")
                {
                    UserNameTextBox.Text = CurrentUser.userName;
                    RoleComboBox.SelectedItem = Data.User4Entities.GetContext().RoleUser
                        .Where(d => d.id == CurrentUser.idGender).FirstOrDefault();
                    NumberPhoneTextBox.Text = CurrentUser.numberPhone;
                    GenderComboBox.SelectedItem = Data.User4Entities.GetContext().GenderUser
                        .Where(d => d.id == CurrentUser.idGender).FirstOrDefault();
                    EmailTextBox.Text = CurrentUser.email;
                    LoginTextBox.Text = CurrentUser.login;
                    PasswordTextBox.Password = CurrentUser.password;
                    ConfirmPasswordTextBox.Password = PasswordTextBox.Password;
                }

            }
            catch 
            {
                
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.UsersListPage());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(UserNameTextBox.Text))
                {
                    errors.AppendLine("Заполните ФИО");
                }
                if (string.IsNullOrEmpty(RoleComboBox.Text))
                {
                    errors.AppendLine("Выберите роль");
                }
                if (string.IsNullOrEmpty(NumberPhoneTextBox.Text))
                {
                    errors.AppendLine("Заполните номер телефона");
                }
                if (string.IsNullOrEmpty(GenderComboBox.Text))
                {
                    errors.AppendLine("Выберите пол");
                }
                if (string.IsNullOrEmpty(EmailTextBox.Text))
                {
                    errors.AppendLine("Заполните почту");
                }
                if (string.IsNullOrEmpty(BitrhdayDatePicker.Text))
                {
                    errors.AppendLine("Выберите дату рождения");
                }
                if (string.IsNullOrEmpty(LoginTextBox.Text))
                {
                    errors.AppendLine("Заполните логин");
                }
                if (string.IsNullOrEmpty(PasswordTextBox.Password))
                {
                    errors.AppendLine("Заполните пароль");
                }
                if (string.IsNullOrEmpty(ConfirmPasswordTextBox.Password))
                {
                    errors.AppendLine("Подтвердите пароль");
                }
                if(errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                CurrentUser.userName = UserNameTextBox.Text;
                CurrentUser.password = PasswordTextBox.Password;
                CurrentUser.login = LoginTextBox.Text;
                CurrentUser.email = EmailTextBox.Text;
                CurrentUser.numberPhone = NumberPhoneTextBox.Text;
                
                MessageBox.Show("Успешно добавлено!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                Classes.Manager.MainFrame.Navigate(new Pages.UsersListPage());
            }
            catch
            {

            }
        }
    }
}
