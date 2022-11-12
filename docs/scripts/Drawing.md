# Functions

```cpp
int new(const char* type, ...);
```
Create a new drawing object then get the id for the newly created object

```cpp
void visible(int id, bool visible);
```
Change an objects visibility

# Details

```lua
local colour = Color.new(255, 0, 0, 255)

local obj = Drawing.new("Line", 50, 50, 150, 150, colour, 3) -- type, x, y, x2, y2, colour, thickness
Drawing.visible(obj, true)
```

```lua
local colour = Color.new(255, 0, 0, 255)

local obj = Drawing.new("Text", 50, 50, colour, "Hello world!") -- type, x, y, colour, text
Drawing.visible(obj, true)
```

```lua
local colour = Color.new(255, 0, 0, 255)

local obj = Drawing.new("Square", 50, 50, 200, 200, colour, 8) -- type, x, y, sizex, sizey, colour, scale
Drawing.visible(obj, true)
```

```lua
local colour = Color.new(255, 0, 0, 255)

local obj = Drawing.new("Triangle", 50, 50, 200, 50, 50, 200, colour) -- type, x, y, x2, y2, x3, y3 colour
Drawing.visible(obj, true)
```
