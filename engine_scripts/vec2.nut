class Vec2
{
    x = 0.0;
    y = 0.0;

    constructor(_x, _y)
    {
        x = _x;
        y =_y;
    }

    function _add(B)
    {
        return ::Vec2(x + B.x, y + B.y);
    }

    function _tostring()
    {
        return format("{%f, %f}", x, y);
    }
}