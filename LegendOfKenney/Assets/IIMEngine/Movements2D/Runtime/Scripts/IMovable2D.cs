using IIMEngine.Movements2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IIMEngine.Movements2D
{
    public interface IMovable2D : IMove2DDirWriter, IMove2DOrientWriter, IMove2DSpeedWriter, IMove2DSpeedMaxWriter, IMove2DTurnBackWriter, IMove2DLockedWriter,
            IMove2DDirReader, IMove2DOrientReader, IMove2DSpeedReader, IMove2DSpeedMaxReader, IMove2DTurnBackReader, IMove2DLockedReader
    {
        new Vector2 MoveDir { get; set; }

        new Vector2 OrientDir { get; set; }

        new float OrientX { get; set; }

        new float OrientY { get; set; }

        new float MoveSpeed { get; set; }

        new float MoveSpeedMax { get; set; }

        new bool IsTurningBack { get; set; }

        new bool AreMovementsLocked { get; set; }
    }
}
