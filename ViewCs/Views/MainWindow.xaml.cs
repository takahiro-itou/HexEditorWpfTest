
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

using WpfHexEditor;


namespace  WpfHexEditor.Views  {

public  partial class  MainWindow : Window
{

private   byte[]    m_dummyData;
private   const int BytesPerRow = 16;

//----------------------------------------------------------------
/**   デフォルトコンストラクタ。
**
**/
public  MainWindow()
{
    InitializeComponent();

    m_dummyData = new byte[1048575];
    new Random().NextBytes(this.m_dummyData);

    HexEditor.setData(this.m_dummyData);
    this.Loaded += MainWindow_Loaded;
}

private  void
MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    UpdateScrollRange();
}

private  void
UpdateScrollRange()
{
    int  totalRows = (int)Math.Ceiling((double)(m_dummyData.Length / BytesPerRow);
    int  visibleRows = (int)(HexEditor.AcutualHeight / HexEditor.RowHeight);

    VerticalScroll.Minimum = 0;
    VerticalScroll.Maximum = Math.Max(0, totalRows - visibleRows);
    VerticalScroll.ViewportSize = visibleRows;
}


private  void
VerticalScroll_Scroll(object sender, ScrollEventArgs e)
{
    HexEditor.CurrentRowOffset = (int)e.NewValue;
}


protected  override  void
OnRenderSizeChanged(SizeChangedInfo sizeInfo)
{
    base.OnRenderSizeChanged(sizeInfo);
    if ( this.m_dummyData != null ) {
        UpdateScrollRange();
    }
}


}   //  End class  MainWindow

}   //  End of namespace  WpfHexEditor.Views
