<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOnOff = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblRemoteStatus = New System.Windows.Forms.Label()
        Me.lblStopDelay = New System.Windows.Forms.Label()
        Me.txtStopDelay = New System.Windows.Forms.TextBox()
        Me.chkAutoStop = New System.Windows.Forms.CheckBox()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Interval (secs)"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(170, 9)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(77, 21)
        Me.txtInterval.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Win Menu X"
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(170, 36)
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(77, 21)
        Me.txtX.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Win Menu Y"
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(170, 63)
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(77, 21)
        Me.txtY.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "On/Off Short Cut Key"
        '
        'txtOnOff
        '
        Me.txtOnOff.Location = New System.Drawing.Point(170, 90)
        Me.txtOnOff.Name = "txtOnOff"
        Me.txtOnOff.Size = New System.Drawing.Size(77, 21)
        Me.txtOnOff.TabIndex = 1
        Me.txtOnOff.Text = "F2"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(33, 126)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(92, 12)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.Text = "Program Status"
        '
        'lblRemoteStatus
        '
        Me.lblRemoteStatus.AutoSize = True
        Me.lblRemoteStatus.Location = New System.Drawing.Point(21, 78)
        Me.lblRemoteStatus.Name = "lblRemoteStatus"
        Me.lblRemoteStatus.Size = New System.Drawing.Size(87, 12)
        Me.lblRemoteStatus.TabIndex = 2
        Me.lblRemoteStatus.Text = "Remote Status"
        '
        'lblStopDelay
        '
        Me.lblStopDelay.AutoSize = True
        Me.lblStopDelay.Location = New System.Drawing.Point(21, 51)
        Me.lblStopDelay.Name = "lblStopDelay"
        Me.lblStopDelay.Size = New System.Drawing.Size(133, 12)
        Me.lblStopDelay.TabIndex = 0
        Me.lblStopDelay.Text = "Auto-stop delay(mins)"
        '
        'txtStopDelay
        '
        Me.txtStopDelay.Location = New System.Drawing.Point(158, 47)
        Me.txtStopDelay.Name = "txtStopDelay"
        Me.txtStopDelay.Size = New System.Drawing.Size(77, 21)
        Me.txtStopDelay.TabIndex = 1
        Me.txtStopDelay.Text = "60"
        '
        'chkAutoStop
        '
        Me.chkAutoStop.AutoSize = True
        Me.chkAutoStop.Location = New System.Drawing.Point(23, 20)
        Me.chkAutoStop.Name = "chkAutoStop"
        Me.chkAutoStop.Size = New System.Drawing.Size(121, 16)
        Me.chkAutoStop.TabIndex = 3
        Me.chkAutoStop.Text = "Auto-Stop On/Off"
        Me.chkAutoStop.UseVisualStyleBackColor = True
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblStopDelay)
        Me.GroupBox1.Controls.Add(Me.chkAutoStop)
        Me.GroupBox1.Controls.Add(Me.lblRemoteStatus)
        Me.GroupBox1.Controls.Add(Me.txtStopDelay)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 156)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 118)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Auto-Stop"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 286)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.txtOnOff)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtY)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "NRCAD Click"
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtInterval As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtX As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtY As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtOnOff As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblRemoteStatus As Label
    Friend WithEvents lblStopDelay As Label
    Friend WithEvents txtStopDelay As TextBox
    Friend WithEvents chkAutoStop As CheckBox
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents GroupBox1 As GroupBox
End Class
