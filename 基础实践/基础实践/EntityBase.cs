using System;
using System.Collections.Generic;
using System.Text;
using 基础实践;

namespace Main {
    internal abstract class EntityBase : IEntity {
        public EntityType Type { get; private set; }
        public int PosIndex { get; set; }
        public bool IsPause { get; set; }
        abstract public void Draw(Map mapInfo);
        public Vector2i GetGridPosition(Map mapInfo) {
            Grid grid = mapInfo.Grids[PosIndex];
            return grid.Position;
        }

        protected EntityBase(EntityType type,int index) {
            Type = type;
            PosIndex = index;
            IsPause = false;
        }

        
    }

    enum EntityType {
        Player,
        Bot
        
    }
}
