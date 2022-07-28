using GetCepXamarinForms.Models;
using GetCepXamarinForms.ViewModels;
using System;
using Xamarin.Forms;

namespace GetCepXamarinForms
{
    public partial class MainPage : ContentPage
    {
        readonly CepViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CepViewModel();
        }

        async void Pesquisar_ClickedAsync(object sender, EventArgs e)
        {
            await _viewModel.UpdateCepAsync();
        }

        void CepInputed_changed(object sender, TextChangedEventArgs e)
        {
            _viewModel.InvalidCep = false;
            _viewModel.CepNotFound = false;
            _viewModel.CepSchema = new CepSchema();
        }
    }
}