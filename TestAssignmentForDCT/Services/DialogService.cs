using TestAssignmentForDCT.Views.Windows.Abstractions;

namespace TestAssignmentForDCT.Services
{
    public class DialogService : IDialogService
    {
        public void ShowDialog(string dialogName, BaseViewModel? viewModel = null)
        {
            var dialogWindow = new DialogWindow();

            Type? fullTypeOfDialogWindow = Type.GetType($"TestAssignmentForDCT.Views.DialogWindows.{dialogName}");

            if (fullTypeOfDialogWindow != null) 
            {
                dialogWindow.Title = dialogName;
                var content = Activator.CreateInstance(fullTypeOfDialogWindow);

                if (viewModel != null)
                {
                    (content as FrameworkElement)!.DataContext = viewModel;
                    dialogWindow.Content = content;
                }
                
                dialogWindow.ShowDialog();
            }
        }
    }
}
