using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Core.Board
{
	public struct FieldPosition
	{
		public int X;
		public int Y;

		public FieldPosition(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
	
	[RequireComponent(typeof(Button))]
	public sealed class Cell : MonoBehaviour
	{
		public bool IsFilled { get; private set; }
		public FieldPosition Position { get; set; }
		public Button button { get; private set; }

		[SerializeField] private GameObject oPrefab;
		[SerializeField] private GameObject crossPrefab;

		private Gameplay _gameplay;
		private GameBoard _gameBoard;
		
		[Inject]
		public void Construct(Gameplay gameplay, GameBoard gameBoard)
		{
			_gameplay = gameplay;
			_gameBoard = gameBoard;
		}
		private void Awake()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(Fill);
		}
		
		private void Fill()
		{
			if (IsFilled) return;
			
			IsFilled = true;
			_gameBoard.FieldValue[Position.X][ Position.Y] = (sbyte)_gameplay.Move;
			Instantiate(_gameplay.Move == EPlayerType.Circle ? oPrefab : crossPrefab, transform);
			
			_gameplay.EndMove.Invoke();
			button.interactable = false;
			
			Debug.Log($"Cell {Position.X} {Position.Y} filled. Field value: {_gameBoard.FieldValue[Position.X][ Position.Y]}");
		}

		public void Fill(EPlayerType ePlayerType)
		{
			IsFilled = true;
			Instantiate(ePlayerType == EPlayerType.Circle ? oPrefab : crossPrefab, transform);
			button.interactable = false;
			Debug.Log($"Cell {Position.X} {Position.Y} filled. Field value: {_gameBoard.FieldValue[Position.X][ Position.Y]}");
		}
	}
}