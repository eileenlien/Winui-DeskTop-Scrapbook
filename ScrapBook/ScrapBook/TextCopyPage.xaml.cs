using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScrapBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TextCopyPage : Page
    {
        public TextCopyPage()
        {
            this.InitializeComponent();
            initializePage();
        }

        public void initializePage()
        {
            ImgPagebtn.Click += new RoutedEventHandler(NavigateImgPage_Click);
            TextContentSetting();
            CopyTextBtn.IsChecked = false;
            CutTextBtn.IsChecked = false;
            PasteTextBtn.IsChecked = false;
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
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
                ReadingContent.Text = ReadingContent.Text.Remove(ReadingContent.SelectionStart, selectlen);
            }
            CutTextBtn.IsChecked = false;
        }
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            if (ReadingContent.SelectedText.Length > 0)
            {
                dataPackage.SetText(ReadingContent.SelectedText);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            }
            CopyTextBtn.IsChecked = false;
        }


        private async void BtnPaste_Click(object sender, RoutedEventArgs e)
        {

            DataPackageView dataPackageView = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
            if (dataPackageView.Contains(StandardDataFormats.Text))
            {
                string text = await dataPackageView.GetTextAsync();
                Memo1.Text = text;
            }
            else
            {
                Memo1.Text = "Error!Text format is not available in clipboard.";
            }
            PasteTextBtn.IsChecked = false;
        }

        private void NavigateImgPage_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("NavigateImgPage");
            var parameters = new pickerParameter();
            parameters.parentPage = "TextCopyPage";
            parameters.singlePicPath = "";
            TextCopyPageFrame.Navigate(typeof(ImageCopyPage), parameters);
            
        }

    }
}
