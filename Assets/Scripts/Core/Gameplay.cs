using System;
using Core.Board;
using UI;
using UnityEngine;
using Zenject;

namespace Core
{
	public sealed class Gameplay : IDisposable
	{
		public Action StartMove;
		public Action EndMove;
		public Action<EResult> EndGame;
		
		public PlayerType Move { get; set; }
		private Validation _validation;
		private GameBoard _gameBoard;
		private DataManager _dataManager;

		[Inject]
		public Gameplay(Validation validation, GameBoard gameBoard, DataManager dataManager)
		{
			StartMove += OnStartMove;
			EndMove += OnEndMove;
			EndGame += OnEndGame;
			_validation = validation;
			_gameBoard = gameBoard;
			_dataManager = dataManager;
			var saveData = _dataManager.Load();
			Move = saveData?.lastMove ?? PlayerType.Cross;
		}

		private void OnEndGame(EResult result)
		{
			_gameBoard.DisableButtons();
		}

		private void OnEndMove()
		{
			Move = Move == PlayerType.Circle ? PlayerType.Cross : PlayerType.Circle;
			
			EResult result = _validation.Validate(_gameBoard.FieldValue);
			if (result != EResult.NotFinished)
			{
				EndGame.Invoke(result);
				
				int[][] field = new int[_gameBoard.fieldSize][];
				for (int i = 0; i < field.Length; i++)
				{
					field[i] = new int[_gameBoard.fieldSize];
				}
				
				_dataManager.Save(field, Move);
				Debug.Log($"Result game - {result}");
				return ;
			}
			_dataManager.Save(_gameBoard.FieldValue, Move);
			StartMove.Invoke();
		}

		private void OnStartMove()
		{
			_gameBoard.EnableButtons();
		}


		public void Dispose()
		{
			StartMove -= OnStartMove;
			EndMove -= OnEndMove;
		}
	}
}