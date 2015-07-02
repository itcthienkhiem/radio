<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateKey
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateKey))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdRegister = New System.Windows.Forms.Button()
        Me.cmdDel = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.optLis = New System.Windows.Forms.RadioButton()
        Me.optDcmViewer = New System.Windows.Forms.RadioButton()
        Me.optRISlink = New System.Windows.Forms.RadioButton()
        Me.optDROC = New System.Windows.Forms.RadioButton()
        Me.txtpwd = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpExpd = New System.Windows.Forms.DateTimePicker()
        Me.lblExpr = New System.Windows.Forms.Label()
        Me.cmdCopy2Clipboard = New System.Windows.Forms.Button()
        Me.pnlpublic = New System.Windows.Forms.Panel()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlpublic.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(53, 54)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 66)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(780, 2)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtKey
        '
        Me.txtKey.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtKey.Location = New System.Drawing.Point(112, 258)
        Me.txtKey.MaxLength = 50000
        Me.txtKey.Multiline = True
        Me.txtKey.Name = "txtKey"
        Me.txtKey.ReadOnly = True
        Me.txtKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtKey.Size = New System.Drawing.Size(610, 107)
        Me.txtKey.TabIndex = 0
        Me.txtKey.Tag = "Leadtools.Memory.Sum.dll"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(50, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(726, 62)
        Me.Label10.TabIndex = 193
        Me.Label10.Text = "THANK YOUR FOR CHOOSING OUR SOFTWARE"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 371)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(780, 2)
        Me.GroupBox2.TabIndex = 194
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 261)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 14)
        Me.Label1.TabIndex = 195
        Me.Label1.Text = "Khóa kích hoạt:"
        '
        'cmdRegister
        '
        Me.cmdRegister.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRegister.Location = New System.Drawing.Point(112, 390)
        Me.cmdRegister.Name = "cmdRegister"
        Me.cmdRegister.Size = New System.Drawing.Size(83, 24)
        Me.cmdRegister.TabIndex = 1
        Me.cmdRegister.Text = "Đăng ký"
        Me.cmdRegister.UseVisualStyleBackColor = True
        '
        'cmdDel
        '
        Me.cmdDel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDel.Location = New System.Drawing.Point(201, 390)
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.Size = New System.Drawing.Size(83, 24)
        Me.cmdDel.TabIndex = 2
        Me.cmdDel.Text = "Xóa khóa"
        Me.cmdDel.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(290, 390)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(83, 24)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "Thoát"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 14)
        Me.Label2.TabIndex = 197
        Me.Label2.Text = "Giá trị sinh khóa:"
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtValue.Location = New System.Drawing.Point(112, 152)
        Me.txtValue.MaxLength = 50000
        Me.txtValue.Multiline = True
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtValue.Size = New System.Drawing.Size(607, 100)
        Me.txtValue.TabIndex = 196
        '
        'optLis
        '
        Me.optLis.AutoSize = True
        Me.optLis.Checked = True
        Me.optLis.Location = New System.Drawing.Point(50, 25)
        Me.optLis.Name = "optLis"
        Me.optLis.Size = New System.Drawing.Size(61, 17)
        Me.optLis.TabIndex = 198
        Me.optLis.TabStop = True
        Me.optLis.Text = "LABlink"
        Me.optLis.UseVisualStyleBackColor = True
        '
        'optDcmViewer
        '
        Me.optDcmViewer.AutoSize = True
        Me.optDcmViewer.Location = New System.Drawing.Point(191, 25)
        Me.optDcmViewer.Name = "optDcmViewer"
        Me.optDcmViewer.Size = New System.Drawing.Size(87, 17)
        Me.optDcmViewer.TabIndex = 199
        Me.optDcmViewer.Text = "DicomViewer"
        Me.optDcmViewer.UseVisualStyleBackColor = True
        '
        'optRISlink
        '
        Me.optRISlink.AutoSize = True
        Me.optRISlink.Location = New System.Drawing.Point(115, 25)
        Me.optRISlink.Name = "optRISlink"
        Me.optRISlink.Size = New System.Drawing.Size(59, 17)
        Me.optRISlink.TabIndex = 200
        Me.optRISlink.Text = "RISlink"
        Me.optRISlink.UseVisualStyleBackColor = True
        '
        'optDROC
        '
        Me.optDROC.AutoSize = True
        Me.optDROC.Location = New System.Drawing.Point(284, 25)
        Me.optDROC.Name = "optDROC"
        Me.optDROC.Size = New System.Drawing.Size(56, 17)
        Me.optDROC.TabIndex = 201
        Me.optDROC.Text = "DROC"
        Me.optDROC.UseVisualStyleBackColor = True
        '
        'txtpwd
        '
        Me.txtpwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtpwd.Location = New System.Drawing.Point(113, 76)
        Me.txtpwd.MaxLength = 50000
        Me.txtpwd.Multiline = True
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtpwd.Size = New System.Drawing.Size(607, 70)
        Me.txtpwd.TabIndex = 202
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 14)
        Me.Label3.TabIndex = 203
        Me.Label3.Text = "p"
        '
        'dtpExpd
        '
        Me.dtpExpd.CustomFormat = "dd/MM/yyyy"
        Me.dtpExpd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpd.Location = New System.Drawing.Point(115, 50)
        Me.dtpExpd.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpExpd.Name = "dtpExpd"
        Me.dtpExpd.ShowUpDown = True
        Me.dtpExpd.Size = New System.Drawing.Size(200, 20)
        Me.dtpExpd.TabIndex = 204
        '
        'lblExpr
        '
        Me.lblExpr.AutoSize = True
        Me.lblExpr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpr.Location = New System.Drawing.Point(10, 56)
        Me.lblExpr.Name = "lblExpr"
        Me.lblExpr.Size = New System.Drawing.Size(33, 14)
        Me.lblExpr.TabIndex = 205
        Me.lblExpr.Text = "ExpD"
        '
        'cmdCopy2Clipboard
        '
        Me.cmdCopy2Clipboard.Location = New System.Drawing.Point(728, 258)
        Me.cmdCopy2Clipboard.Name = "cmdCopy2Clipboard"
        Me.cmdCopy2Clipboard.Size = New System.Drawing.Size(28, 23)
        Me.cmdCopy2Clipboard.TabIndex = 212
        Me.cmdCopy2Clipboard.Text = "..."
        Me.cmdCopy2Clipboard.UseVisualStyleBackColor = True
        '
        'pnlpublic
        '
        Me.pnlpublic.Controls.Add(Me.optLis)
        Me.pnlpublic.Controls.Add(Me.txtKey)
        Me.pnlpublic.Controls.Add(Me.Label1)
        Me.pnlpublic.Controls.Add(Me.cmdCopy2Clipboard)
        Me.pnlpublic.Controls.Add(Me.txtValue)
        Me.pnlpublic.Controls.Add(Me.Label2)
        Me.pnlpublic.Controls.Add(Me.optDcmViewer)
        Me.pnlpublic.Controls.Add(Me.optRISlink)
        Me.pnlpublic.Controls.Add(Me.optDROC)
        Me.pnlpublic.Controls.Add(Me.cmdClose)
        Me.pnlpublic.Controls.Add(Me.txtpwd)
        Me.pnlpublic.Controls.Add(Me.cmdDel)
        Me.pnlpublic.Controls.Add(Me.Label3)
        Me.pnlpublic.Controls.Add(Me.cmdRegister)
        Me.pnlpublic.Controls.Add(Me.lblExpr)
        Me.pnlpublic.Controls.Add(Me.GroupBox2)
        Me.pnlpublic.Controls.Add(Me.dtpExpd)
        Me.pnlpublic.Location = New System.Drawing.Point(3, 69)
        Me.pnlpublic.Name = "pnlpublic"
        Me.pnlpublic.Size = New System.Drawing.Size(773, 481)
        Me.pnlpublic.TabIndex = 215
        Me.pnlpublic.Visible = False
        '
        'CreateKey
        '
        Me.AcceptButton = Me.cmdRegister
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.pnlpublic)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CreateKey"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tạo khóa ứng dụng cho LABlink,DicomViewer và RISlink"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlpublic.ResumeLayout(False)
        Me.pnlpublic.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdRegister As System.Windows.Forms.Button
    Friend WithEvents cmdDel As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents optLis As System.Windows.Forms.RadioButton
    Friend WithEvents optDcmViewer As System.Windows.Forms.RadioButton
    Friend WithEvents optRISlink As System.Windows.Forms.RadioButton
    Friend WithEvents optDROC As System.Windows.Forms.RadioButton
    Friend WithEvents txtpwd As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpExpd As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblExpr As System.Windows.Forms.Label
    Friend WithEvents cmdCopy2Clipboard As System.Windows.Forms.Button
    Friend WithEvents pnlpublic As System.Windows.Forms.Panel

End Class
