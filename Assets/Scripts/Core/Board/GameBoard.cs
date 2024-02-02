using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Board
{
	[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
	public sealed class GameBoard : MonoBehaviour
	{
		[Header("Field size")]
		private static int _size = 3;
		public int fieldSize = 3;
		
		[SerializeField] private GameObject cellPrefab;
		private GridLayoutGroup _gridLayout;
		private RectTransform _rectTransform;
		public int[][] FieldValue;
		private DiContainer _diContainer;
		private List<Cell> _cells = new List<Cell>();
		private DataManager _dataManager;

		[Inject]
		public void Construct(DiContainer diContainer, DataManager dataManager)
		{
			_diContainer = diContainer;
			_dataManager = dataManager;
		}
		private void Start()
		{
			_size = fieldSize;
			var saveData = _dataManager.Load();
			if (saveData != null)
			{
				FieldValue = saveData.FieldValues;
			}
			else
			{
				FieldValue = new int[_size][];
				for (int i = 0; i < _size ; i++)
					FieldValue[i] = new int[_size];
			}
			
			_gridLayout = GetComponent<GridLayoutGroup>();
			_rectTransform = GetComponent<RectTransform>();
			CleanField();
			InitField();
		}

		private void InitField()
		{
			var cellSize = (_rectTransform.rect.width - _size * _gridLayout.spacing.x) / _size;
			_gridLayout.cellSize = new Vector2(cellSize, cellSize);
			_gridLayout.constraintCount = _size;
			for (int x = 0; x < _size; x++)
			{
				int y;
				for (y = 0; y < _size; y++)
				{
					GameObject cell = _diContainer.InstantiatePrefab(cellPrefab, transform);
					var cellComponent = cell.GetComponent<Cell>();
					_cells.Add(cellComponent);
					cellComponent.Position = new FieldPosition(x, y);
					if (FieldValue[x][y] == 0) continue;
					cellComponent.Fill((EPlayerType)FieldValue[x][y]);
				}
			}
			Debug.Log("GameBoard has been initialized.");
		}

		private void CleanField()
		{
			foreach (Transform child in _rectTransform)
			{
				Destroy(child.gameObject);
			}
			Debug.Log("GameBoard has been cleaned.");
		}

		public void DisableButtons()
		{
			foreach (var cell in _cells)
			{
				cell.button.interactable = false;
			}
			Debug.Log("Buttons disabled.");
		}

		public void EnableButtons()
		{
			foreach (var cell in _cells)
			{
				cell.button.interactable = true;
			}
			Debug.Log("Buttons enabled.");
		}
	}
}