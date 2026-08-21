
using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace  WpfHexEditor.Views  {

public  class  HexRenderElement : FrameworkElement
{

private   byte[]    m_data  = Array.Empty<byte>();
private   readonly  DrawingVisual       m_drawingVisual;
private   readonly  VisualCollection    m_children;

public    double    RowHeight { get; } = 18;
private   const int BytesPerRow = 16;

private   readonly  Typeface    m_typeface = new Typeface("Consolas");
private   readonly  double      m_fontSize = 13;

private   int       m_currentRowOffset = 0;
public  int
CurrentRowOffset  {
    get { return  this.m_currentRowOffset; }
    set {
        if (this.m_currentRowOffset != value) {
            this.m_currentRowOffset = value;
            Render();
        }
    }
}

public  HexRenderElement()
{
    this.m_children = new VisualCollection(this);
    this.m_drawingVisual = new DrawingVisual();
    this.m_children.Add(this.m_drawingVisual);
}

public  void
setData(
    byte[]  data)
{
    this.m_data = data;
    Render();
}


private  void
Render()
{
    if ( this.m_data == null || this.m_data.Length == 0) { return; }
    using (DrawingContext dc = this.m_drawingVisual.RenderOpen())
    {
        //  背景の塗りつぶし
        dc.DrawRectangle(Brushes.White, null,
            new Rect(0, 0, ActualWidth, ActualHeight));

        int visibleRows = (int)Math.Ceiling(ActualHeight / RowHeight);
        int totalRows = (int)Math.Ceiling((double)m_data.Length / BytesPerRow);
        double  addressX = 10;
        double  hexX = 100;
        double  asciiX = 420;

        for ( int i = 0; i < visibleRows; ++ i ) {
            int rowIndex = this.m_currentRowOffset + i;
            if ( rowIndex >= totalRows ) { break; }

            double y = i * RowHeight;

            int fileOffset = rowIndex * BytesPerRow;
            drawText(dc, fileOffset.ToString("X8"),
                addressX, y, Brushes.Gray);

            StringBuilder hexBuilder = new StringBuilder();
            StringBuilder asciiBuilder = new StringBuilder();
            for (int j = 0; j < BytesPerRow; ++ j ) {
                int byteIndex = fileOffset + j;
                if ( byteIndex < this.m_data.Length ) {
                    byte b = this.m_data[byteIndex];
                    hexBuilder.Append(b.ToString("X2") + " ");
                    asciiBuilder.Append(
                        b >= 32 && b <= 126 ? (char)b : '.');
                } else {
                    hexBuilder.Append("   ");
                }
            }
            drawText(dc, hexBuilder.ToString(), hexX, y, Brushes.Black);
            drawText(dc, asciiBuilder.ToString(), asciiX, y, Brushes.Blue);

        }
    }
}


private  void  drawText(DrawingContext dc, string text, double x, double y,Brush brush)
{
    var pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
    var formattedText = new FormattedText(
        text,
        CultureInfo.InvariantCulture,
        FlowDirection.LeftToRight,
        this.m_typeface,
        this.m_fontSize,
        brush,
        pixelsPerDip);
    dc.DrawText(formattedText, new Point(x, y));
}

protected  override  int
VisualChildrenCount => this.m_children.Count;

protected  override  Visual
GetVisualChild(int index) => this.m_children[index];

protected  override  void
OnRenderSizeChanged(SizeChangedInfo sizeInfo)
{
    base.OnRenderSizeChanged(sizeInfo);
    Render();
}

}   //  End class  HexRenderElement

}   //  End of namespace  WpfHexEditor.Views
