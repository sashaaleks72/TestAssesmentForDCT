﻿
namespace TestAssignmentForDCT.Services.Abstractions
{
    interface IDialogService
    {
        void ShowDialog(string dialogName, BaseViewModel? viewModel = null);
    }
}
