﻿using Services.ObjectPool;

namespace Services.Providers
{
    public class GameObjectPoolProvider
    {
        public GameObjectPool GameObjectPool { get; set; }
        public GameObjectPool SpiderWebPool { get; set; }
    }
}