namespace OOP_Assignment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question1
            //a)  What is the difference between Method Overloading and Method Overriding?

            //sol :   overloading it means having one orm ore methods with same name but in differnent parameters or different data type
            // overriding it means having one method that already exist in in the parent class inside the child class and it happens when there's inheritance

            //b)  What is the difference between Static Binding and Dynamic Binding?

            //sol : Binding>> 1  static : the method that will be called is decided at compile time
            // Binding>> 2  dynamic : the method that will be called is decided at run time and it happens with overriding
            #endregion

            #region Question2
            //a) What is the purpose of the sealed keyword when applied to a class?
            //sol : Sealed keyword when used on class means no other class can inherit from it

            //b) What is the difference between a sealed class and a sealed method?
            // Sealed class: stops any class from inheriting from it
            // Sealed method: prevents a method from being overridden again in any
            // child classes but the class itself can still be inherited

            //c) Can a sealed method be overridden? Why?
            // No because sealing the method means we stop it from being overridden
            // again in any child class so it stays the same everywhere
            #endregion
        }
    }
}
