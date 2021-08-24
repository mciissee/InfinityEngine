using System;
using System.Collections.Generic;
using System.Linq;

namespace InfinityEngine.Interpolations
{
    [Serializable]
    internal class InterpolationManager
    {
        internal interface IInterpolationPool
        {
            void Clear();

            void Reset();
        }

        [Serializable]
        internal class InterpolationPool<T> : IInterpolationPool
        {
            private Stack<Interpolation<T>> availables;

            private List<Interpolation<T>> all;

            public InterpolationPool(int initialCapacity)
            {
                availables = new Stack<Interpolation<T>>();
                all = new List<Interpolation<T>>();
                T val = default(T);
                for (int i = 0; i < initialCapacity; i++)
                {
                    Interpolation<T> item = new Interpolation<T>(val, val, 0f, null);
                    all.Add(item);
                    availables.Push(item);
                }
            }

            public Interpolation<T> Pop(T starts, T ends, float duration, Action<T> setter)
            {
                if (!availables.Any())
                {
                    Interpolation<T> item = new Interpolation<T>(starts, ends, duration, setter);
                    all.Add(item);
                    availables.Push(item);
                }
                return availables.Pop().Reset(starts, ends, duration, setter);
            }

            public void Push(Interpolation<T> interpolation)
            {
                availables.Push(interpolation);
            }

            public void Clear()
            {
                all.Clear();
                availables.Clear();
            }

            public void Reset()
            {
                all.Clear();
                foreach (Interpolation<T> item in all)
                {
                    availables.Push(item);
                }
            }
        }

        internal Dictionary<Type, IInterpolationPool> pools;

        internal InterpolationManager()
        {
            pools = new Dictionary<Type, IInterpolationPool>();
        }

        internal Interpolation<T> Pop<T>(T starts, T ends, float duration, Action<T> setter)
        {
            Type typeFromHandle = typeof(T);
            if (pools.TryGetValue(typeFromHandle, out IInterpolationPool value))
            {
                return (value as InterpolationPool<T>).Pop(starts, ends, duration, setter);
            }
            InterpolationPool<T> interpolationPool = new InterpolationPool<T>(5);
            pools.Add(typeFromHandle, interpolationPool);
            return interpolationPool.Pop(starts, ends, duration, setter);
        }

        internal void Push<T>(Interpolation<T> interpolation)
        {
            if (pools.TryGetValue(typeof(T), out IInterpolationPool value))
            {
                (value as InterpolationPool<T>).Push(interpolation);
            }
        }

        internal void Clear()
        {
            foreach (KeyValuePair<Type, IInterpolationPool> pool in pools)
            {
                pool.Value.Clear();
            }
        }

        internal void Reset()
        {
            foreach (KeyValuePair<Type, IInterpolationPool> pool in pools)
            {
                pool.Value.Reset();
            }
        }
    }
}
