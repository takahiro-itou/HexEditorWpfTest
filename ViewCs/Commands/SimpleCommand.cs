//  -*-  coding: utf-8-with-signature;  mode: c++  -*-  //
/*************************************************************************
**                                                                      **
**                  ---  WPF UserControl Library.  ---                  **
**                                                                      **
**          Copyright (C), 2026-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using System;
using System.ComponentModel;
using System.Windows.Input;


namespace  ViewCs.Commands  {

//========================================================================
//
//    SimpleCommand  class.
//

public  class  SimpleCommand : ICommand
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

    //----------------------------------------------------------------
    /**   コンストラクタ。
    **
    **/
    public SimpleCommand(
            Action<object?>     execute,
            Predicate<object?>? canExecute = null)
    {
        this.m_execute  = execute ?? throw new ArgumentNullException(
                nameof(execute));
        this.m_canExecute = canExecute;
    }


//========================================================================
//
//    Public Member Functions (Implement Interface).
//

//========================================================================
//
//    Public Properties (Implement Interface).
//

    //----------------------------------------------------------------
    /**   コマンドが実行可能か否かを返す。
    **
    **/
    public bool CanExecute(object? parameter)
        => this.m_canExecute?.Invoke(parameter) ?? true;

    //----------------------------------------------------------------
    /**
    **
    **/
    public void Execute(object? parameter)
        => this.m_execute(parameter);

    //----------------------------------------------------------------
    /**
    **
    **/
    public event EventHandler?  CanExecuteChanged;


//========================================================================
//
//    Public Member Functions (Implement Interface).
//

    //----------------------------------------------------------------
    /**   CanExecuteChanged イベントを発生させる。
    **
    **/
    public void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);


//========================================================================
//
//    Member Variables.
//

    /**   実行する内容。    **/
    private readonly  Action<object?>       m_execute;

    /**   実行可否の判定。  **/
    private readonly  Predicate<object?>?   m_canExecute;

}   //  End class  SimpleCommand

}   //  End of namespace  ViewCs.Commands
