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
            TextContentSetting();
        }

        public void iniPage()
        {
            TextContentSetting();
            BtnCopyText.IsChecked = false;
            BtnCutText.IsChecked = false;
            BtnPasteText.IsChecked = false;
        }
        public void TextContentSetting()
        {
            ReadingContent.Text = "1. Innovation distinguishes between a leader and a follower.\n\n "
                + "2. We don’t get a chance to do that many things, and every one should be really excellent.\n\n"
                + "3. If you live each day as if it was your last, someday you’ll most certainly be right.\n\n";

        }

        DataPackage dataPackage = new DataPackage();
        private void BtnCut_Click(object sender, RoutedEventArgs e)
        {
            //update the original content
            dataPackage.RequestedOperation = DataPackageOperation.Move;
            int selectlen = ReadingContent.SelectedText.Length;
            if (selectlen > 0)
            {
                dataPackage.SetText(ReadingContent.SelectedText);
                Clipboard.SetContent(dataPackage);
                ReadingContent.Text = ReadingContent.Text.Remove(ReadingContent.SelectionStart, selectlen);
            }
            BtnCutText.IsChecked = false;
        }
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            if (ReadingContent.SelectedText.Length > 0)
            {
                dataPackage.SetText(ReadingContent.SelectedText);
                Clipboard.SetContent(dataPackage);
            }
            BtnCopyText.IsChecked = false;
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
            BtnPasteText.IsChecked = false;
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
