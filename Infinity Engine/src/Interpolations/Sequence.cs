using InfinityEngine.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityEngine.Interpolations
{
    /// <summary>
    ///     This class encapsulates a sequence of <see cref="T:InfinityEngine.Interpolations.Interpolable" /> and play them.
    /// </summary>
    public class Sequence
    {
        private readonly List<Interpolable> interpolations;
        private readonly List<Interpolable> toRemoves;

        private float startDelay;

        private int repeatCount;

        private float repeatInterval;

        private LoopTypes loopType;

        private int current;

        private bool terminated;

        private Action<Sequence> StartCallback;

        private Action<Sequence> UpdateCallback;

        private Action<Sequence> CompleteCallback;

        private Action<Sequence> FinishCallback;

        /// <summary>
        /// Creates new sequence
        /// </summary>
        public Sequence()
        {
            interpolations = new List<Interpolable>();
            toRemoves = new List<Interpolable>();
            repeatCount = 1;
            startDelay = 0f;
            repeatInterval = 0f;
            loopType = LoopTypes.Restart;
        }

        /// <summary>
        /// Creates new sequence with the given <see cref="T:InfinityEngine.Interpolations.Interpolable" /> objects in parameter.
        /// </summary>
        /// <param name="args">Array of all <see cref="T:InfinityEngine.Interpolations.Interpolable" /> to add into this <see cref="T:InfinityEngine.Interpolations.Sequence" /></param>
        public Sequence(params Interpolable[] args)
        {
            startDelay = 0f;
            repeatInterval = 0f;
            repeatCount = 1;
            loopType = LoopTypes.Restart;
            interpolations = new List<Interpolable>();
            toRemoves = new List<Interpolable>();
            args.ForEach(elem =>
            {
                Add(elem);
            });
        }

        /// <summary>
        /// Add the given <see cref="T:InfinityEngine.Interpolations.Interpolable" /> into the Sequence
        /// </summary>
        /// <param name="item">The interpolable object</param>
        public void Add(Interpolable item)
        {
            interpolations.Add(item);
        }

        /// <summary>
        /// Removes the given <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object from the sequence.
        /// </summary>
        /// <remarks>
        /// The <see cref="T:InfinityEngine.Interpolations.Interpolable" /> will be removed only after the end of the current <see cref="T:InfinityEngine.Interpolations.Interpolable" /> that is played.
        /// </remarks>
        public void Remove(Interpolable item)
        {
            toRemoves.Add(item);
        }

        /// <summary>
        /// Removes the <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object at the given index from the sequence.
        /// </summary>
        /// <remarks>
        /// The <see cref="T:InfinityEngine.Interpolations.Interpolable" /> will be removed only after the end of the current <see cref="T:InfinityEngine.Interpolations.Interpolable" /> that is played.
        /// </remarks>
        /// <exception cref="T:System.IndexOutOfRangeException">Raised if the index is invalid.</exception>
        /// <param name="index">The index of the <see cref="T:InfinityEngine.Interpolations.Interpolable" /> to removes</param>
        public void Remove(int index)
        {
            if (interpolations == null || index < 0 || index >= interpolations.Count)
            {
                throw new IndexOutOfRangeException();
            }
            toRemoves.Add(interpolations[index]);
        }

        /// <summary>
        /// Returns the <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object at the given index of the sequence.
        /// </summary>
        /// <param name="index">index of the Interpolable</param>
        /// <exception cref="T:System.IndexOutOfRangeException">Raised if the index is invalid.</exception>
        /// <returns>The <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object at the given index</returns>
        public Interpolable GetAt(int index)
        {
            if (interpolations == null || index < 0 || index >= interpolations.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return interpolations[index];
        }

        /// <summary>
        /// Changes the <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object at the given index of the sequence.
        /// </summary>
        /// <exception cref="T:System.IndexOutOfRangeException">Raised if the index is invalid.</exception>
        /// <param name="index">Index of the interpolation</param>
        /// <param name="value">the interpolation</param>
        public void SetAt(int index, Interpolable value)
        {
            if (interpolations == null || index < 0 || index >= interpolations.Count)
            {
                throw new IndexOutOfRangeException();
            }
            if (value == null)
            {
                toRemoves.Add(value);
            }
            else
            {
                interpolations[index] = value;
            }
        }

        /// <summary>
        /// Inserts the given <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object at the given index in the sequence.
        /// </summary>
        /// <param name="index">Index where the interpolation will be inserted</param>
        /// <param name="value">the interpolation</param>
        public void Insert(int index, Interpolable value)
        {
            if (interpolations == null || index < 0 || index >= interpolations.Count)
                throw new IndexOutOfRangeException();
            interpolations.Insert(index, value);
        }

        /// <summary>
        /// Chained method to add repeatition option to the sequence.
        /// </summary>
        /// <param name="count">Number of repeatition</param>
        /// <param name="loopType">Repeatition type : Restart or Reverse</param>
        /// <returns>This <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence SetRepeat(int count, LoopTypes loopType)
        {
            repeatCount = count;
            repeatCount = Mathf.Clamp(repeatCount, -1, repeatCount);
            this.loopType = loopType;
            return this;
        }

        /// <summary>
        /// Adds srepeatition interval to the sequence.
        /// </summary>
        /// <param name="interval">repeat interval</param>
        /// <returns>This <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence SetRepeatInterval(float interval)
        {
            repeatInterval = interval;
            repeatInterval = Mathf.Clamp(repeatInterval, 0f, repeatInterval);
            return this;
        }

        /// <summary>
        /// Adds a starts delay to this sequence
        /// </summary>
        /// <param name="delay">time to wait before starting tthe sequence</param>
        /// <returns>This <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence SetStartDelay(float delay)
        {
            startDelay = delay;
            startDelay = Mathf.Clamp(startDelay, 0f, startDelay);
            return this;
        }

        /// <summary>
        /// On Start callback
        /// </summary>
        /// <param name="arg">callback action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence OnStart(Action<Sequence> arg)
        {
            StartCallback += arg;
            return this;
        }

        /// <summary>
        /// On Update callback
        /// </summary>
        /// <param name="arg">callback action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence OnUpdate(Action<Sequence> arg)
        {
            UpdateCallback += arg;
            return this;
        }

        /// <summary>
        /// On Complete callback
        /// </summary>
        /// <param name="arg">callback action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence OnComplete(Action<Sequence> arg)
        {
            CompleteCallback += arg;
            return this;
        }

        /// <summary>
        /// On Finish callback
        /// </summary>
        /// <param name="arg">callback action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence OnFinish(Action<Sequence> arg)
        {
            FinishCallback += arg;
            return this;
        }

        /// <summary>
        /// REverse this sequence
        /// </summary>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence Reverse()
        {
            interpolations.Reverse();
            return this;
        }

        /// <summary>
        /// Shuffle this sequence
        /// </summary>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence Shuffle()
        {
            interpolations.Shuffle();
            return this;
        }

        /// <summary>
        /// Shift this Sequene
        /// </summary>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Sequence" /></returns>
        public Sequence Shift()
        {
            interpolations.Shift();
            return this;
        }

        /// <summary>
        /// Stop this sequence
        /// </summary>
        public void Terminate()
        {
            terminated = true;
        }

        /// <summary>
        /// Starts the Sequence
        /// </summary>
        public void Start()
        {
            Infinity.DOCoroutine(_Start());
        }

        private IEnumerator _Start()
        {
            int count = 0;
            yield return new WaitForSeconds(startDelay);
            StartCallback?.Invoke(this);
            while ((repeatCount == -1 || count < repeatCount) && !terminated)
            {
                current = 0;
                while (current < interpolations.Count && !terminated)
                {
                    interpolations[current].Start();
                    while (!interpolations[current].IsTerminated && !terminated)
                    {
                        UpdateCallback?.Invoke(this);
                        yield return new WaitForEndOfFrame();
                    }
                    current++;
                    interpolations.RemoveAll((Interpolable elem) => toRemoves.Contains(elem));
                    toRemoves.Clear();
                }
                if (loopType == LoopTypes.Reverse)
                {
                    interpolations.ForEach(elem =>
                    {
                        elem.Reverse();
                    });
                    Reverse();
                }
                CompleteCallback?.Invoke(this);
                count++;
                yield return new WaitForSeconds(repeatInterval);
            }
            FinishCallback?.Invoke(this);
            yield return null;
        }
    }
}
