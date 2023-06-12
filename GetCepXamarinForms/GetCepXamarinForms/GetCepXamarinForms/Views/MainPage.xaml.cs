using GetCepXamarinForms.ViewModels;
using System;
using Xamarin.Forms;

namespace GetCepXamarinForms
{
    public partial class MainPage : ContentPage
    {
        private readonly CepViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CepViewModel();
        }

        private async void Pesquisar_ClickedAsync(object sender, EventArgs e)
        {
            await _viewModel.UpdateCepAsync();
        }

        private void CepInputed_changed(object sender, TextChangedEventArgs e)
        {
            _viewModel.ResetProperties();
        }
    }
}