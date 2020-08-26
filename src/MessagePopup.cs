// StatusPopup.cs: A popup window that displays statuses [150304]
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ailanthus
{
  public partial class MessagePopup : Form
  {
    /// <summary>Default contructor</summary>
    public MessagePopup()
    {
      InitializeComponent();
    }

    /// <summary>Close the popup</summary>
    public void ClosePopup()
    {
      this.Dispose();
    }
    /// <summary>Hide the popup</summary>
    public void HidePopup()
    {
      this.Hide();
    }

    /// <summary>Set the content of the status popup</summary>
    /// <remarks>
    /// [1] If a custom title string was not passed, use a generic "Please wait..." message. Otherwise set the title to
    /// the custom message.
    /// [2] Set the primary and secondary messages.
    /// [3] Force the form to refresh.
    /// </remarks>
    /// <param name="title">Title of the popup.</param>
    /// <param name="msgPrimary">Primary popup message.</param>
    /// <param name="msgSecondary">Secondary popup message.</param>
    public void SetContent(string msgTitle, string msgPrimary, string msgSecondary)
    {
      //[1]
      if (String.IsNullOrEmpty(msgTitle))
      {
        lblMsgTitle.Text = "Please Wait...";
      }
      else
      {
        lblMsgTitle.Text = msgTitle;
      }
      //[2]
      lblMsgPrimary.Text = msgPrimary;
      lblMsgSecondary.Text = msgSecondary;
      //[3]
      Application.DoEvents();
    }
  }
}