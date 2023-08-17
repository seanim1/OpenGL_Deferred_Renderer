# OpenGL_Deferred_Renderer

Unzip the "libraries.zip" file and place the two folders (include and lib) in ./OpenGL_Deferred_Renderer/ folder along with "main.cpp".

Have Visual Studio installed and open and run the .sln file. It should run.

Tested on RTX3080:

It seems to be able to render about 280 point lights + 1 shadow casting directional light at 4K (3840x2160) 60 fps.
It seems to be able to render about 780 point lights + 1 shadow casting directional light at 1080p (1920x1080) 60 fps.

G-Buffers:
<div class="row">
  <img src="Examples/FinishedHD.png?raw=true" width="1000">
  Finished
  <img src="Examples/lightViewDepthHD.png?raw=true" width="200">
  Light View Depth / Shadow Map
  <img src="Examples/PositionHD.png?raw=true" width="300">
  Position (3)
  <img src="Examples/NormalHD.png?raw=true" width="300">
  Normal (3)
  <img src="Examples/ShadowMaskHD.png?raw=true" width="300">
  Shadow Mask (1)
  <img src="Examples/DepthHD.png?raw=true" width="300">
  Depth (Depth)
  <img src="Examples/AlbedoHD.png?raw=true" width="300">
  Albedo (3)
</div>
