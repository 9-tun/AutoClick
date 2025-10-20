Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Form1
    <DllImport("user32.dll")>
    Private Shared Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As Integer, vk As Integer) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function SetCursorPos(ByVal X As Integer, ByVal Y As Integer) As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Sub mouse_event(ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    End Sub

    Private Const HOTKEY_ID As Integer = 1
    Private Const MOUSEEVENTF_LEFTDOWN As Integer = &H2
    Private Const MOUSEEVENTF_LEFTUP As Integer = &H4
    Private keepRunning As Boolean = False
    Dim isChromeRemoteConnected As Boolean = False
    Dim disconnectedSince As DateTime? = Nothing
    Private WithEvents clickTimer As New Windows.Forms.Timer()
    Private WithEvents remoteTimer As New Windows.Forms.Timer()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Auto Stop Check Box Unchecked, Related Controls are disabled.
        chkAutoStop.Checked = False
        txtStopDelay.Enabled = False
        lblStopDelay.Enabled = False
        'lblRemoteStatus.Enabled = False

        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        ' Only set default if user left field empty
        If String.IsNullOrWhiteSpace(txtX.Text) Then
            txtX.Text = (screenWidth * 0.02).ToString("0")   ' 2% from left edge
        End If

        If String.IsNullOrWhiteSpace(txtY.Text) Then
            txtY.Text = (screenHeight * 0.97).ToString("0")  ' 97% from top (near bottom)
        End If

        If String.IsNullOrWhiteSpace(txtInterval.Text) Then
            txtInterval.Text = "599"   ' Default 599 seconds = nearly 10 mins
        End If

        If String.IsNullOrWhiteSpace(txtOnOff.Text) Then
            txtOnOff.Text = "F2"     ' Default hotkey
        End If

        lblStatus.Text = $"Detected Screen Size: ({screenWidth}x{screenHeight})"
        lblStatus.ForeColor = Color.Blue

        ' Register the hotkey (using whatever user entered)
        Dim hotKeyValue As Keys
        If [Enum].TryParse(txtOnOff.Text, True, hotKeyValue) Then
            RegisterHotKey(Me.Handle, HOTKEY_ID, 0, hotKeyValue)
        Else
            RegisterHotKey(Me.Handle, HOTKEY_ID, 0, Keys.F3)
        End If

        'Check Remote Connection Status
        remoteTimer.Interval = 5000   ' 5초마다 확인
        remoteTimer.Start()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_HOTKEY As Integer = &H312
        If m.Msg = WM_HOTKEY AndAlso m.WParam.ToInt32() = HOTKEY_ID Then
            ToggleAutoClick()
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub ToggleAutoClick()
        keepRunning = Not keepRunning
        If keepRunning Then
            lblStatus.Text = "Running..."
            lblStatus.ForeColor = Color.Green
            MessageBox.Show("Auto click has been turned ON.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            clickTimer.Interval = CInt(Val(txtInterval.Text) * 1000)
            clickTimer.Start()
        Else
            lblStatus.Text = "Stopped"
            lblStatus.ForeColor = Color.Red
            clickTimer.Stop()
            MessageBox.Show("Auto click has been turned OFF.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub clickTimer_Tick(sender As Object, e As EventArgs) Handles clickTimer.Tick
        Dim x As Integer = CInt(Val(txtX.Text))
        Dim y As Integer = CInt(Val(txtY.Text))

        ' Get Cursor Postion
        SetCursorPos(x, y)
        Thread.Sleep(100)
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        Thread.Sleep(50)
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)

        ' Show Msg 
        Dim msg As String = $"Windows Start menu at ({x}, {y})"
        lblStatus.Text = msg
        Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] Clicked at ({x}, {y})")

    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        UnregisterHotKey(Me.Handle, HOTKEY_ID)
        MyBase.OnFormClosed(e)
    End Sub

    Private Sub remoteTimer_Tick(sender As Object, e As EventArgs) Handles remoteTimer.Tick
        CheckRemoteStatus()
    End Sub

    Private Sub CheckRemoteStatus()
        ' Always reset before checking
        isChromeRemoteConnected = False


        ' ==========[The Way I found the Chrome Remote Connection]==========
        ' I used the command below to find connected remote control info
        ' tasklist | find "remoting_host.exe"
        ' It always retreive two list. 
        ' One of them changed according to the CRD is connected. 
        ' When Connected:   remoting_host.exe           9452 Services   0       134,036 K
        ' When DisConnected: remoting_host.exe          9316 Services   0       38,812 K
        ' So I used WorkingSet64 to found CRD Connection

        ' How I detected the Chrome Remote Desktop connection status:
        ' I used the following command to check if Chrome Remote Desktop is active:
        '     tasklist | find "remoting_host.exe"
        '
        ' The command always returns two processes.
        ' One of the two processes changes its memory usage depending on the connection state.
        '
        ' When connected:     remoting_host.exe   9452  Services   0   134,036 K
        ' When disconnected:  remoting_host.exe   9316  Services   0    38,812 K
        '
        ' Therefore, I used the WorkingSet64 property to determine
        ' whether the Chrome Remote Desktop Is currently connected.

        ' Iterate all remoting_host processes
        For Each p As Process In Process.GetProcessesByName("remoting_host")
            ' If any process uses more than 100MB, assume connection is active
            If p.WorkingSet64 > 100 * 1024 * 1024 Then
                isChromeRemoteConnected = True
                Exit For
            End If
        Next

        If isChromeRemoteConnected Then
            lblRemoteStatus.Text = "🔴 Remote Connected"
            lblRemoteStatus.ForeColor = Color.Green
            disconnectedSince = Nothing
        Else
            lblRemoteStatus.ForeColor = Color.Red

            If chkAutoStop.Checked Then

                'Record first disconnection time
                If disconnectedSince Is Nothing And InStr(lblRemoteStatus.Text, "Remote Connected") > 0 Then
                    disconnectedSince = DateTime.Now
                ElseIf disconnectedSince Is Nothing Then
                    Exit Sub
                End If


                ' Calculate elapsed and remaining time
                Dim elapsed As TimeSpan = DateTime.Now - disconnectedSince.Value
                Dim elapsedMin As Double = elapsed.TotalMinutes

                ' Read stop delay (in mins)
                Dim stopDelayMinutes As Double = 60   ' default = 60 min = 1 hour
                If Not Double.TryParse(txtStopDelay.Text, stopDelayMinutes) Then stopDelayMinutes = 60

                ' Calculate remaining time
                Dim remainingMin As Double = Math.Max(0, stopDelayMinutes - elapsedMin)
                Dim remainingText As String = $"{remainingMin:F0} mins left to Auto Stop"

                lblRemoteStatus.Text = $"🔴 Remote Disconnected{vbCrLf}    : {remainingText}"

                ' Compare with elapsed minutes
                If elapsed.TotalMinutes >= stopDelayMinutes AndAlso keepRunning Then
                    keepRunning = False
                    clickTimer.Stop()
                    lblStatus.Text = $"Stopped (Disconnected > {stopDelayMinutes} min)"
                    lblStatus.ForeColor = Color.Red
                End If

            Else
                lblRemoteStatus.Text = "🔴 Remote Disconnected"
            End If

        End If

    End Sub

    Private Sub chkAutoStop_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoStop.CheckedChanged

        If chkAutoStop.Checked Then
            ' Control enable/disable by chkAutoStop
            txtStopDelay.Enabled = True
            lblStopDelay.Enabled = True

        Else
            txtStopDelay.Enabled = False
            lblStopDelay.Enabled = False
            disconnectedSince = Nothing

        End If

    End Sub


End Class
