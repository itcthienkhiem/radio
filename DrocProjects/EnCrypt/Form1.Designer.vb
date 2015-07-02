<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.lbldbpwd = New System.Windows.Forms.Label
        Me.lblSum = New System.Windows.Forms.Label
        Me.lblpSum = New System.Windows.Forms.Label
        Me.lblAlgorithsm = New System.Windows.Forms.Label
        Me.lblAllowSum = New System.Windows.Forms.Label
        Me.lblAllowpSum = New System.Windows.Forms.Label
        Me.lblCropAndMark = New System.Windows.Forms.Label
        Me.lblWYSIWYG = New System.Windows.Forms.Label
        Me.lblZap = New System.Windows.Forms.Label
        Me.lblCam = New System.Windows.Forms.Label
        Me.lblFreeMemoryOnCell = New System.Windows.Forms.Label
        Me.lblFMOC = New System.Windows.Forms.Label
        Me.lblBurnPath = New System.Windows.Forms.Label
        Me.lblAllowBurn = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lbldbpwd
        '
        Me.lbldbpwd.AutoSize = True
        Me.lbldbpwd.Location = New System.Drawing.Point(0, 0)
        Me.lbldbpwd.Name = "lbldbpwd"
        Me.lbldbpwd.Size = New System.Drawing.Size(0, 13)
        Me.lbldbpwd.TabIndex = 0
        Me.lbldbpwd.Tag = "VBITDROC"
        '
        'lblSum
        '
        Me.lblSum.AutoSize = True
        Me.lblSum.Location = New System.Drawing.Point(182, 142)
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(50, 13)
        Me.lblSum.TabIndex = 1
        Me.lblSum.Tag = "Leadtools.Memory.Sum.dll"
        Me.lblSum.Text = "SumPath"
        '
        'lblpSum
        '
        Me.lblpSum.AutoSize = True
        Me.lblpSum.Location = New System.Drawing.Point(185, 169)
        Me.lblpSum.Name = "lblpSum"
        Me.lblpSum.Size = New System.Drawing.Size(56, 13)
        Me.lblpSum.TabIndex = 2
        Me.lblpSum.Tag = "Leadtools.Memory.Psum.dll"
        Me.lblpSum.Text = "pSumPath"
        '
        'lblAlgorithsm
        '
        Me.lblAlgorithsm.AutoSize = True
        Me.lblAlgorithsm.Location = New System.Drawing.Point(85, 196)
        Me.lblAlgorithsm.Name = "lblAlgorithsm"
        Me.lblAlgorithsm.Size = New System.Drawing.Size(39, 13)
        Me.lblAlgorithsm.TabIndex = 3
        Me.lblAlgorithsm.Tag = "Rijndael"
        Me.lblAlgorithsm.Text = "Label1"
        '
        'lblAllowSum
        '
        Me.lblAllowSum.AutoSize = True
        Me.lblAllowSum.Location = New System.Drawing.Point(182, 212)
        Me.lblAllowSum.Name = "lblAllowSum"
        Me.lblAllowSum.Size = New System.Drawing.Size(63, 13)
        Me.lblAllowSum.TabIndex = 4
        Me.lblAllowSum.Tag = "Allow Memory Saver"
        Me.lblAllowSum.Text = "lblAllowSum"
        '
        'lblAllowpSum
        '
        Me.lblAllowpSum.AutoSize = True
        Me.lblAllowpSum.Location = New System.Drawing.Point(182, 240)
        Me.lblAllowpSum.Name = "lblAllowpSum"
        Me.lblAllowpSum.Size = New System.Drawing.Size(69, 13)
        Me.lblAllowpSum.TabIndex = 5
        Me.lblAllowpSum.Tag = "Allow Print Memory Saver"
        Me.lblAllowpSum.Text = "lblAllowpSum"
        '
        'lblCropAndMark
        '
        Me.lblCropAndMark.AutoSize = True
        Me.lblCropAndMark.Location = New System.Drawing.Point(120, 9)
        Me.lblCropAndMark.Name = "lblCropAndMark"
        Me.lblCropAndMark.Size = New System.Drawing.Size(152, 13)
        Me.lblCropAndMark.TabIndex = 6
        Me.lblCropAndMark.Tag = "Allow Crop And Mark Together"
        Me.lblCropAndMark.Text = "Allow Crop And Mark Together"
        '
        'lblWYSIWYG
        '
        Me.lblWYSIWYG.AutoSize = True
        Me.lblWYSIWYG.Location = New System.Drawing.Point(120, 34)
        Me.lblWYSIWYG.Name = "lblWYSIWYG"
        Me.lblWYSIWYG.Size = New System.Drawing.Size(107, 13)
        Me.lblWYSIWYG.TabIndex = 7
        Me.lblWYSIWYG.Tag = "Allow Zoom and Print"
        Me.lblWYSIWYG.Text = "Allow Zoom and Print"
        '
        'lblZap
        '
        Me.lblZap.AutoSize = True
        Me.lblZap.Location = New System.Drawing.Point(123, 87)
        Me.lblZap.Name = "lblZap"
        Me.lblZap.Size = New System.Drawing.Size(28, 13)
        Me.lblZap.TabIndex = 9
        Me.lblZap.Tag = "Leadtools.Codecs.Zap.dll"
        Me.lblZap.Text = "ZAP"
        '
        'lblCam
        '
        Me.lblCam.AutoSize = True
        Me.lblCam.Location = New System.Drawing.Point(120, 60)
        Me.lblCam.Name = "lblCam"
        Me.lblCam.Size = New System.Drawing.Size(30, 13)
        Me.lblCam.TabIndex = 8
        Me.lblCam.Tag = "Leadtools.Codecs.Cam.dll"
        Me.lblCam.Text = "CAM"
        '
        'lblFreeMemoryOnCell
        '
        Me.lblFreeMemoryOnCell.AutoSize = True
        Me.lblFreeMemoryOnCell.Location = New System.Drawing.Point(32, 123)
        Me.lblFreeMemoryOnCell.Name = "lblFreeMemoryOnCell"
        Me.lblFreeMemoryOnCell.Size = New System.Drawing.Size(246, 13)
        Me.lblFreeMemoryOnCell.TabIndex = 10
        Me.lblFreeMemoryOnCell.Tag = "Allow Free Memory Captured By MedicalViewerCell"
        Me.lblFreeMemoryOnCell.Text = "Allow Free Memory Captured By MedicalViewerCell"
        '
        'lblFMOC
        '
        Me.lblFMOC.AutoSize = True
        Me.lblFMOC.Location = New System.Drawing.Point(13, 169)
        Me.lblFMOC.Name = "lblFMOC"
        Me.lblFMOC.Size = New System.Drawing.Size(37, 13)
        Me.lblFMOC.TabIndex = 11
        Me.lblFMOC.Tag = "Leadtools.Memory.Fmoc.dll"
        Me.lblFMOC.Text = "FMOC"
        '
        'lblBurnPath
        '
        Me.lblBurnPath.AutoSize = True
        Me.lblBurnPath.Location = New System.Drawing.Point(22, 240)
        Me.lblBurnPath.Name = "lblBurnPath"
        Me.lblBurnPath.Size = New System.Drawing.Size(38, 13)
        Me.lblBurnPath.TabIndex = 12
        Me.lblBurnPath.Tag = "Leadtools.Memory.Burning.dll"
        Me.lblBurnPath.Text = "BURN"
        '
        'lblAllowBurn
        '
        Me.lblAllowBurn.AutoSize = True
        Me.lblAllowBurn.Location = New System.Drawing.Point(17, 212)
        Me.lblAllowBurn.Name = "lblAllowBurn"
        Me.lblAllowBurn.Size = New System.Drawing.Size(57, 13)
        Me.lblAllowBurn.TabIndex = 13
        Me.lblAllowBurn.Tag = "Allow Burning Text 16"
        Me.lblAllowBurn.Text = "Allow Burn"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.lblAllowBurn)
        Me.Controls.Add(Me.lblBurnPath)
        Me.Controls.Add(Me.lblFMOC)
        Me.Controls.Add(Me.lblFreeMemoryOnCell)
        Me.Controls.Add(Me.lblZap)
        Me.Controls.Add(Me.lblCam)
        Me.Controls.Add(Me.lblWYSIWYG)
        Me.Controls.Add(Me.lblCropAndMark)
        Me.Controls.Add(Me.lblAllowpSum)
        Me.Controls.Add(Me.lblAllowSum)
        Me.Controls.Add(Me.lblAlgorithsm)
        Me.Controls.Add(Me.lblpSum)
        Me.Controls.Add(Me.lblSum)
        Me.Controls.Add(Me.lbldbpwd)
        Me.Name = "Form1"
        Me.Tag = "DQMNNQDVC080920080111198315011981"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbldbpwd As System.Windows.Forms.Label
    Friend WithEvents lblSum As System.Windows.Forms.Label
    Friend WithEvents lblpSum As System.Windows.Forms.Label
    Friend WithEvents lblAlgorithsm As System.Windows.Forms.Label
    Friend WithEvents lblAllowSum As System.Windows.Forms.Label
    Friend WithEvents lblAllowpSum As System.Windows.Forms.Label
    Friend WithEvents lblCropAndMark As System.Windows.Forms.Label
    Friend WithEvents lblWYSIWYG As System.Windows.Forms.Label
    Friend WithEvents lblZap As System.Windows.Forms.Label
    Friend WithEvents lblCam As System.Windows.Forms.Label
    Friend WithEvents lblFreeMemoryOnCell As System.Windows.Forms.Label
    Friend WithEvents lblFMOC As System.Windows.Forms.Label
    Friend WithEvents lblBurnPath As System.Windows.Forms.Label
    Friend WithEvents lblAllowBurn As System.Windows.Forms.Label

End Class
