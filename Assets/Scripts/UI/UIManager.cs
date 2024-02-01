using Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
	public sealed class UIManager : MonoBehaviour
	{
		[SerializeField] private GameObject winTab;
		[SerializeField] private TextMeshProUGUI headText;
		private string _turnMessage = "The move for the ";
		private string _crossName = "Cross";
		private string _circleName = "Circle";
		private Gameplay _gameplay;

		[Inject]
		public void Constructor(Gameplay gameplay)
		{
			_gameplay = gameplay;
		}
		
		private void SetResultText(EResult result)
		{
			switch (result)
			{
				case EResult.OWin:
					headText.text = $"{_circleName} wins!";
					break;
				case EResult.CrossWin:
					headText.text = $"{_crossName} wins!";
					break;
				case EResult.Draw:
					headText.text = $"Draw!";
					break;
			}

			OpenWinTab();
		}

		private void SetTurnMessage()
		{
			headText.text = $"{_turnMessage} {(_gameplay.Move == PlayerType.Circle ? _circleName : _crossName)}.";
		}

		private void Start()
		{
			SetTurnMessage();
			_gameplay.StartMove += SetTurnMessage;
			_gameplay.EndGame += SetResultText;
		}

		private void OnDestroy()
		{
			_gameplay.EndMove -= SetTurnMessage;
			_gameplay.EndGame -= SetResultText;
		}

		private void OpenWinTab()
		{
			winTab.SetActive(true);
		}
	}
}