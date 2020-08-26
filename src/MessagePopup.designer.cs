namespace Ailanthus
{
  partial class MessagePopup
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lblMsgTitle = new System.Windows.Forms.Label();
      this.lblMsgPrimary = new System.Windows.Forms.Label();
      this.pnlBkgrnd = new System.Windows.Forms.Panel();
      this.lblMsgSecondary = new System.Windows.Forms.Label();
      this.pnlBkgrnd.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblMsgTitle
      // 
      this.lblMsgTitle.BackColor = System.Drawing.Color.White;
      this.lblMsgTitle.Font = new System.Drawing.Font("Dust West", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMsgTitle.ForeColor = System.Drawing.Color.Black;
      this.lblMsgTitle.Location = new System.Drawing.Point(9, 15);
      this.lblMsgTitle.Name = "lblMsgTitle";
      this.lblMsgTitle.Size = new System.Drawing.Size(357, 38);
      this.lblMsgTitle.TabIndex = 0;
      this.lblMsgTitle.Text = "Popup Title";
      this.lblMsgTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblMsgPrimary
      // 
      this.lblMsgPrimary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMsgPrimary.Location = new System.Drawing.Point(3, 51);
      this.lblMsgPrimary.Name = "lblMsgPrimary";
      this.lblMsgPrimary.Size = new System.Drawing.Size(369, 28);
      this.lblMsgPrimary.TabIndex = 1;
      this.lblMsgPrimary.Text = "Primary message";
      this.lblMsgPrimary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pnlBkgrnd
      // 
      this.pnlBkgrnd.BackColor = System.Drawing.Color.White;
      this.pnlBkgrnd.Controls.Add(this.lblMsgPrimary);
      this.pnlBkgrnd.Controls.Add(this.lblMsgSecondary);
      this.pnlBkgrnd.Controls.Add(this.lblMsgTitle);
      this.pnlBkgrnd.Location = new System.Drawing.Point(3, 3);
      this.pnlBkgrnd.Name = "pnlBkgrnd";
      this.pnlBkgrnd.Size = new System.Drawing.Size(375, 133);
      this.pnlBkgrnd.TabIndex = 2;
      // 
      // lblMsgSecondary
      // 
      this.lblMsgSecondary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMsgSecondary.Location = new System.Drawing.Point(3, 70);
      this.lblMsgSecondary.Name = "lblMsgSecondary";
      this.lblMsgSecondary.Size = new System.Drawing.Size(369, 28);
      this.lblMsgSecondary.TabIndex = 2;
      this.lblMsgSecondary.Text = "Secondary message";
      this.lblMsgSecondary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // MessagePopup
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(381, 139);
      this.Controls.Add(this.pnlBkgrnd);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "MessagePopup";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PleaseWait";
      this.TopMost = true;
      this.TransparencyKey = System.Drawing.Color.Blue;
      this.pnlBkgrnd.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblMsgTitle;
    private System.Windows.Forms.Label lblMsgPrimary;
    private System.Windows.Forms.Panel pnlBkgrnd;
    private System.Windows.Forms.Label lblMsgSecondary;
  }
}