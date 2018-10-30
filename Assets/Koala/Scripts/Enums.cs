namespace Koala
{
	public enum EAnimatorVariableType
	{
		Int,
		Float,
		Bool,
		Trigger,
	}

	public enum EUIElementType
	{
		Text,
		Slider,
		RawImage,
	}

	public enum ETextHAlignment // Horizontal
	{
		Left = 0x1,
		Center = 0x2,
		Right = 0x4,
		Justified = 0x8,
		Flush = 0x10,
		Geometry = 0x20,
	}

	public enum ETextVAlignment // Vertical
	{
		Top = 0x100,
		Middle = 0x200,
		Bottom = 0x400,
		Baseline = 0x800,
		Geometry = 0x1000,
		Capline = 0x2000,
	}
}
