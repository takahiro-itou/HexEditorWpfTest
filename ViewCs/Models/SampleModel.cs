
using System;


namespace  ViewCs.Models  {

public  class  SampleModel
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

    //----------------------------------------------------------------
    /**   コンストラクタ。
    **
    **/
    public SampleModel()
    {
        this.m_resultText = "";
    }


//========================================================================
//
//    Public Properties.
//

    //----------------------------------------------------------------
    /**   モデルのタスクを実行する。
    **
    **/
    public  virtual  int
    executeCommand(IProgress<int>  progress)
    {
        using (var process = new System.Diagnostics.Process()) {
            process.StartInfo.FileName = "ipconfig.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = false;
            process.Start();

            System.IO.StreamReader reader = process.StandardOutput;
            string  output  = reader.ReadToEnd();

            this.ResultText = output;
            process.WaitForExit();
            process.Close();
        }

        progress.Report(100);
        return ( 0 );
    }


//========================================================================
//
//    Properties.
//

    public  string
    ResultText  {
        get { return  this.m_resultText; }
        set { this.m_resultText = value; }
    }


//========================================================================
//
//    Protected Member Functions.
//

//========================================================================
//
//    Member Variables.
//

    private  string     m_resultText;

}   //  End class  SampleModel

}   //  End of namespace  ViewCs.Models
