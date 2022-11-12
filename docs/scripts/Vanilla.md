# Functions

```cpp
void print(const char* str);
```
Lets you print to console

```cpp
void warn(const char* str);
```
Lets you print a warning to console

```cpp
void error(const char* str);
```
Cast an error in the scripts tracks with a custom message

```cpp
void wait(float x);
```
Wait "x" amount of seconds

```cpp
bool iswindowactive();
```
Check if roblox is in focus/the currently active window

```cpp
void keypress(char key);
```
Simulate a keypress

```cpp
void keyrelease(char key);
```
Simulate a keyrelease

```cpp
void rconsoleprint(const char* str);
```
Lets you print to the external console window

```cpp
void rconsoleclear();
```
Clear the external console window

```cpp
void rconsolename(const char* str);
```
Set the external console window title

```cpp
const char* rconsoleinput();
```
Yields the thread then returns whatever the user has entered as input into the external console

```cpp
void* hookfunction(void* orig, void* detour);
```
Replace orig with your own custom detour function then return the pointer back to the original
