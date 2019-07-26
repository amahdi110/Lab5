Option Strict On
Imports System.IO

Public Class frmTextEditor

    Dim filePath As String = ""
    Dim windowTitle As String = "Text Editor - New file"
    Private Sub SaveAsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click

        SaveFileDialog1.Filter = "TXT Files (*.txt*)|*.txt"
        Me.Text = "Text Editor - Select the save destination"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, txtText.Text, False)
            filePath = SaveFileDialog1.FileName
            windowTitle = "Text Editor - " + filePath
        End If

        Me.Text = windowTitle
    End Sub

    Private Sub OpenAsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenAsToolStripMenuItem.Click
        Me.Text = "Text Editor - Browse and open a text file"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                Dim reader As New StreamReader(OpenFileDialog1.FileName)
                txtText.Text = reader.ReadToEnd
                reader.Close()
                filePath = OpenFileDialog1.FileName
                windowTitle = "Text Editor - " + filePath
            Catch ex As Exception
                Throw New ApplicationException(ex.ToString)
            End Try
        End If

        Me.Text = windowTitle

    End Sub

    Private Sub CloseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub NewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        filePath = String.Empty
        txtText.Text = String.Empty
        windowTitle = "Text Editor - New file"
        Me.Text = windowTitle
    End Sub

    Private Sub SaveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Me.Text = "Text Editor - Save file"
        If filePath = "" Then
            SaveFileDialog1.Filter = "TXT Files (*.txt*)|*.txt"

            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, txtText.Text, False)
                filePath = SaveFileDialog1.FileName
                windowTitle = "Text Editor - " + filePath
            End If
        Else
            My.Computer.FileSystem.WriteAllText(filePath, txtText.Text, False)
        End If

        Me.Text = windowTitle

    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        If Not txtText.SelectionLength = 0 Then
            My.Computer.Clipboard.SetText(txtText.SelectedText)
            txtText.SelectedText = ""
            Me.Text = "Text Editor - " + filePath + " (Cut)"
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If Not txtText.SelectionLength = 0 Then
            My.Computer.Clipboard.SetText(txtText.SelectedText)
            Me.Text = "Text Editor - " + filePath + " (Copy)"
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        txtText.Paste(My.Computer.Clipboard.GetText())
        Me.Text = "Text Editor - " + filePath + " (Paste)"
    End Sub

End Class
