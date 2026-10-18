using System;
using UnityEngine;

namespace MiniGame
{
    public class MiniGameEgg : MiniGameEntity
    {
        public bool hasBeenFound { get; private set; }
        public static event Action onEggFound;
        public static MiniGameEgg Instance;

        [SerializeField] GameObject toActiveOnFound;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override void OnGameReset()
        {
            base.OnGameReset();
            toActiveOnFound.SetActive(false);
            hasBeenFound = false;
        }

        protected override void OnGameLateUpdate(MiniGameInput input)
        {
            if (MiniGamePlayer.Instance.currentCoords.IsAdjacent(currentCoords))
                FindEgg();
        }

        void FindEgg()
        {
            hasBeenFound = true;
            toActiveOnFound.SetActive(true);
            onEggFound?.Invoke();
        }
    }
}
