using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	[RequireComponent(typeof(Button))]
	public class ExitButton : MonoBehaviour
	{
		private Button _button;
		private void Start()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnClick);
		}
		private void OnClick()
		{
			Application.Quit();
		}
	}
}