using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first
{
    public sealed class SomeType // 1
    {
        // Вложенный класс
        private class  SomeNestedType { }// 2

        // Константа, неизменяемое и статическое изменяемое поле
        private const Int32 SomeConstant = 1;   //  3
        private readonly Int32 SomeReadonlyField = 2; // 4
        private static Int32 SomeReadWriteField = 3; // 5

        // Конструктор типа
        static SomeType() { } // 6

        // Конструкторы экземпляров
        public SomeType() { } // 7
        public SomeType(Int32 x){} // 8

        // Экземлярный и статические методы
        private String InstanceMethod() { return null; } // 9
        private static void Main() { } // 10

        // Непараметризованное экземлярное свойство
        public Int32 SomePop { // 11
            get { return 0; } // 12
            set { } // 13
        }


        // Параметризованное экзмеплярное свойство 
        public Int32 this[String s] { // 14
            get { return 0; } // 15
            set { } // 16
        }


       // Экземлярное событие 
        public event EventHandler SomeEvent; // 17
    }
}
