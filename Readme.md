<h1>
  Creating a map configuration file
</h1>

<p>
  The map configuration file is read into the generator to auto generate each tile and their positions in relation to each other.
  In order to use the tool to generate your own custom map you simply need to create a new json file in this directory using
  the template show below. 

  The images you wish to be drawn within the hex tiles should be placed in the Images directory above. Each of these images must
  adhear to the Image dimension requirements below. The shape of these images beforehand does not matter, 
  the hexagons will clip the image to the correct shape.

  To understand how to use the Expand_direction property refer to the Hex Map Expansion Model below.
</p>

<br/>
<br/>


<div>
  <img src="https://i.ibb.co/TmtKkZ1/Hex-Tile-Dimensions.png" align="left">
  <h3>
    Image dimension requirements
  </h3>
  <div>
    <p>
      A hexagon is a six sided polygon. The total of the internal angles within a hexagon must add up to 720 degrees
      or 60 degrees per internal angle. 
    </p>
    </p>
      As a result a hexagons height must not be equal to its width. Using trigonometry you can see that the width of
      a hexagon must be equal to (2 * height) / sqrt(3). Refer to the image on the left.
    </p>
    <p>
      Example: Height = 550 pixels <br/>
      Width = (2 * 550) / sqrt(3) <br/>
      Width = 1100 / 1.73 <br/>
      Width = 635 pixels
    </p>
  </div>
</div>

<br/>
<br/>

<div>
  <h3>
    Template map configuration file
  </h3>
  <pre align="left">
    <code>
      {
        "Tiles":[
          {
            "Rotation": ,                     // The degrees of rotation in the clockwise direction
            "Expand_direction":"",            // The direction of offset in which the next tile should be drawn
            "Image":""                        // The image name and extension
          }
        ],
        "Offset": ,                           // The pixel distance between the hexagons
        "Width": ,                            // The pixel width of the image file
        "Height": ,                           // The pixel height of the image file
      }
    </code>
  </pre>
</div>

<br/>
<br/>

<div>
  <img src="https://i.ibb.co/26LQDGD/Hex-Map-Expansion-Model.png" align="left">
  <h3 align="center">Hex Map Expansion Model</h3>
  <div>
    <p>
      Each tile in the template config file has an Expand_direction property. The combinations of these properties make up the
      shape of the map.
    </p>    
    <p>
      This property dictates where the next tile will be placed in relation to the current tile. For example in the reference image
      on the left: The first tile in the top left corner has an arrow pointing to the next tile in the list. This arrow depicts
      what the Expand_direction would be. In the case of the first tile this property would be SE which implies South East.
    </p>
    <p>
      If you were to flatten this list of expansion directions it would look like this : SE, SW, SE, NE, NE, S, SW, SW, SE, NE, IGNORE.
      The last value: IGNORE, in the list is simply to identify the end of the map construction.
    </p>
  </div>
</div>
