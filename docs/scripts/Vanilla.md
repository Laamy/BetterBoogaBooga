# Functions

```cpp
const char* getexecutorname();
const char* identifyexecutor();
```
Returns the name of the executor (To match up with scriptware)
</br> </br> </br>

```cpp
void print(const char* str);
```
Lets you print to console
</br> </br> </br>

```cpp
void warn(const char* str);
```
Lets you print a warning to 
</br> </br> </br>

```cpp
void error(const char* str);
```
Cast an error in the scripts tracks with a custom message
</br> </br> </br>

```cpp
void wait(float x);
```
Wait "x" amount of seconds (not linked to framerate so you can wait for as little as you want)
</br> </br> </br>

```cpp
bool iswindowactive();
```
Check if roblox is in focus/the currently active window
</br> </br> </br>

```cpp
void keypress(char key);
```
Simulate a keypress
</br> </br> </br>

```cpp
void keyrelease(char key);
```
Simulate a keyrelease
</br> </br> </br>

```cpp
void mouse1click();
```
Simulate a full left mouse click
</br> </br> </br>

```cpp
void mouse1press();
```
Simulate a left mouse press
</br> </br> </br>

```cpp
void mouse1release();
```
Simulate a left mouse release
</br> </br> </br>

```cpp
void mouse2click();
```
Simulate a full right mouse click
</br> </br> </br>

```cpp
void mouse2press();
```
Simulate a right mouse press
</br> </br> </br>

```cpp
void mouse2release();
```
Simulate a right mouse release
</br> </br> </br>

```cpp
void rconsoleprint(const char* str);
```
Lets you print to the external console window
</br> </br> </br>

```cpp
void rconsoleclear();
```
Clear the external console window
</br> </br> </br>

```cpp
void rconsolename(const char* str);
```
Set the external console window title
</br> </br> </br>

```cpp
int, int getmouse();
```
get the crosshair position roblox window wise
</br> </br> </br>

```cpp
void setclipboard(const char* str);
```
Set the global clipboard to str
</br> </br> </br>

```cpp
void messagebox(const char* title, const char* captions, uint flags);
```
Show a messagebox window (only works if roblox window is the currently active window)
</br> </br> </br>

```cpp
const char* rconsoleinput();
```
Yields the thread then returns whatever the user has entered as input into the external console
</br> </br> </br>

```cpp
void* hookfunction(void* orig, void* detour);
```
Replace orig with your own custom detour function then return the pointer back to the original

# Details

rconsoleprint the color codes (NOT ID) for the respected color
| Id | Color  | Code |
| ------------- | ------------- | ------------- |
| 0 | Black  | !BLACK!  |
| 1 | Blue | !BLUE! |
| 2 | Green | !GREEN! |
| 3 | Cyan | !CYAN! |
| 4 | Red | !RED! |
| 5 | Magenta | !MAGENTA! |
| 6 | Brown | !BROWN! |
| 7 | Light Gray | !LIGHT_GRAY! |
| 8 | Dark Gray | !DARK_GRAY! |
| 9 | Light Blue  | !LIGHT_BLUE! |
| 10 | Light Green | !LIGHT_GREEM! |
| 11 | Light Cyan | !LIGHT_CYAN! |
| 12 | Light Red | !LIGHT_RED! |
| 13 | Light Magenta | !LIGHT_MAGENTA! |
| 14 | Yellow | !YELLOW! |
| 15 | White | !WHITE! |
