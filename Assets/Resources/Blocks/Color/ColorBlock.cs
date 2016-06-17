using UnityEngine;
using System.Collections;

public class ColorBlock : Block {
	public override Material GetMaterial() {
		return Resources.Load("Blocks/Color/" + Name + "Material") as Material;
	}
}

public class ColorBlackBlock : ColorBlock {
	public ColorBlackBlock() {
		Name = "ColorBlack";
		DisplayName = "Black Color";
	}
}

public class ColorGrayBlock : ColorBlock {
	public ColorGrayBlock() {
		Name = "ColorGray";
		DisplayName = "Gray Color";
	}
}

public class ColorWhiteBlock : ColorBlock {
	public ColorWhiteBlock() {
		Name = "ColorWhite";
		DisplayName = "White Color";
	}
}

public class ColorTanBlock : ColorBlock {
	public ColorTanBlock() {
		Name = "ColorTan";
		DisplayName = "Tan Color";
	}
}

public class ColorLightBrownBlock : ColorBlock {
	public ColorLightBrownBlock() {
		Name = "ColorLightBrown";
		DisplayName = "Light Brown Color";
	}
}

public class ColorBrownBlock : ColorBlock {
	public ColorBrownBlock() {
		Name = "ColorBrown";
		DisplayName = "Brown Color";
	}
}

public class ColorRedBlock : ColorBlock {
	public ColorRedBlock() {
		Name = "ColorRed";
		DisplayName = "Red Color";
	}
}

public class ColorOrangeBlock : ColorBlock {
	public ColorOrangeBlock() {
		Name = "ColorOrange";
		DisplayName = "Orange Color";
	}
}

public class ColorYellowBlock : ColorBlock {
	public ColorYellowBlock() {
		Name = "ColorYellow";
		DisplayName = "Yellow Color";
	}
}

public class ColorLightGreenBlock : ColorBlock {
	public ColorLightGreenBlock() {
		Name = "ColorLightGreen";
		DisplayName = "Light Green Color";
	}
}

public class ColorGreenBlock : ColorBlock {
	public ColorGreenBlock() {
		Name = "ColorGreen";
		DisplayName = "Green Color";
	}
}

public class ColorLightBlueBlock : ColorBlock {
	public ColorLightBlueBlock() {
		Name = "ColorLightBlue";
		DisplayName = "LightBlue Color";
	}
}

public class ColorBlueBlock : ColorBlock {
	public ColorBlueBlock() {
		Name = "ColorBlue";
		DisplayName = "Blue Color";
	}
}

public class ColorTealBlock : ColorBlock {
	public ColorTealBlock() {
		Name = "ColorTeal";
		DisplayName = "Teal Color";
	}
}

public class ColorPurpleBlock : ColorBlock {
	public ColorPurpleBlock() {
		Name = "ColorPurple";
		DisplayName = "Purple Color";
	}
}

public class ColorPinkBlock : ColorBlock {
	public ColorPinkBlock() {
		Name = "ColorPink";
		DisplayName = "Pink Color";
	}
}