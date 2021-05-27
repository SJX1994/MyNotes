Attribute VB_Name = "模块1"
'Option Explicit

Public GV As Variables_Global

Type Variables_Global

     shtName As String
     nameTitle As String
     typeName As Variant
     addNew As Variant
    
End Type





Sub Main()
    
    DeletAll
    
    'Initialize
   
    'RowNum_Creat
    
    'ComboBox_Create
    
End Sub




Private Sub Initialize()
    
    
    
    
   
    
    
    
    GV.shtName = ""
    
    GV.nameTitle = " "
    
    GV.typeName = Array("", "")
    
    GV.addNew = Array("", "")
    
    
    
    GV.shtName = "Sheet1"
    
    GV.nameTitle = "项目名称2 "
    
    GV.typeName = Array("1", "2", "3")
    
    GV.addNew = Array("2", "4")
    
   
    Log ("初始化成功")
    
End Sub





Private Sub Log(tex)

    MsgBox (tex)
    
End Sub


Private Sub RowNum_Creat()
   
   

   Set sht = ThisWorkbook.Worksheets(GV.shtName)
   
    For i = GV.addNew(0) To GV.addNew(1)

        sht.Cells(i, 1) = GV.nameTitle & i

    Next
    
    
End Sub


Private Sub ComboBox_Create()
    
    'PURPOSE: Create a form control combo box and position/size it

    Dim Cell As Range
    'Dim sht As Worksheet
    Dim myDropDown As Shape
    Dim myArray As Variant
    
    'myArray = Array("Q1", "Q2")
    
    Set sht = ThisWorkbook.Worksheets(GV.shtName)
    
   
    For i = 2 To sht.UsedRange.Rows.Count
    
          'Create & Dimension to a Specific Cell Range
          Set Cell = Range("D" & i & ":D" & i)
          
          With Cell
            sht.DropDowns.Add(.Left, .Top, .Width, .Height).Name = "Combo Box " & i
            
          End With
          
          Set myDropDown = sht.Shapes("Combo Box " & i)
          
          myDropDown.OLEFormat.Object.List = GV.typeName
            
        
          
    Next
    

End Sub

Private Sub DeletAll()
    Set sht = ThisWorkbook.Worksheets(GV.shtName)
   
    
    For i = 0 To 1000
       Dim myDropDown As Shape
       Set myDropDown = sht.Shapes("Combo Box " & i)
       
       If IsNull(myDropDown) Then
            
            
            On Error Resume Next
       Else
            sht.Shapes("Combo Box " & i).Delete
            
       End If
            
    Next
    
        
        
    
End Sub
