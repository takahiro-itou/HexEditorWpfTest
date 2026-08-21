
using System.Runtime.InteropServices;

namespace  ViewCs  {

public  class  WinAPI
{

public  const  int  SRCCOPY = 0xCC0020;

[DllImport("gdi32.dll")]
public  static  extern  int  BitBlt(
    System.IntPtr hDestDC,
    int X, int Y,
    int nWidth, int nHeight,
    System.IntPtr hSrcDC,
    int xSrc, int ySrc,
    int dwRop
);

[DllImport("user32.dll")]
public  static  extern  System.IntPtr  GetDC(
    System.IntPtr hWnd
);

[DllImport("user32.dll")]
public  static  extern  System.IntPtr  ReleaseDC(
    System.IntPtr hWnd,
    System.IntPtr hDC
);

}   //  End class  WinAPI

}   //  End of namespace  ViewCs
