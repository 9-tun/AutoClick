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
    Private WithEvents clickTimer As New Windows.Forms.Timer()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ' Register the hotkey (using whatever user entered)
        Dim hotKeyValue As Keys
        If [Enum].TryParse(txtOnOff.Text, True, hotKeyValue) Then
            RegisterHotKey(Me.Handle, HOTKEY_ID, 0, hotKeyValue)
        Else
            RegisterHotKey(Me.Handle, HOTKEY_ID, 0, Keys.F3)
        End If
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
            MessageBox.Show("Auto click has been turned ON.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            clickTimer.Interval = CInt(Val(txtInterval.Text) * 1000)
            clickTimer.Start()
        Else
            lblStatus.Text = "Stopped"
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

    Private Sub txtInterval_TextChanged(sender As Object, e As EventArgs) Handles txtInterval.TextChanged

    End Sub
End Class
