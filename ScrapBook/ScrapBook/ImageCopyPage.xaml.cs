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
using Windows.Storage;
using System.Diagnostics;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScrapBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageCopyPage : Page
    {
        public static string operationStatus = "";
        public static string userCopyImagePath = "";
        public static StorageFile file;
        DataPackage dataPackage;
        public ImageCopyPage()
        {
            this.InitializeComponent();
            InitialPage();
        }
        void InitialPage()
        {
            dataPackage = new DataPackage();
            btnCutImg.Click += new RoutedEventHandler(btnCutImg_Click);
            btnCopyImg.Click += new RoutedEventHandler(btnCopyImg_Click);
            btnPasteImg.Click += new RoutedEventHandler(btnPasteImg_Click);
            btnMassagePage.Click += new RoutedEventHandler(btnMassagePage_Click);
        }

        public void btnCopyImg_Click(object sender, RoutedEventArgs e)
        {

            btnCopyImg.IsChecked = false;
            operationStatus = "Copy";
            jump_FilePicerPage();

        }


        public void btnCutImg_Click(object sender, RoutedEventArgs e)
        {
            btnCutImg.IsChecked = false;
            operationStatus = "Cut";
            Debug.WriteLine(operationStatus);
            jump_FilePicerPage();

        }
        public async void btnPasteImg_Click(object sender, RoutedEventArgs e)
        {
            var dataPackageView = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
            if (dataPackageView.Contains(StandardDataFormats.Bitmap))
            {
                IRandomAccessStreamReference imageReceived = null;
                imageReceived = await dataPackageView.GetBitmapAsync();
                using (var imageStream = await imageReceived.OpenReadAsync())
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(imageStream);
                    OutputImage.Source = bitmapImage;
                    OutputImage.Visibility = Visibility.Visible;

                }
                if (file != null && "Cut".Equals(operationStatus))
                {
                    await file.DeleteAsync();
                }

            }
            else
            {
                Debug.WriteLine("DataPakage error!! ");
            }

        }

        private void jump_FilePicerPage()
        {
            imgFrame.Navigate(typeof(ImageSelectorPage));
        }

        private void btnMassagePage_Click(object sender, RoutedEventArgs e)
        {
            imgFrame.Navigate(typeof(TextCopyPage));

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            var parameters = (pickerParameter)e.Parameter;
            string target = parameters.singlePicPath;
            string parentPage = parameters.parentPage;
            if ("ImgPicker".Equals(parentPage))
            {
                userCopyImagePath = target;
                if (userCopyImagePath != "")
                {
                    dataPackage = new DataPackage();
                    string path = userCopyImagePath;
                    file = await StorageFile.GetFileFromPathAsync(path);
                    dataPackage.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
                    Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
                }
            }
            else
            {
                userCopyImagePath = "";
            }

        }

    }
}
