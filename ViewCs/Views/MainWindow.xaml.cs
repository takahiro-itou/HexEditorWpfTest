
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

using WpfHexEditor;


namespace  WpfHexEditor.Views  {

public  partial class  MainWindow : Window
{

//----------------------------------------------------------------
/**   デフォルトコンストラクタ。
**
**/
public  MainWindow()
{
    InitializeComponent();
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
}


private  void
VerticalScroll_Scroll(object sender, ScrollEventArgs e)
{
}


protected  override  void
OnRenderSizeChanged(SizeChangedInfo sizeInfo)
{
    base.OnRenderSizeChanged(sizeInfo);
}


}   //  End class  MainWindow

}   //  End of namespace  WpfHexEditor.Views
