using ReactiveUI;
using System;
using System.Runtime.CompilerServices;

namespace PearlCalculatorCP.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected TRet RaiseAndSetProperty<TRet>(ref TRet backingField, TRet newValue, [CallerMemberName]string? propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof (propertyName));
            this.RaisePropertyChanging(propertyName);
            backingField = newValue;
            this.RaisePropertyChanged(propertyName);
            return newValue;
        }

        protected TRet RaiseAndSetOrIfChanged<TRet>(ref TRet backingField, TRet newValue, bool isIfChanged,
            [CallerMemberName] string? propertyName = null)
        {
            return isIfChanged
                ? this.RaiseAndSetIfChanged(ref backingField, newValue, propertyName)
                : RaiseAndSetProperty(ref backingField, newValue, propertyName);

        }
    }
}
