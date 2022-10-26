# SkiaSharpClippingBug
Demonstrates a bug with non-rectangular clipping of an image

Using `SKCanvas.Save()` before applying non-rectangular clip to an image that is scaled causes the resulting image to be incorrectly clipped.

Left circle is done with `SKCanvas.Save()` while the right circle is done with `SKCanvas.SaveLayer()`
![image](https://user-images.githubusercontent.com/116768779/198153917-b3ad6980-794a-4372-928b-8897221128b6.png)
