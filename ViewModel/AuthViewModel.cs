using CookbookClient.DTO;
using CookbookClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookbookClient.ViewModel
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        public AuthViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
        }

        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private async Task LoginAsync()
        {
            try
            {
                await _authService.LoginAsync(new LoginRequestDto
                {
                    UsernameOrEmail = UsernameOrEmail,
                    Password = Password
                });

                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async Task RegisterAsync()
        {
            try
            {
                await _authService.RegisterAsync(new RegisterRequestDto
                {
                    Username = Username,
                    Email = Email,
                    Password = Password
                });

                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
