using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GetCepXamarinForms.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected bool SetProperty<TValue>(ref TValue backingStore, TValue value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<TValue>.Default.Equals(backingStore, value)) { return false; }
            backingStore = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}