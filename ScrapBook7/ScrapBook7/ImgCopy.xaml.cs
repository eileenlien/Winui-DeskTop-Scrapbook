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
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScrapBook7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class ImgCopy : Page
    {
        public string userCopyImage = "";
        public ImgCopy()
        {
            this.InitializeComponent();
            Init();
        }
        void Init()
        {
            Debug.WriteLine("Here is ImgCopy Page !!");
            Copybtn.Click += new RoutedEventHandler(CopyImg_btn);
            Pastebtn.Click += new RoutedEventHandler(PasteImg_btn);
            MassagePagebtn.Click += new RoutedEventHandler(MassagePage_btn);
        }

        public void CopyImg_btn(object sender, RoutedEventArgs e)
        {
            jump_FilePicerPage();

        }

        DataPackage dataPackage = new DataPackage();
        public async void PasteImg_btn(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            Clipboard.Clear();
            StorageFile file = null;
            dataPackage = new DataPackage();

            
            if (userCopyImage != "")
            {
                Message_area.Text = userCopyImage;
                string path = userCopyImage;
                file = await StorageFile.GetFileFromPathAsync(path);


                dataPackage.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
                Clipboard.SetContent(dataPackage);
                var dataPackageView = Clipboard.GetContent();
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

                }
                else
                {
                    Debug.WriteLine("DataPakage error!! ");
                }
            }
            else
            {
                Message_area.Text = "Please use copy btn to copy.";
            }
        }

        private void jump_FilePicerPage()
        {
            imgFrame.Navigate(typeof(ImageSelector));
        }

        private void MassagePage_btn(object sender, RoutedEventArgs e)
        {
            imgFrame.Navigate(typeof(TextCopy));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            base.OnNavigatedTo(e);
            var parameters = (pickerParameter)e.Parameter;
            string target = parameters.singlePicPath;
            string parentPage = parameters.parentPage;
            if ("ImgPicker".Equals(parentPage))
            {
                userCopyImage = target;
            }
            else
            {
                userCopyImage = "";
            }

        }
    }
}
