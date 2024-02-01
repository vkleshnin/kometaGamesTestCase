using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	[RequireComponent(typeof(Button))]
	public class ReloadButton : MonoBehaviour
	{
		private Button _button;
		private DataManager _dataManager;

		[Inject]
		public void Construct(DataManager dataManager)
		{
			_dataManager = dataManager;
		}
		private void Start()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			_dataManager.CleanData();
			SceneManager.LoadScene(0);
		}
	}
}