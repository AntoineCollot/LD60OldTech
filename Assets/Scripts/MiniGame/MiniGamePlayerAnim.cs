using UnityEngine;

namespace MiniGame
{
    public class MiniGamePlayerAnim : MiniGameIdleAnim
    {
        [SerializeField] protected Sprite happySprite;
        bool IsHappy => MiniGameEgg.Instance.hasBeenFound;
        protected override Sprite AnimSprite
        {
            get
            {
                if(IsHappy)
                    return happySprite;
                return idleSprite;
            }
        }
    }
}
