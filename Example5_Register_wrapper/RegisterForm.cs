using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sipek.Sip;
using Sipek.Common;

namespace Example5_Register_wrapper
{
  public partial class RegisterForm : Form
  {
    #region Inner class (Account data holder)

    class MyConfig : IConfiguratorInterface
    {
      #region IConfiguratorInterface Members

      public bool AAFlag
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public List<IAccount> Accounts
      {
        get { throw new Exception("The method or operation is not implemented."); }
      }

      public bool CFBFlag
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public string CFBNumber
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public bool CFNRFlag
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public string CFNRNumber
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public bool CFUFlag
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public string CFUNumber
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public List<string> CodecList
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public bool DNDFlag
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public int DefaultAccountIndex
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public bool IsNull
      {
        get { throw new Exception("The method or operation is not implemented."); }
      }

      public int SIPPort
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }

      public void Save()
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public bool PublishEnabled
      {
        get
        {
          throw new Exception("The method or operation is not implemented.");
        }
        set
        {
          throw new Exception("The method or operation is not implemented.");
        }
      }
      #endregion

    }

    class MyAccount : IAccount
    {
      RegisterForm _form;

      public MyAccount(RegisterForm form)
      {
        _form = form;
      }

      #region IAccount Members

      public string AccountName
      {
        get { return _form.textBoxId.Text; }
        set { }
      }

      public string DisplayName
      {
        get { return ""; }
        set { }
      }

      public string DomainName
      {
        get { return _form.textBoxDomain.Text; }
        set { }
      }

      public string HostName
      {
        get { return _form.textBoxRegistrar.Text; }
        set { }
      }

      public string Id
      { 
        get { return _form.textBoxId.Text; }
        set { }
      }

      public string Password
      {
        get { return _form.textBoxPassword.Text; }
        set { }
      }

      public string ProxyAddress
      {
        get { return ""; }
        set { }
      }

      public int RegState
      {
        get { return 0; }
        set { }
      }

      public string UserName
      {
        get { return _form.textBoxUsername.Text; }
        set { }
      }
      public int Index
      {
        get
        {
          return 0;
        }
        set
        {
          ;
        }
      }

      public ETransportMode TransportMode
      {
        get
        {
          return ETransportMode.TM_UDP;
        }
        set
        {
          ;
        }
      }

      #endregion
    }

    #endregion

    // store registrar proxy instance
    pjsipRegistrar registrar = pjsipRegistrar.Instance;

    public RegisterForm()
    {
      InitializeComponent();

      // Initialize pjsip stack
      pjsipStackProxy.Instance.initialize();

      //  register event for registration status change
      registrar.AccountStateChanged += new DAccountStateChanged(proxy_AccountStateChanged);
    }

    /// <summary>
    /// Invoke status refresh. First check for cross threading!!!
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="accState"></param>
    void proxy_AccountStateChanged(int accountId, int accState)
    {
      if (this.InvokeRequired)
      {
        Invoke(new DAccountStateChanged(proxy_AccountStateChanged), new object[] {accountId, accState});
      }
      else 
      {
        sync_AccountStateChanged(accountId, accState);
      }
    }
    /// <summary>
    /// Update registration status
    /// </summary>
    /// <param name="accId"></param>
    /// <param name="accState"></param>
    private void sync_AccountStateChanged(int accId, int accState)
    {
          this.textBoxStatus.Text = accState.ToString();
    }

    /// <summary>
    /// On click handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonRegister_Click(object sender, EventArgs e)
    {
      // collect account data from windows form 
      MyAccount acc = new MyAccount(this);

      // clean previous accounts (if any)
      registrar.Config.Accounts.Clear();

      // add account data holder to proxy!
      registrar.Config.Accounts.Add(acc);

      this.textBoxStatus.Text = "Waiting status...";
      this.Refresh();
      // send register request
      registrar.registerAccounts();
    }

  }


 

}