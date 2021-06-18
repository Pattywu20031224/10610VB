Public Class Form1
    Dim is_shoot As Boolean = True
    Dim StartGame As Boolean = True
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If StartGame = True Then
            game_intro.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            'ProgressBar1.Visible = False
        End If

        '我方飛機移動
        If e.KeyCode = Keys.Left Then
            PictureBox1.Left -= 10
        End If
        If e.KeyCode = Keys.Right Then
            PictureBox1.Left += 10
        End If

        '我方飛機穿牆
        If PictureBox1.Left < 0 - PictureBox1.Width Then
            PictureBox1.Left = Me.Width - 20
        ElseIf PictureBox1.Left > Me.Width - 20 Then
            PictureBox1.Left = 0 - PictureBox1.Width
        End If

        '我方子彈射擊
        If e.KeyCode = Keys.Space Then
            PictureBox3.Left = PictureBox1.Left + PictureBox1.Width / 2 - PictureBox3.Width / 2
            PictureBox3.Visible = True
            is_shoot = False
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PictureBox3.Visible = True
        '我方子彈移動
        PictureBox3.Top -= 40

        '我方子彈打到敵機
        If PictureBox3.Left < PictureBox2.Left + PictureBox2.Width And
           PictureBox3.Left + PictureBox3.Width > PictureBox2.Left And
           PictureBox3.Top < PictureBox2.Top + PictureBox2.Height And
           PictureBox3.Top + PictureBox3.Height > PictureBox2.Top Then
            My.Computer.Audio.Play(My.Application.Info.DirectoryPath & "\explosion2_1.wav", AudioPlayMode.Background)
            PictureBox2.Image = PictureBox4.Image
            Timer3.Enabled = False
            Timer2.Enabled = True
            Timer1.Enabled = False
            PictureBox3.Top = PictureBox1.Top
            PictureBox3.Visible = False
        End If

        '我方子彈出界
        If PictureBox3.Top < 0 - PictureBox3.Height Then
            Timer1.Enabled = False
            PictureBox3.Top = PictureBox1.Top
            PictureBox3.Visible = False
            is_shoot = True
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Randomize()
        Timer2.Enabled = False
        Timer3.Enabled = True
        PictureBox2.Image = PictureBox5.Image
        PictureBox2.Left = Int(0 + Rnd() * (Me.Width - 20 - PictureBox2.Width - 0 + 1))
        is_shoot = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs)
        '敵機飛行
        Randomize()
        PictureBox2.Left = Int((PictureBox2.Left - 30) + Rnd() * ((PictureBox2.Left + 30) - (PictureBox2.Left - 30) + 1))

        '敵機擋牆
        If PictureBox2.Left < 0 Then
            PictureBox2.Left = 0
        ElseIf PictureBox2.Left > Me.Width - 20 - PictureBox2.Width Then
            PictureBox2.Left = Me.Width - 20 - PictureBox2.Width
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        StartGame = True
        Button1.Enabled = False
        Button1.Visible = False
        game_intro.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        game_intro.Visible = True
        Button2.Enabled = False
        Button2.Visible = False
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click
        ProgressBar1.Visible = True
    End Sub

End Class
