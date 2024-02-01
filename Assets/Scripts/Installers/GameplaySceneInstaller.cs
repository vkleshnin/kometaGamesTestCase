using Core;
using Core.Board;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
	public sealed class GameplaySceneInstaller : MonoInstaller
	{
		[SerializeField] private GameBoard gameBoard;
		[SerializeField] private TurnInfo turnInfo;
		public override void InstallBindings()
		{
			Container.Bind<Gameplay>().FromNew().AsSingle().NonLazy();
			Container.Bind<GameBoard>().FromInstance(gameBoard).AsSingle();
			Container.Bind<Validation>().FromNew().AsSingle().NonLazy();
			Container.Bind<DataManager>().FromNew().AsSingle().NonLazy();
		}
	}
}