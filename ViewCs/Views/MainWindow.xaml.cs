
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

using ViewCs;


namespace  ViewCs.Views  {

public  partial class  MainWindow : Window
{

    //----------------------------------------------------------------
    /**   デフォルトコンストラクタ。
    **
    **/
    public  MainWindow()
    {
        InitializeComponent();

        this.m_taskModel = new Models.SampleModel();
        this.m_viewModel = new ViewModels.SampleViewModel(this.m_taskModel);

        this.DataContext = this.m_viewModel;
    }


    //----------------------------------------------------------------
    /**   指定したコマンドを実行する。
    **
    **/
    private  void
    runCommand()
    {
        IntPtr  hDisplayDC  = WinAPI.GetDC(IntPtr.Zero);

        Bitmap    imgBuffer = new Bitmap(200, 100);
        Graphics  grpBuffer = Graphics.FromImage(imgBuffer);

        Color       colorBG = Color.FromArgb(0xFF, 0xFE, 0xF0, 0xBA);
        SolidBrush  brushBG = new SolidBrush(colorBG);
        grpBuffer.FillRectangle(brushBG, 0, 0, 200, 100);

        IntPtr  hDC = grpBuffer.GetHdc();
        WinAPI.BitBlt(hDC, 8, 8, 184, 84, hDisplayDC,
            (int)(SystemParameters.PrimaryScreenWidth - 184),
            (int)(SystemParameters.PrimaryScreenHeight - 84),
            WinAPI.SRCCOPY);
        grpBuffer.ReleaseHdc(hDC);

        grpBuffer.DrawRectangle(Pens.Yellow, 50, 30, 100, 60);
        grpBuffer.DrawPie(Pens.Red, 60, 10, 80, 80, 30, 300);
        grpBuffer.Dispose();

        Bitmap    imgCanvas = new Bitmap(300, 300);
        Graphics  grpCanvas = Graphics.FromImage(imgCanvas);

        colorBG = Color.FromArgb(0x80, 0x00, 0x00, 0xff);
        brushBG = new SolidBrush(colorBG);
        grpCanvas.FillRectangle(brushBG, 0, 0, 300, 300);
        grpCanvas.Dispose();

        hDC = grpCanvas.GetHdc();
        WinAPI.BitBlt(hDC, 8, 8, 284, 284, hDisplayDC, 0, 0, WinAPI.SRCCOPY);
        grpCanvas.ReleaseHdc(hDC);

        WinAPI.ReleaseDC(IntPtr.Zero, hDisplayDC);

        grpCanvas.DrawImage(imgBuffer, 50, 100, 200, 100);
        grpCanvas.Dispose();

        System.IntPtr hBitmap = imgCanvas.GetHbitmap();
        System.Windows.Media.Imaging.BitmapSource   bmpSrc =
            System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        picView.Source = bmpSrc;
    }

    //----------------------------------------------------------------
    /**
    **
    **/
    private  void  mnuFileExit_Click(object sender, EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }

    //----------------------------------------------------------------
    /**
    **
    **/
    private  void  mnuRunCommand_Click(object sender, EventArgs e)
    {
        runCommand();
    }

    private Models.SampleModel          m_taskModel;
    private ViewModels.SampleViewModel  m_viewModel;

}   //  End class  MainWindow

}   //  End of namespace  ViewCs.Views
