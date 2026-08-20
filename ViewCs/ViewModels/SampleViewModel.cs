
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using ViewCs.Commands;
using ViewCs.Models;


namespace  ViewCs.ViewModels  {

public  class  SampleViewModel : INotifyPropertyChanged
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

    //----------------------------------------------------------------
    /**   コンストラクタ。
    **
    **/
    public SampleViewModel(
            SampleModel model)
    {
        this.m_progress = new System.Progress<int>(updateProgress);
        this.m_trgModel = model;

        this.m_runModelTaskCommand  = new SimpleCommand(
                _ => runModelTaskAsync(),
                _ => canRunTask()
        );
        this.m_clearTextCommand     = new SimpleCommand(
                _ => clearText(),
                _ => ! this.IsRunning
        );

        this.m_returnCode   = 0;
        this.m_isRunning    = false;
    }


//========================================================================
//
//    Public Properties.
//

    //----------------------------------------------------------------
    /**
    **
    **/
    public  event PropertyChangedEventHandler?  PropertyChanged;

    public  bool
    IsRunning  {
        get { return  this.m_isRunning; }
        private set {
            this.m_isRunning = value;
            raisePropertyChanged();
            raiseCanExecuteChanged();
        }
    }

    public  string
    ResultText  {
        get { return  this.m_trgModel.ResultText; }
        set {
            this.m_trgModel.ResultText = value;
            raisePropertyChanged();
        }
    }

    public  int
    ReturnCode  {
        get {
            return  this.m_returnCode;
        }
        private set {
            this.m_returnCode = value;
            raisePropertyChanged();
        }
    }

    public  virtual  ICommand
    ClearTextCommand {
        get { return  this.m_clearTextCommand; }
    }

    //----------------------------------------------------------------
    /**   タスクを実行するコマンドを取得するプロパティ。
    **
    **/
    public  virtual  ICommand
    RunModelTaskCommand {
        get { return  this.m_runModelTaskCommand; }
    }


//========================================================================
//
//    Public Member Functions.
//

    //----------------------------------------------------------------
    /**
    **
    **/
    public  virtual  bool
    canRunTask()
    {
        return ( ! this.IsRunning );
    }

    //----------------------------------------------------------------
    /**
    **
    **/
    public  virtual  void
    clearText()
    {
        this.ResultText = "";
        this.ReturnCode = 0;
    }

    //----------------------------------------------------------------
    /**   モデルのタスクを非同期で実行する。
    **
    **/
    public  async  void
    runModelTaskAsync()
    {
        this.IsRunning  = true;

        Task<int>  task = Task.Run<int>(
            () => this.m_trgModel.executeCommand(this.m_progress));
        int  result = await task;

        this.ReturnCode = result;
        this.IsRunning  = false;
    }


//========================================================================
//
//    Protected Member Functions.
//

    //----------------------------------------------------------------
    /**
    **
    **/
    protected  virtual  void
    raiseCanExecuteChanged()
    {
        this.m_runModelTaskCommand.RaiseCanExecuteChanged();
        this.m_clearTextCommand.RaiseCanExecuteChanged();
    }

    //----------------------------------------------------------------
    /**
    **
    **/
    protected  virtual  void
    raisePropertyChanged(
            [CallerMemberName]  System.String?  propertyName = null)
    {
        PropertyChanged?.Invoke(
                this, new PropertyChangedEventArgs(propertyName));
    }


    //----------------------------------------------------------------
    /**
    **
    **/
    protected  virtual  void
    updateProgress(int progressValue)
    {
        raisePropertyChanged(nameof(ResultText));
    }


//========================================================================
//
//    Member Variables.
//

    private  readonly   System.IProgress<int>   m_progress;
    private  readonly   SampleModel             m_trgModel;

    private  readonly   SimpleCommand           m_runModelTaskCommand;
    private  readonly   SimpleCommand           m_clearTextCommand;

    private  int    m_returnCode;
    private  bool   m_isRunning;

}   //  End class  SampleViewModel

}   //  End of namespace  ViewCs.ViewModels
