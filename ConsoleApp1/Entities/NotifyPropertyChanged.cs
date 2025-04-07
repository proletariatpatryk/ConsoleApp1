using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsoleApp1.Entities
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanging, INotifyPropertyChanged
    {
        protected void SetWithNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                NotifyChanging(propertyName);
                field = value;
                NotifyChanged(propertyName);
            }
        }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected virtual void NotifyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }
}
