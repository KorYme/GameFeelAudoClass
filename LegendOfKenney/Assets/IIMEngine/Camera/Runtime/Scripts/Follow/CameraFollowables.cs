using System;
using System.Collections.Generic;
using System.Linq;

namespace IIMEngine.Camera
{
    public static class CameraFollowables
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        private static List<CameraFollowable> _allFollowables = new List<CameraFollowable>();
        
        #pragma warning restore 0414
        #endregion

        public static void Register(CameraFollowable followable)
        {
            //Add Followable into _allFollowables List
            if (_allFollowables.Contains(followable)) return;
            _allFollowables.Add(followable);
        }

        public static void Unregister(CameraFollowable followable)
        {
            if (!_allFollowables.Contains(followable)) return;
            _allFollowables.Remove(followable);
        }

        public static CameraFollowable[] FindByGroups(string[] groups)
        {
            //TODO: Find Followable matching with groups provided
            //You can use Linq Intersect Function (but be careful, it costs memory allocation ;) )
            return _allFollowables.FindAll(x => groups.Intersect(x.TargetGroups).Count() > 0).ToArray();
        }
    }
}