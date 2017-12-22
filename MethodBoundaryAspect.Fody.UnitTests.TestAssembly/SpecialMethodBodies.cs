using System;
using System.Collections.Generic;
using System.Linq;
using MethodBoundaryAspect.Fody.UnitTests.TestAssembly.Aspects;

namespace MethodBoundaryAspect.Fody.UnitTests.TestAssembly
{
    [FirstAspect]
    public class SpecialMethodBodies
    {
        public static void MethodBodyEndsWithBrtrueOpcode()
        {
            var x = new List<string>();
            if (x.Any())
            {
                Console.WriteLine("test");
            }
        }

        public static void MethodBodyWithBrtrueOpcodeEndsWithBrtrueOpcode()
        {
            var x = new List<string>();
            if (!x.Any())
            {
                Console.WriteLine("test");
            }

            if (x.Any())
            {
                Console.WriteLine("test");
            }
        }

        public static void MethodBodyEndsWithBleOpcode()
        {
            var x = 5;
            if (x < 3)
            {
                Console.WriteLine("test");
            }
        }

        [OnlyOnEntryAspect]
        public string MethodWhichEndsWithThrowAndHasMultipleReturns(int number)
        {
            switch (number)
            {
                case 1:
                    return "1";
                case 2:
                    return "2";
                default:
                    throw new InvalidOperationException("This exception is expected");
            }
        }

        /// <summary>
        /// from https://github.com/vescon/MethodBoundaryAspect.Fody/issues/10
        /// </summary>
        /// <param name="value"></param>
        [OnlyOnEntrAndExityAspect]
        public void StrangeMethodForIssue9(bool value)
        {
            if (!value)
                return;
            
            SomeMethod();  // When the comment is removed it still fails

            var window = new Outer
            {
                Inner = new Inner(),
                DateTime = new DateTime()
            };

            window.Show();
        }

        private void SomeMethod()
        {
            // this method is really empty
        }

        private class Outer
        {
            public Inner Inner { get; set; }
            public DateTime DateTime { get; set; }

            public void Show()
            {
            }
        }

        private class Inner
        {
        }
    }
}