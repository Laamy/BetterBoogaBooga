# Functions

```cpp
void OnKeyEvent(KeyEventFunc func);
```
Connect func to the keyevent stream

# Details

```cpp
local function OnKeyStuff(key, held)
    print(tostring(key) .. " " .. tostring(held))
end)

UserInput.OnKeyEvent(OnKeyStuff)```
