using System.Collections.Generic;
using System.Threading.Tasks;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Models;

namespace HostelPortable
{
    public static class Extensions
    {
        public static Task<bool> ShowQuestion(this IMessagePresenter messageBox, string messageBoxText, string caption)
        {
            return messageBox
                .ShowAsync(messageBoxText, caption, MessageButton.YesNo)
                .ContinueWith(task => task.Result == MessageResult.Yes);
        }

        public static Task<bool> ShowDeleteQuestion(this IMessagePresenter messagePresenter)
        {
            return messagePresenter.ShowQuestion(UiResources.DeleteMessage, UiResources.DeleteCaption);
        }

        public static T? ToNulllable<T>(this T value) where T : struct
        {
            return EqualityComparer<T>.Default.Equals(value, default(T)) ? (T?)null : value;
        }
    }
}
