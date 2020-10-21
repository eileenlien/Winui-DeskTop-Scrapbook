using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.ViewManagement;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScrapBook7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TextCopy : Page
    {
        public TextCopy()
        {
            this.InitializeComponent();
        }

        DataPackage dataPackage = new DataPackage();
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            if (ReadingContent.SelectedText.Length > 0)
            {
                dataPackage.SetText(ReadingContent.SelectedText);
                Clipboard.SetContent(dataPackage);
            }
        }
        private async void BtnPaste_Click(object sender, RoutedEventArgs e)
        {

            DataPackageView dataPackageView = Clipboard.GetContent();
            if (dataPackageView.Contains(StandardDataFormats.Text))
            {
                string text = await dataPackageView.GetTextAsync();
                Memo1.Text = text;
            }
            else
            {
                Memo1.Text = "Error!Text format is not available in clipboard.";
            }
        }
        private void jump_ImgCP(object sender, RoutedEventArgs e)
        {
            var parameters = new pickerParameter();
            parameters.parentPage = "TextCopy";
            parameters.singlePicPath = "";

            TextFrame.Navigate(typeof(ImgCopy), parameters);
        }
    }
}
