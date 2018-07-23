# RetroArch-Cheats-Manager

WPF Application that allows the user to create and edit cheat files used by RetroArch.

----------------------------------------------------------------------------------------------------

# Known Issues:
* Crash When Canceling the OpenFileDialog
* Crash When Doing Anything Out Of Order

----------------------------------------------------------------------------------------------------

# How To Use:

* Editing an Existing Cheat
  1. Open a file via "File > Open".
  2. Select a Cheat on the left pane and press the "Edit Cheat" button.
  3. Edit the Cheat Name and Code on the right panes, when done press the "Save Cheat" button.
  4. When done editing cheats use "File > Save" to save your new cheats file.
* Creating a New Cheats File
  1. Create a new file via "File > New", select where to save the file and give it a meaningful name.
  2. Enter a Cheat Name and it's corresponding code(s) in the text boxes on the right half of the window.
  3. Press the "Add As New Cheat" or "Save Cheat" button to add the new cheat entry.
  4. When done, use "File > Save" ro save your new cheats file.
  
## For Multi-Line Cheats

Enter each code on it's own line inside the "Cheat Codes" textbox, the program will format the text appropriatly when saving the cheat file. **DO NOT ENTER RANDOM TEXT INTO THIS TEXTBOX**, you must enter the cheat code exactly as it appears from where you have sourced it from. The application will automatically filter out extra spaces and newline characters, _it will not filter out bad codes or any additional text._

----------------------------------------------------------------------------------------------------

# ToDo:
* Add exception checks to avoid crashes
* Allow user to modify a specific cheat's index (So it can appear earlier or later in the list in RetroArch)
