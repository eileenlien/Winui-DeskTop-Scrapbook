# ClipBorad(Scrapbook) - Clipboard on WinUI3 #

## Build Enviroment for WinUI3 ##
- Steps:
    1. 確定您的開發電腦已安裝 Windows 10 1803 (組建 17134) 或較新版本
    2. 安裝 Visual Studio 2019 (16.7.2 版本)， 安裝 Visual Studio 時，您必須包含下列選項:
        * .NET 桌面開發
        * 通用 Windows 平台開發
    3. 下載 Visual Studio 之後，請確保在程式中啟用 .NET 預覽：
        * 中文: [工具] > [選項] > [預覽功能] > 選取 [使用 .NET Core SDK 的預覽 (需要重新開機)]
        * 英文: Tools >Options > Preview Features > Select previews of .net Core SDK (requires resart)
    4. 想要建立適用於 C#/.NET 5 和 C++/Win32 應用程式的桌面 WinUI 專案，您也必須**同時安裝 x64** 和**x86 版本**的 .NET 5 預覽版 5： 請注意，.NET 5 預覽版 5 目前僅支援 WinUI 3 的 .NET 5 預覽：
        * 適用於 .NET 5 Preview 5 的 x64 安裝程式
        * 適用於 .NET 5 Preview 5 的 x86 安裝程式
    5. 下載及安裝 WinUI 3 預覽版 2 VSIX 套件。 此 VSIX 套件會將 WinUI 3 專案範本和 NuGet 套件 (包含 WinUI 3 程式庫) 新增至 Visual Studio 2019。

- Example for Winui in desktop and uwp:
    - Desktop: https://docs.microsoft.com/zh-tw/windows/apps/winui/winui3/get-started-winui3-for-desktop

    - UWP: https://docs.microsoft.com/zh-tw/windows/apps/winui/winui3/get-started-winui3-for-uwp

### Reference ### 
https://docs.microsoft.com/zh-tw/windows/apps/winui/winui3/

## Notice ##
- Desktop 與 uwp 語法與套件極為相似，但是兩方的功能**不能全部互通**以及**操作方法也會有變化**
- 會在下面章節針對遇到的問題進行說明。


## Text part ##
- How to start:
    - use the [Copy and paste](https://docs.microsoft.com/en-us/windows/uwp/app-to-app/copy-and-paste) and build the simple Clipboard for text.
    - 引入 Windows.ApplicationModel.DataTransfer 來操作剪貼簿的功能，此功能在UWP與Desktop上接可互通。

## Image part ##
- 範例程式碼(For Desktop)


### Problems ###

1. File Open Picker work in uwp but not work in desktop
    - [FileOpenPicker](https://docs.microsoft.com/en-us/uwp/api/Windows.Storage.Pickers.FileOpenPicker?view=winrt-19041) dose not work on desktop
    - OpenFileDialog can not find in desktop
        - [Microsoft.Win32.OpenFileDialog](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog?view=netcore-3.1) does not work
        - [System.Windows.Forms.OpenFileDialog](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netcore-3.1) does not work
    - MyOwnSolution: Use [Windows.Storage.StorageFile](https://docs.microsoft.com/en-us/uwp/api/Windows.Storage.StorageFile?view=winrt-19041) to get the files in Customalize UI.

### Navigation Page ###
