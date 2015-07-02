<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CheckCode
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CheckCode))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtpwd = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdRegister = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.lnkGenKey = New System.Windows.Forms.LinkLabel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtsource = New System.Windows.Forms.TextBox
        Me.cmdBrowseKey = New System.Windows.Forms.Button
        Me.cmdBrowseSource = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtvalue = New System.Windows.Forms.TextBox
        Me.chkExp = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(9, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(53, 54)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(509, 2)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtpwd
        '
        Me.txtpwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtpwd.Location = New System.Drawing.Point(66, 110)
        Me.txtpwd.MaxLength = 500
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.Size = New System.Drawing.Size(405, 20)
        Me.txtpwd.TabIndex = 0
        Me.txtpwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 207)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(509, 2)
        Me.GroupBox2.TabIndex = 194
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 14)
        Me.Label1.TabIndex = 195
        Me.Label1.Text = "pwd"
        '
        'cmdRegister
        '
        Me.cmdRegister.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRegister.Location = New System.Drawing.Point(227, 226)
        Me.cmdRegister.Name = "cmdRegister"
        Me.cmdRegister.Size = New System.Drawing.Size(90, 30)
        Me.cmdRegister.TabIndex = 1
        Me.cmdRegister.Text = "Giải mã"
        Me.cmdRegister.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(331, 226)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(90, 30)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "&Thoát"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(75, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(426, 71)
        Me.Label10.TabIndex = 193
        Me.Label10.Text = "Check Code"
        '
        'lnkGenKey
        '
        Me.lnkGenKey.AutoSize = True
        Me.lnkGenKey.Location = New System.Drawing.Point(63, 170)
        Me.lnkGenKey.Name = "lnkGenKey"
        Me.lnkGenKey.Size = New System.Drawing.Size(0, 13)
        Me.lnkGenKey.TabIndex = 196
        Me.ToolTip1.SetToolTip(Me.lnkGenKey, "Nhấn vào đây để copy Giá trị sinh khóa gửi tới nhà cung cấp...")
        '
        'txtsource
        '
        Me.txtsource.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtsource.Location = New System.Drawing.Point(66, 136)
        Me.txtsource.MaxLength = 500
        Me.txtsource.Name = "txtsource"
        Me.txtsource.Size = New System.Drawing.Size(405, 20)
        Me.txtsource.TabIndex = 197
        Me.txtsource.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.txtsource, "Nhập giá trị cần giải mã vào đây")
        '
        'cmdBrowseKey
        '
        Me.cmdBrowseKey.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowseKey.Location = New System.Drawing.Point(477, 99)
        Me.cmdBrowseKey.Name = "cmdBrowseKey"
        Me.cmdBrowseKey.Size = New System.Drawing.Size(34, 31)
        Me.cmdBrowseKey.TabIndex = 199
        Me.cmdBrowseKey.Tag = "Rijndael"
        Me.cmdBrowseKey.Text = "..."
        Me.ToolTip1.SetToolTip(Me.cmdBrowseKey, "Chọn file regkey.dat trong thư mục chạy")
        Me.cmdBrowseKey.UseVisualStyleBackColor = True
        '
        'cmdBrowseSource
        '
        Me.cmdBrowseSource.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowseSource.Location = New System.Drawing.Point(477, 136)
        Me.cmdBrowseSource.Name = "cmdBrowseSource"
        Me.cmdBrowseSource.Size = New System.Drawing.Size(34, 31)
        Me.cmdBrowseSource.TabIndex = 203
        Me.cmdBrowseSource.Tag = "Rijndael"
        Me.cmdBrowseSource.Text = "..."
        Me.ToolTip1.SetToolTip(Me.cmdBrowseSource, "Chọn file chứa dữ liệu cần giải mã")
        Me.cmdBrowseSource.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 14)
        Me.Label2.TabIndex = 198
        Me.Label2.Text = "Source"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 14)
        Me.Label3.TabIndex = 201
        Me.Label3.Text = "valA"
        '
        'txtvalue
        '
        Me.txtvalue.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtvalue.Location = New System.Drawing.Point(66, 167)
        Me.txtvalue.MaxLength = 500
        Me.txtvalue.Name = "txtvalue"
        Me.txtvalue.ReadOnly = True
        Me.txtvalue.Size = New System.Drawing.Size(405, 20)
        Me.txtvalue.TabIndex = 200
        Me.txtvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkExp
        '
        Me.chkExp.AutoSize = True
        Me.chkExp.Location = New System.Drawing.Point(66, 87)
        Me.chkExp.Name = "chkExp"
        Me.chkExp.Size = New System.Drawing.Size(156, 17)
        Me.chkExp.TabIndex = 202
        Me.chkExp.Text = "Đang giải mã hạn sử dụng?"
        Me.chkExp.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(85, 226)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 30)
        Me.Button1.TabIndex = 204
        Me.Button1.Text = "Gen LasttimeAccess"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frm_CheckCode
        '
        Me.AcceptButton = Me.cmdRegister
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(513, 273)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdBrowseSource)
        Me.Controls.Add(Me.chkExp)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtvalue)
        Me.Controls.Add(Me.cmdBrowseKey)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtsource)
        Me.Controls.Add(Me.lnkGenKey)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdRegister)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtpwd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_CheckCode"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "DigitalRadiologyOperationConsole"
        Me.Text = "Đăng ký khóa cho ứng dụng"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtpwd As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdRegister As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lnkGenKey As System.Windows.Forms.LinkLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtsource As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowseKey As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtvalue As System.Windows.Forms.TextBox
    Friend WithEvents chkExp As System.Windows.Forms.CheckBox
    Friend WithEvents cmdBrowseSource As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
