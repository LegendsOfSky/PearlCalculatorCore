using PearlCalculatorLib.PearlCalculationLib.World;
using System.Collections.Generic;

namespace PearlCalculatorLib.PearlCalculationLib.Utility
{
    public static class ListCoverterUtility
    {
        public static List<Chunk> ToChunk(List<Space3D> list)
        {
            List<Chunk> result = new List<Chunk>(list.Count);
            foreach(var item in list)
                result.Add(item.ToChunk());
            return result;
        }

        public static List<Chunk> ToChunk(List<Entity.Entity> list)
        {
            List<Chunk> result = new List<Chunk>(list.Count);
            foreach(var item in list)
                result.Add(item.Position.ToChunk());
            return result;
        }

        public static List<Chunk> ToChunk(List<Surface2D> list)
        {
            List<Chunk> result = new List<Chunk>(list.Count);
            foreach(var item in list)
                result.Add(item.ToChunk());
            return result;
        }

        public static List<Surface2D> ToSurface2D(List<Space3D> list)
        {
            List<Surface2D> result = new List<Surface2D>(list.Count);
            foreach(var item in list)
                result.Add(item.ToSurface2D());
            return result;
        }

        public static List<Surface2D> ToSurface2D(List<Entity.Entity> list)
        {
            List<Surface2D> result = new List<Surface2D>(list.Count);
            foreach(var item in list)
                result.Add(item.Position.ToSurface2D());
            return result;
        }
    }
}
