﻿using System;
using UniRx;

namespace BouncingBall.Game.Gameplay.Coins
{
    public class CoinData
    {
        private readonly int _rewordCount;
        private readonly Subject<int> _reword;

        public IObservable<int> Reword => _reword;

        public CoinData(int reword)
        {
            _rewordCount = reword;
            _reword = new();
        }

        public void SendReword()
        {
            _reword.OnNext(_rewordCount);
        }
    }
}
