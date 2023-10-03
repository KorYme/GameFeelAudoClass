using IIMEngine.Movements2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IIMEngine.Movements2D
{
    public interface IMovable2DReader : IMove2DDirReader,
            IMove2DOrientReader,
            IMove2DSpeedReader,
            IMove2DSpeedMaxReader,
            IMove2DTurnBackReader,
            IMove2DLockedReader
    {
    }

    public interface IMovable2DWriter : IMove2DDirWriter,
            IMove2DOrientWriter,
            IMove2DSpeedWriter,
            IMove2DSpeedMaxWriter,
            IMove2DTurnBackWriter,
            IMove2DLockedWriter
    {
    }
}
