# Functions

```cpp
void OnKeyEvent(KeyEventFunc func);
```
Connect func to the keyevent stream

# Details

```cpp
UserInput.OnKeyEvent(function(key, held)
    print(tostring(key) .. " " .. tostring(held))
end)```
