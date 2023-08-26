# OpenGL_Deferred_Shading
The following project implements Deferred Rendering and Shadow Mapping based on examples from “learnopengl.com”.

Link to Youtube video demo: ( https://youtu.be/PEu0Og-CiwY )

The rendering is done in several passes:

&emsp;1st Pass (Shadow Map): The Light View Depth Map is drawn from the directional light’s perspective on a 1024x1024 texture.

&emsp;2nd Pass (Geometry Pass): The following G-Buffers are generated: Position (3), Shadow Mask (1), Normal (3), Albedo (3). Shadow calculation is handled in this step based on the Light View Depth Map.

&emsp;3rd Pass (Lighting Pass): Based on the G-Buffers, Blinn-Phong lighting method is used to determine the final pixel color which takes into account every single point light. This can be improved via Tiled Deferred Shading.

Installation:

&emsp;Unzip the "libraries.zip" file and place the two folders (include and lib) in ./OpenGL_Deferred_Renderer/ folder along with "main.cpp".

&emsp;Have Visual Studio installed and open and run the .sln file. It should run.

Tested on RTX3080:

&emsp;It seems to be able to render about 280 point lights + 1 shadow casting directional light at 4K (3840x2160) 60 fps.
&emsp;It seems to be able to render about 780 point lights + 1 shadow casting directional light at 1080p (1920x1080) 60 fps.

G-Buffers:
<div class="row">
  Finished
  <img src="Examples/FinishedHD.png?raw=true" width="1000">
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
