using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.MvcUi.Models
{
    public class OneThingCase<T>
    {
        private T _thing;

        public bool IsEmpty { get; private set; } = true;

        public void Put(T thing)
        {
            if (!IsEmpty) throw new InvalidOperationException("Depozyt nie może być zadysponowany");

            _thing = thing;
            IsEmpty = false;
        }

        public T Get()
        {
            if (IsEmpty) throw new InvalidOperationException("Jak chcesz cos wyjąć ze schowka, jak on jest pusty. Z pustego i Salomon nie naleje");

            IsEmpty = true;

            return _thing;
        }
    }
}