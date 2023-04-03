using TestAssesmentForDCT.Views.Windows.Abstractions;

namespace TestAssesmentForDCT.Services
{
    public class DialogService : IDialogService
    {
        public void ShowDialog(string dialogName, BaseViewModel? viewModel = null)
        {
            var dialogWindow = new DialogWindow();

            Type? fullTypeOfDialogWindow = Type.GetType($"TestAssesmentForDCT.Views.DialogWindows.{dialogName}");

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
