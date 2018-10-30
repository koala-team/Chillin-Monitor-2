using UnityEngine;
using UnityEngine.UI;

public class SliderParts : MonoBehaviour
{
	public Slider Slider { get; set; }
	public Image BackgroundImage { get; set; }
	public Image FillImage { get; set; }


	void Awake ()
	{
		Slider = transform.GetComponent<Slider>();
		BackgroundImage = transform.Find("Background").GetComponent<Image>();
		FillImage = transform.Find("Fill Area/Fill").GetComponent<Image>();
	}
}
