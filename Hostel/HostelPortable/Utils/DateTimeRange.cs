using System;

namespace HostelPortable.Utils
{
    public sealed class DateTimeRange
    {
        #region Fields

        private readonly DateTime _start;
        private readonly DateTime _end;
        
        #endregion

        #region Constructors

        public DateTimeRange(DateTime? start, DateTime? end, bool throwOnError = false)
        {
            if (throwOnError && start == null && end == null)
                throw new Exception("«апрещено использовать null дл€ значений границ периода!");

            _start = start ?? DateTime.MinValue;
            _end = end ?? DateTime.MaxValue;

            if (_start > _end)
            {
                throw new Exception("ƒата конца периода меньше даты начала периода!");
            }

            Start = start;
            End = end;
        }

        #endregion

        #region Properties

        public DateTime? Start { get; }

        public DateTime? End { get; }

        #endregion

        #region Methods

        public bool Contains(DateTime date)
        {
            return _start >= date && _end <= date;
        }

        public bool Intersects(DateTimeRange period)
        {
            return period._start <= _end && period._end >= _start;
        }

        public bool Intersects(DateTime? start, DateTime? end)
        {
            return (start ?? DateTime.MinValue) <= _end && (end ?? DateTime.MaxValue) >= _start;
        }

        public DateTimeRange GetIntersection(DateTimeRange period)
        {
            if (!Intersects(period))
            {
                return null;
            }

            var start = period._start >= _start ? period.Start : Start;
            var end = period._end <= _end ? period.End : End;

            return new DateTimeRange(start, end);
        }

        public DateTimeRange GetIntersection(DateTime? start, DateTime? end)
        {
            return GetIntersection(new DateTimeRange(start, end));
        }

        #endregion
    }
}