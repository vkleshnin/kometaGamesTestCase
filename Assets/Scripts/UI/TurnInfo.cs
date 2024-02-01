using Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public sealed class TurnInfo : MonoBehaviour
	{
		private TextMeshProUGUI _text;
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
					_text.text = $"{_circleName} wins!";
					break;
				case EResult.CrossWin:
					_text.text = $"{_crossName} wins!";
					break;
				case EResult.Draw:
					_text.text = $"Draw!";
					break;
			}
		}

		private void SetTurnMessage()
		{
			_text.text = $"{_turnMessage} {(_gameplay.Move == PlayerType.Circle ? _circleName : _crossName)}.";
		}

		private void Start()
		{
			_text = GetComponent<TextMeshProUGUI>();
			SetTurnMessage();
			_gameplay.StartMove += SetTurnMessage;
			_gameplay.EndGame += SetResultText;
		}

		private void OnDestroy()
		{
			_gameplay.EndMove -= SetTurnMessage;
			_gameplay.EndGame -= SetResultText;
		}
	}
}